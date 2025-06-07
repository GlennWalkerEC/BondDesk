using BondDesk.Domain.Interfaces.Models;

namespace Portfolio.Domain.Interfaces;

public interface IQuoteRepo
{
	Task<IBondData> BondValuation(string symbol);
}