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
	public decimal DirtyPrice => CalculateDirtyPrice();
	public decimal RunningYield => Coupon * FaceValue / LastPrice * FaceValue;
	public decimal AccruedDays => CalculateDaysSinceLastCoupon();
	public decimal DaysSinceLastPayment => CalculateDaysSinceLastCouponPayment();
	public decimal AccruedInterest => CalculateAccruedInterest();
	public decimal Convexity => CalculateConvexity();

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
		decimal accruedInterest = CalculateAccruedInterest();
		return LastPrice + accruedInterest;
	}

	private decimal CalculateAccruedInterest()
	{
		var lastAndNext = LastAndRemainingCoupons().OrderBy(x => x.Date).Take(2).ToArray();
		return (FaceValue * (Coupon / 2)) * (AccruedDays / (lastAndNext[1].Date - lastAndNext[0].Date).Days);
	}

	public decimal CalculateConvexity()
	{
		double convexity = 0;
		var yieldDouble = Convert.ToDouble(RunningYield);
		var couponDouble = Convert.ToDouble(Coupon);
		var yearsDouble = Convert.ToDouble(Tenor);
		var faceDouble = Convert.ToDouble(FaceValue);

		for (int t = 1; t <= yearsDouble * 2; t++) // Semi-annual payments
		{
			double discountFactor = Math.Pow(1 + yieldDouble / 200, t);
			double cashFlow = (faceDouble * couponDouble / 2);
			convexity += (Convert.ToDouble(cashFlow) * t * (t + 1)) / (discountFactor * discountFactor);
		}
		convexity += (faceDouble * yearsDouble * (yearsDouble + 1)) / Math.Pow(1 + yieldDouble / 200, yearsDouble * 2);
		return Convert.ToDecimal(convexity / Math.Pow(1 + yieldDouble / 200, 2));
	}

	public override string ToString()
	{
		return $"{Name} ({Epic}) - {Coupon}% coupon, matures on {MaturityDate.ToShortDateString()}";
	}
}