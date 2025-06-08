namespace BondDesk.Domain.Interfaces.Entities;

public interface IBondEntity
{
	decimal Coupon { get; }
	string Epic { get; }
	decimal FaceValue { get; }
	decimal LastPrice { get; }
	DateTime Maturity { get; }
	string Name { get; }
	decimal Tenor { get; }

	decimal RunningYield { get; }
	decimal YieldToMaturity { get; }

	string ToString();
}