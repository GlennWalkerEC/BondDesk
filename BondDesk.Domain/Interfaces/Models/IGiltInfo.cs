namespace BondDesk.Domain.Interfaces.Models;

public interface IGiltInfo
{
    decimal FaceValue => 100m;
    int CouponPeriodMonths => 6;
    decimal CouponPeriodDays => 182.5m;

	string Epic { get; }
    string Name { get; }
    decimal Coupon { get; }
    DateTime Maturity { get; }
}