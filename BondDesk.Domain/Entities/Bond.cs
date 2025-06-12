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
	public DateTime Maturity => _giltInfo.Maturity;
	public string Epic => _giltInfo.Epic ?? throw new InvalidOperationException("Epic cannot be null.");
	public decimal Tenor => (_giltInfo.Maturity - _dateTimeProvider.GetNow()).Days / 365m;

	public decimal DaysSinceLastCoupon => CalculateDaysSinceLastCoupon();
	public decimal LastPrice => GetValuation().LastPrice ?? throw new NullReferenceException(nameof(Epic));
	public decimal DirtyPrice => CalculateDirtyPrice();
	public decimal RunningYield => Coupon * FaceValue / LastPrice * FaceValue;
	
	public decimal YieldToMaturity => CalculateYieldToMaturity();
	//public decimal MacaulayDuration => CalculateMacaulayDuration();

	public IBondQuoteData GetValuation() => _quoteRepo.BondValuation(Epic).Result;

	protected decimal CalculateDaysSinceLastCoupon()
	{
		var today = _dateTimeProvider.GetNow();
		// Find the last coupon payment date before today
		DateTime lastCouponDate = _giltInfo.Maturity;

		if(today == lastCouponDate) return _giltInfo.CouponPeriodDays;

		while (lastCouponDate.AddMonths(-_giltInfo.CouponPeriodMonths) >= today)
		{
			lastCouponDate = lastCouponDate.AddMonths(-_giltInfo.CouponPeriodMonths);
		}

		// Calculate days since last coupon
		return Convert.ToDecimal(Math.Abs((today - lastCouponDate).Days));
	}

	protected decimal CalculateYieldToMaturity(int iterations = 100, decimal tolerance = 1e-6m)
	{
		decimal ytm = Coupon;
		for (int i = 0; i < iterations; i++)
		{
			decimal f = 0m, df = 0m;

			// Compute function value (Bond price equation) and its derivative  
			for (int t = 1; t <= Tenor; t++)
			{
				double discountFactor = Math.Pow((double)(1m + ytm), t);
				f += (FaceValue * Coupon) / (decimal)discountFactor;
				df += -(t * (FaceValue * Coupon)) / (decimal)Math.Pow((double)(1m + ytm), t + 1);
			}

			// Add present value of face value  
			f += FaceValue / (decimal)Math.Pow((double)(1m + ytm), (double)Tenor) - LastPrice;
			df += -Tenor * FaceValue / (decimal)Math.Pow((double)(1m + ytm), (double)(Tenor + 1));

			// Newton-Raphson iteration step  
			decimal newYtm = ytm - f / df;
			if (Math.Abs(newYtm - ytm) < tolerance)
				return newYtm;

			ytm = newYtm;
		}
		return ytm; // Return last iteration result if convergence isn't achieved  
	}

	public decimal CalculateYTM(out bool isApproximate)
    {
        decimal ytm = 0.045m; // Initial guess
        decimal tolerance = 1e-6m;
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
            f += FaceValue / (decimal)Math.Pow((double)(1 + ytm), Tenor) - LastPrice;
            df += -Tenor * FaceValue / (decimal)Math.Pow((double)(1 + ytm), Tenor + 1);

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

	protected decimal CalculateDirtyPrice()
	{
		decimal accruedInterest = (FaceValue * Coupon * CalculateDaysSinceLastCoupon()) / _giltInfo.CouponPeriodDays;
		return LastPrice + accruedInterest;
	}

	public override string ToString()
	{
		return $"{Name} ({Epic}) - {Coupon}% coupon, matures on {Maturity.ToShortDateString()}";
	}
}