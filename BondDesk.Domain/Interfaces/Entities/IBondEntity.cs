namespace BondDesk.Domain.Interfaces.Entities;

public interface IBondEntity
{
    string Name { get; }
    decimal Coupon { get; }
    DateTime MaturityDate { get; }
    string Epic { get; }
    decimal DirtyPrice { get; }
    decimal RunningYield { get; }
    decimal AccruedInterest { get; }
    decimal Convexity { get; }
    decimal ModifiedDuration { get; }
    decimal YieldToWorst { get; }
}