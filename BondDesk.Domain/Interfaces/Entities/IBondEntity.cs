namespace BondDesk.Domain.Interfaces.Entities;

public interface IBondEntity
{
	decimal Coupon { get; }
	string Epic { get; }
	decimal FaceValue { get; }
	decimal LastPrice { get; }
	DateTime MaturityDate { get; }
	string Name { get; }
	decimal Tenor { get; }
	decimal RunningYield { get; }
	decimal DaysSinceLastCoupon { get; }
	decimal DirtyPrice { get; }
	decimal AccruedDays { get; }

	string ToString();
}