using BondDesk.Domain.Interfaces.Entities;
using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Providers;
using BondDesk.Domain.Interfaces.Repos;

namespace BondDesk.Domain.Entities;

public class Bond : IGiltInfo, IBondEntity
{
	private readonly IQuoteRepo _quoteRepo;
	private readonly IGiltInfo _giltInfo;
	private readonly IDateTimeProvider _dateTimeProvider;

	public Bond(IQuoteRepo quoteRepo, IGiltInfo giltInfo, IDateTimeProvider dateTimeProvider)
	{
		_quoteRepo = quoteRepo ?? throw new ArgumentNullException(nameof(quoteRepo), "Quote repository cannot be null.");
		_giltInfo = giltInfo ?? throw new ArgumentNullException(nameof(giltInfo), "Gilt information cannot be null.");
		_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider), "DateTime provider cannot be null.");
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
	public decimal LastPrice => GetValuation().LastPrice ?? throw new NullReferenceException(nameof(Epic));
	public decimal AccruedDays => CalculateDaysSinceLastCoupon();
	public decimal DaysSinceLastPayment => CalculateDaysSinceLastCouponPayment();
	public decimal AccruedInterest => CalculateAccruedInterest();

	public decimal DirtyPrice => CalculateDirtyPrice();
	public decimal RunningYield => Coupon * FaceValue / LastPrice * FaceValue;
	public decimal Convexity => CalculateConvexity();
	public decimal YieldToWorst => CalculateYieldToWorst();
	public decimal ModifiedDuration => CalculateModifiedDuration();

	protected IEnumerable<Coupon> LastAndRemainingCoupons()
	{
		var coupons = new List<Coupon>();
		var today = _dateTimeProvider.GetToday();

		for (var date = _giltInfo.MaturityDate; date >= today.AddMonths(-_giltInfo.CouponPeriodMonths); date = date.AddMonths(-_giltInfo.CouponPeriodMonths))
		{
			coupons.Add(new Coupon(date, _giltInfo.Coupon));
		}

		return coupons.OrderBy(x => x.Date);
	}

	protected IEnumerable<Coupon> LastAndNextCoupon() => LastAndRemainingCoupons().OrderBy(x => x.Date).Take(2).ToArray();

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
		var lastAndNext = LastAndNextCoupon().ToArray();
		decimal accruedInterest = (FaceValue * Coupon * CalculateDaysSinceLastCoupon()) / GetCouponPeriodDays();
		return LastPrice + accruedInterest;
	}

    protected decimal CalculateModifiedDuration()
    {
        return CalculateMacaulayDuration() / (1 + CalculateYTM(out _));
    }

	private decimal CalculateAccruedInterest()
	{
		var lastAndNext = LastAndNextCoupon().ToArray();
		return (FaceValue * (Coupon / 2)) * (AccruedDays / GetCouponPeriodDays());
	}

	protected int GetCouponPeriodDays()
	{
		var lastAndNext = LastAndNextCoupon().ToArray();
		return (lastAndNext[1].Date - lastAndNext[0].Date).Days;
	}


	protected decimal CalculateYTM(out bool isApproximate)
	{
		decimal ytm = 0.045m; // Initial guess
		decimal tolerance = 0.0001M;
		int maxIterations = 1000;

		for (int i = 0; i < maxIterations; i++)
		{
			decimal f = 0, df = 0;
			for (int t = 1; t <= Tenor; t++)
			{
				decimal discountFactor = (decimal)Math.Pow((double)(1 + ytm), t);
				f += (Coupon * FaceValue) / discountFactor;
				df += -(t * (Coupon * FaceValue)) / (discountFactor * (1 + ytm));
			}
			f += FaceValue / (decimal)Math.Pow((double)(1 + ytm), (double)Tenor) - LastPrice;
			df += -Tenor * FaceValue / (decimal)Math.Pow((double)(1 + ytm), (double)Tenor + 1);

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
		var ytm = CalculateYTM(out _);

		for (int t = 1; t <= Tenor; t++)
		{
			decimal discountFactor = (decimal)Math.Pow((double)(1 + ytm), t);
			decimal cashFlow = (Coupon * FaceValue);
			duration += (t * cashFlow) / discountFactor;
			totalPV += cashFlow / discountFactor;
		}

		totalPV += FaceValue / (decimal)Math.Pow((double)(1 + ytm), (double)Tenor);
		duration += (Tenor * FaceValue) / (decimal)Math.Pow((double)(1 + ytm), (double)Tenor);

		return duration / totalPV;
	}

	protected decimal CalculateYieldToWorst()
	{
		throw new NotImplementedException();
	}

	protected decimal CalculateConvexity()
    {
        decimal convexity = 0;
        decimal totalPV = 0;
		var ytm = CalculateYTM(out _);

		for (int t = 1; t <= Tenor; t++)
        {
            decimal discountFactor = (decimal)Math.Pow((double)(1 + ytm), t);
            decimal cashFlow = (Coupon * FaceValue);
            convexity += (t * (t + 1) * cashFlow) / discountFactor;
            totalPV += cashFlow / discountFactor;
        }

        totalPV += FaceValue / (decimal)Math.Pow((double)(1 + ytm), (double)Tenor);
        convexity += (Tenor * (Tenor + 1) * FaceValue) / (decimal)Math.Pow((double)(1 + ytm), (double)Tenor);

        return convexity / (totalPV * (decimal)Math.Pow((double)(1 + ytm), 2));
    }
}