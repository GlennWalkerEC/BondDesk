namespace BondDesk.Domain.Interfaces.Models;

public interface IGiltInfo
{
    string? Epic { get; }
    string? Name { get; }
    decimal? Coupon { get; }
    DateTime? Maturity { get; }
}