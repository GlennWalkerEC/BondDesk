using BondDesk.Domain.Interfaces.Entities;
using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Providers;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Statics;
using System.Text;

namespace BondDesk.Domain.Entities;

public class Bond : IGiltInfo, IBondEntity
{
	private readonly IQuoteRepo _quoteRepo;
	private readonly IGiltInfo _giltInfo;
	private readonly IDateTimeProvider _dateTimeProvider;
	private readonly decimal _assumedReinvestmentRate;

	private IBondQuoteData _valuation;

	public Bond(IQuoteRepo quoteRepo, IGiltInfo giltInfo, IDateTimeProvider dateTimeProvider, decimal assumedReinvestmentRate)
	{
		_quoteRepo = quoteRepo ?? throw new ArgumentNullException(nameof(quoteRepo), "Quote repository cannot be null.");
		_giltInfo = giltInfo ?? throw new ArgumentNullException(nameof(giltInfo), "Gilt information cannot be null.");
		_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider), "DateTime provider cannot be null.");
		_assumedReinvestmentRate = assumedReinvestmentRate;
	}

	protected IBondQuoteData Valuation
	{
		get
		{
			if (_valuation  == null)
			{
				_valuation = _quoteRepo.BondValuation(Epic).Result;
			}
			return _valuation;
		}
	}
	public async Task GetValuation() => _valuation = await _quoteRepo.BondValuation(Epic);

	public decimal FaceValue => _giltInfo.FaceValue;
	public string Name => _giltInfo.Name;
	public decimal Coupon => _giltInfo.Coupon / 100;
	public DateTime IssueDate => _giltInfo.IssueDate;
	public DateTime MaturityDate => _giltInfo.MaturityDate;	
	public string Epic => _giltInfo.Epic ?? throw new InvalidOperationException("Epic cannot be null.");
	public decimal Tenor => (_giltInfo.MaturityDate - _dateTimeProvider.GetToday()).Days / 365m;

	
	public decimal OfferPrice => Valuation.Offer ?? Valuation.LastPrice ?? throw new NullReferenceException("LastPrice");
	public decimal? OfferQty => Valuation.OfferQty;
	public decimal MarketSize => Valuation.MarketSize ?? 0m;
	public decimal? LastPrice => Valuation.LastPrice;
	public decimal? Mid => Valuation.Mid;
	public decimal? Open => Valuation.Open;
	public decimal? Close => Valuation.Close;

	public decimal? LastPricePercentageChange => CalculateLastPricePercentageChange();
	public decimal? OpenPricePercentageChange => CalculateOpenPricePercentageChange();
	public decimal DaysSinceLastCoupon => CalculateDaysSinceLastCoupon();
	public decimal AccruedDays => CalculateDaysSinceLastCoupon();
	public decimal DaysSinceLastPayment => CalculateDaysSinceLastCouponPayment();
	public decimal AccruedInterest => CalculateAccruedInterest();

	public decimal DirtyPrice => CalculateDirtyPrice();
	public decimal CurrentYield => Coupon * FaceValue / OfferPrice;
	public decimal Convexity => CalculateConvexity();
	public decimal ModifiedDuration => CalculateModifiedDuration();
	public decimal PresentValueOverDirty => CalculatePresentValueOverDirty();
	public decimal YieldToMaturity => CalculateYieldToMaturity();
	public bool YieldToMaturityIsEstimate { get; protected set; }
	public decimal PresentValue => CalculatePresentValue();
	public decimal DV01 => CalculateDV01();

	protected decimal? CalculateLastPricePercentageChange()
	{
		if (Close == null || LastPrice == null) return null;
		return ((LastPrice - Close) / Close) * 100;
	}

	protected decimal? CalculateOpenPricePercentageChange()
	{
		if (Close == null || Open == null) return null;
		return ((Open - Close) / Close) * 100;
	}

	protected IEnumerable<Coupon> LastAndRemainingCoupons()
	{
		var coupons = new List<Coupon>();
		var today = _dateTimeProvider.GetToday();

		for (var date = _giltInfo.MaturityDate; date >= today.AddMonths(-_giltInfo.CouponPeriodMonths); date = date.AddMonths(-_giltInfo.CouponPeriodMonths))
		{
			coupons.Add(new Coupon(date, FaceValue, Coupon));
		}

		return coupons.OrderBy(x => x.Date);
	}

	protected IEnumerable<Coupon> LastAndNextCoupons() => LastAndRemainingCoupons().OrderBy(x => x.Date).Take(2).ToArray();

	protected IEnumerable<Coupon> RemainingCoupons() => LastAndRemainingCoupons().Where(x => x.Date > _dateTimeProvider.GetToday());

	protected decimal CalculateDaysSinceLastCoupon()
	{
		var today = _dateTimeProvider.GetToday();

		// Calculate days since last coupon
		return Convert.ToDecimal(Math.Abs((today - LastAndRemainingCoupons().Min(x => x.Date)).Days));
	}

	protected decimal CalculateDaysSinceLastCouponPayment()
	{
		var today = _dateTimeProvider.GetToday();

		// Calculate days since last coupon
		return Convert.ToDecimal(Math.Abs((today - LastAndRemainingCoupons().Min(x => x.PaymentDate)).Days));
	}

	protected decimal CalculateDirtyPrice() => OfferPrice + CalculateAccruedInterest();	

    protected decimal CalculateModifiedDuration()
    {
        return CalculateMacaulayDuration() / (1 + CalculateYieldToMaturity());
    }

	private decimal CalculateAccruedInterest()
	{
		var lastAndNext = LastAndNextCoupons().ToArray();
		return (FaceValue * (Coupon / 2)) * (AccruedDays / GetCouponPeriodDays());
	}

	protected int GetCouponPeriodDays()
	{
		var lastAndNext = LastAndNextCoupons().ToArray();
		return (lastAndNext[1].Date - lastAndNext[0].Date).Days;
	}

	protected decimal CalculateYieldToMaturity()
	{
		decimal ytm = 0.045m; // Initial guess
		decimal tolerance = 0.0001M;
		int maxIterations = 1000;

		for (int i = 0; i < maxIterations; i++)
		{
			decimal f = 0, df = 0;
			for (int t = 1; t <= Tenor; t++)
			{
				decimal discountFactor = DecimalFunctions.DecimalPow((1 + ytm), t);
				f += (Coupon * FaceValue) / discountFactor;
				df += -(t * (Coupon * FaceValue)) / (discountFactor * (1 + ytm));
			}
			f += FaceValue / DecimalFunctions.DecimalPow((1 + ytm), Tenor) - OfferPrice;
			df += -Tenor * FaceValue / DecimalFunctions.DecimalPow((1 + ytm), Tenor);

			decimal newYTM = ytm - f / df;
			if (Math.Abs(newYTM - ytm) < tolerance)
			{
				YieldToMaturityIsEstimate = false;
				return newYTM;
			}
			ytm = newYTM;
		}

		YieldToMaturityIsEstimate = true;
		return ytm;
	}

	protected decimal CalculateMacaulayDuration()
	{
		decimal duration = 0;
		decimal totalPV = 0;
		var ytm = CalculateYieldToMaturity();

		for (int t = 1; t <= Tenor; t++)
		{
			decimal discountFactor = DecimalFunctions.DecimalPow((1 + ytm), t);
			decimal cashFlow = (Coupon * FaceValue);
			duration += (t * cashFlow) / discountFactor;
			totalPV += cashFlow / discountFactor;
		}

		totalPV += FaceValue / DecimalFunctions.DecimalPow((1 + ytm), Tenor);
		duration += (Tenor * FaceValue) / DecimalFunctions.DecimalPow((1 + ytm), Tenor);

		return duration / totalPV;
	}

	protected decimal CalculateConvexity()
    {
        decimal convexity = 0;
        decimal totalPV = 0;
		var ytm = CalculateYieldToMaturity();

		for (int t = 1; t <= Tenor; t++)
        {
            decimal discountFactor = DecimalFunctions.DecimalPow((1 + ytm), t);
            decimal cashFlow = (Coupon * FaceValue);
            convexity += (t * (t + 1) * cashFlow) / discountFactor;
            totalPV += cashFlow / discountFactor;
        }

        totalPV += FaceValue / DecimalFunctions.DecimalPow((1 + ytm), Tenor);
        convexity += (Tenor * (Tenor + 1) * FaceValue) / DecimalFunctions.DecimalPow((1 + ytm), Tenor);

        return convexity / (totalPV * DecimalFunctions.DecimalPow((1 + ytm), 2));
    }

	public decimal CalculatePresentValueOverDirty()
	{
		decimal principalPV = CalculatePresentValue();

		return principalPV / DirtyPrice;
	}

	private decimal CalculatePresentValue()
	{
		var years = Convert.ToDecimal((MaturityDate - _dateTimeProvider.GetToday()).TotalDays / 365.2425);

		var compoundingPerYear = 1; // Annual compounding

		decimal ratePerPeriod = _assumedReinvestmentRate / compoundingPerYear;
		decimal totalPeriods = years * compoundingPerYear;
		decimal factor = DecimalFunctions.DecimalPow(1 + ratePerPeriod, totalPeriods);

		var principalPV = FaceValue / factor;

		// Future Coupons
		foreach (var coupon in RemainingCoupons())
		{
			principalPV += coupon.CalculatePresentValue(_dateTimeProvider, _assumedReinvestmentRate);
		}

		return principalPV;
	}

	private decimal CalculateDV01()
	{
	    return ModifiedDuration * DirtyPrice * 0.01m;
	}

	public override string ToString()
	{
		var sb = new StringBuilder();
		sb.AppendLine($"{nameof(FaceValue)}: {FaceValue}");
		sb.AppendLine($"{nameof(Coupon)}: {Coupon}");
		sb.AppendLine($"{nameof(IssueDate)}: {IssueDate}");
		sb.AppendLine($"{nameof(MaturityDate)}: {MaturityDate}");
		sb.AppendLine($"{nameof(Tenor)}: {Tenor}");
		sb.AppendLine($"{nameof(DaysSinceLastCoupon)}: {DaysSinceLastCoupon}");
		sb.AppendLine($"{nameof(OfferPrice)}: {OfferPrice}");
		sb.AppendLine($"{nameof(OfferQty)}: {OfferQty}");
		sb.AppendLine($"{nameof(MarketSize)}: {MarketSize}");
		sb.AppendLine($"{nameof(AccruedDays)}: {AccruedDays}");
		sb.AppendLine($"{nameof(DaysSinceLastPayment)}: {DaysSinceLastPayment}");
		sb.AppendLine($"{nameof(AccruedInterest)}: {AccruedInterest}");
		sb.AppendLine($"{nameof(YieldToMaturityIsEstimate)}: {YieldToMaturityIsEstimate}");
		sb.AppendLine($"{nameof(PresentValue)}: {PresentValue}");
		sb.AppendLine($"{nameof(Close)}: {Close}");
		sb.AppendLine($"{nameof(LastPrice)}: {LastPrice}");
		sb.AppendLine($"{nameof(Mid)}: {Mid}");
		sb.AppendLine($"{nameof(Open)}: {Open}");
		sb.AppendLine($"{nameof(LastPricePercentageChange)}: {LastPricePercentageChange}");
		sb.AppendLine($"{nameof(OpenPricePercentageChange)}: {OpenPricePercentageChange}");
		sb.AppendLine($"{nameof(Convexity)}: {Convexity}");
		sb.AppendLine($"{nameof(DV01)}: {DV01}");
		return sb.ToString();
	}
}