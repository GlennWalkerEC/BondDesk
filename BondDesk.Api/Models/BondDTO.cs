namespace BondDesk.Api.Models;

public class BondDTO
{
    public string Epic { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Coupon { get; set; }
    public DateTime Maturity { get; set; }
    public decimal FaceValue { get; set; }
    public decimal Tenor { get; set; } = default!;
    public decimal DaysSinceLastCoupon { get; set; }
    public decimal LastPrice { get; set; }
    public decimal DirtyPrice { get; set; }
    public decimal RunningYield { get; set; }
    public decimal YieldToMaturity { get; set; }
}