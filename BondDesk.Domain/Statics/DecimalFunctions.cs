namespace BondDesk.Domain.Statics;

internal class DecimalFunctions
{
	public static decimal DecimalPow(decimal baseValue, decimal exponent)
	{
		return (decimal)Math.Pow((double)baseValue, (double)exponent);
	}
}
