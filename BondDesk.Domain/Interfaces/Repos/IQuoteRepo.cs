using BondDesk.Domain.Interfaces.Models;

namespace BondDesk.Domain.Interfaces.Repos;

public interface IQuoteRepo
{
	Task<IBondQuoteData> BondValuation(string symbol);
}