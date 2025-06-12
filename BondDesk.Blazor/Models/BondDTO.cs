namespace BondDesk.Blazor.Models;

public class BondDTO
{
	public required string Name { get; set; }
	public decimal Coupon { get; set; }
	public DateTime MaturityDate { get; set; }
	public required string Epic { get; set; }
	public decimal DirtyPrice { get; set; }
	public decimal RunningYield { get; set; }
	public decimal AccruedInterest { get; set; }
	public decimal Convexity { get; set; }
	public decimal ModifiedDuration { get; set; }
	public decimal YieldToWorst { get; set; }
}
