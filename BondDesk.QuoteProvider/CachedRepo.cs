using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Repos;
using Portfolio.QuoteProvider;

namespace BondDesk.QuoteProvider;

public class CachedRepo : IQuoteRepo
{
	protected readonly Dictionary<string, IBondQuoteData> _cache;
	protected readonly LseQuoteRepo _repo;

	public CachedRepo()
	{
		_cache = new Dictionary<string, IBondQuoteData>();
		_repo = new LseQuoteRepo();
	}

	public async Task<IBondQuoteData> BondValuation(string symbol)
	{
		if(_cache.ContainsKey(symbol)) return _cache[symbol];

		_cache[symbol] = await _repo.BondValuation(symbol);

		return _cache[symbol];
	}
}
