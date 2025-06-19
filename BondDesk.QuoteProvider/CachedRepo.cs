using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Repos;
using Portfolio.QuoteProvider;

namespace BondDesk.QuoteProvider;

public class CachedRepo : IQuoteRepo
{
	// Store both the data and the time it was cached
	protected readonly Dictionary<string, (IBondQuoteData Data, DateTime CachedAt)> _cache;
	protected readonly LseQuoteRepo _repo;

	public CachedRepo()
	{
		_cache = new Dictionary<string, (IBondQuoteData, DateTime)>();
		_repo = new LseQuoteRepo();
	}

	public async Task<IBondQuoteData> BondValuation(string symbol)
	{
		if (_cache.TryGetValue(symbol, out var entry))	
		{
			// Expire after 20 seconds
			if ((DateTime.UtcNow - entry.CachedAt).TotalSeconds < 20)
			{
				return entry.Data;
			}
			else
			{
				_cache.Remove(symbol);
			}
		}

		var data = await _repo.BondValuation(symbol);
		_cache[symbol] = (data, DateTime.UtcNow);
		return data;
	}
}
