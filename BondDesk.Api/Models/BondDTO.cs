namespace BondDesk.Api.Models;

public class BondDTO
{
	public decimal Coupon { get; set; }
	public DateTime MaturityDate { get; set; }
	public required string Epic { get; set; }
	public decimal DirtyPrice { get; set; }
	public decimal CurrentYield { get; set; }
	public decimal AccruedInterest { get; set; }
	public decimal Convexity { get; set; }
	public decimal ModifiedDuration { get; set; }
	public decimal PresentValueOverDirty { get; set; }
	public decimal YieldToMaturity { get; set; }
	public decimal? MarketSize { get; set; }
	public decimal DV1KQ { get; set; }
}