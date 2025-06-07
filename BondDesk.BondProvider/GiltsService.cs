using BondDesk.Domain.Interfaces;
using Portfolio.Domain.Interfaces;
using Portfolio.QuoteProvider.Models;

namespace BondDesk.BondProvider;

public class GiltsService
{
	protected readonly IQuoteRepo _lseRepo;
	protected readonly IBondSymbolRepo _symbolRepo;

	public GiltsService(IQuoteRepo lseRepo, IBondSymbolRepo symbolRepo)
	{
		_lseRepo = lseRepo ?? throw new ArgumentNullException(nameof(lseRepo));
		_symbolRepo = symbolRepo ?? throw new ArgumentNullException(nameof(symbolRepo));
	}

	public async IAsyncEnumerable<Bond> GetGiltsAsync()
	{
		foreach (var symbol in _symbolRepo.GetAllSymbols())
		{
			var valuation = await _lseRepo.BondValuation(symbol);
			if (valuation == null)
			{
				continue;
			}
			yield return new Bond(valuation);
		}
	}
}
