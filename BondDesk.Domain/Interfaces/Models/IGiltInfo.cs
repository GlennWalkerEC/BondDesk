namespace BondDesk.Domain.Interfaces.Models;

public interface IGiltInfo
{
    decimal FaceValue => 100m;
    int CouponPeriodMonths => 6;

	string Epic { get; }
    string Name { get; }
    decimal Coupon { get; }
    DateTime MaturityDate { get; }
	public DateTime IssueDate { get; }
}