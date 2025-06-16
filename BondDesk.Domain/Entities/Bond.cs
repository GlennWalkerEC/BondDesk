using BondDesk.Domain.Interfaces.Entities;
using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Providers;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Statics;

namespace BondDesk.Domain.Entities;

public class Bond : IGiltInfo, IBondEntity
{
	private readonly IQuoteRepo _quoteRepo;
	private readonly IGiltInfo _giltInfo;
	private readonly IDateTimeProvider _dateTimeProvider;
	private readonly decimal _assumedReinvestmentRate;

	public Bond(IQuoteRepo quoteRepo, IGiltInfo giltInfo, IDateTimeProvider dateTimeProvider, decimal assumedReinvestmentRate)
	{
		_quoteRepo = quoteRepo ?? throw new ArgumentNullException(nameof(quoteRepo), "Quote repository cannot be null.");
		_giltInfo = giltInfo ?? throw new ArgumentNullException(nameof(giltInfo), "Gilt information cannot be null.");
		_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider), "DateTime provider cannot be null.");
		_assumedReinvestmentRate = assumedReinvestmentRate;
	}

	public decimal FaceValue => _giltInfo.FaceValue;
	public string Name => _giltInfo.Name;
	public decimal Coupon => _giltInfo.Coupon / 100;
	public DateTime IssueDate => _giltInfo.IssueDate;
	public DateTime MaturityDate => _giltInfo.MaturityDate;
	public string Epic => _giltInfo.Epic ?? throw new InvalidOperationException("Epic cannot be null.");
	public decimal Tenor => (_giltInfo.MaturityDate - _dateTimeProvider.GetToday()).Days / 365m;

	public IBondQuoteData GetValuation() => _quoteRepo.BondValuation(Epic).Result;

	public decimal DaysSinceLastCoupon => CalculateDaysSinceLastCoupon();
	public decimal OfferPrice => GetValuation().Offer ?? throw new NullReferenceException(nameof(Epic));
	public decimal AccruedDays => CalculateDaysSinceLastCoupon();
	public decimal DaysSinceLastPayment => CalculateDaysSinceLastCouponPayment();
	public decimal AccruedInterest => CalculateAccruedInterest();

	public decimal DirtyPrice => CalculateDirtyPrice();
	public decimal CurrentYield => Coupon * FaceValue / OfferPrice;
	public decimal Convexity => CalculateConvexity();
	public decimal ModifiedDuration => CalculateModifiedDuration();
	public decimal PresentValueOverDirty => CalculatePresentValueOverDirty();
	public decimal YieldToMaturity => CalculateYieldToMaturity(out _);

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

	protected decimal CalculateDirtyPrice()
	{
		var lastAndNext = LastAndNextCoupons().ToArray();
		decimal accruedInterest = (FaceValue * Coupon * CalculateDaysSinceLastCoupon()) / GetCouponPeriodDays();
		return OfferPrice + accruedInterest;
	}

    protected decimal CalculateModifiedDuration()
    {
        return CalculateMacaulayDuration() / (1 + CalculateYieldToMaturity(out _));
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

	protected decimal CalculateYieldToMaturity(out bool isApproximate)
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
				isApproximate = false;
				return newYTM;
			}
			ytm = newYTM;
		}

		isApproximate = true;
		return ytm;
	}

	protected decimal CalculateMacaulayDuration()
	{
		decimal duration = 0;
		decimal totalPV = 0;
		var ytm = CalculateYieldToMaturity(out _);

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
		var ytm = CalculateYieldToMaturity(out _);

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

		return principalPV / DirtyPrice;
	}
}