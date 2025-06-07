using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Repos;

namespace BondDesk.Domain.Entities;

public class Bond : IGiltInfo
{
	private readonly IQuoteRepo _quoteRepo;
	private readonly IGiltInfo _giltInfo;

	public Bond(IQuoteRepo quoteRepo, IGiltInfo giltInfo)
	{
		_quoteRepo = quoteRepo ?? throw new ArgumentNullException(nameof(quoteRepo), "Quote repository cannot be null.");
		_giltInfo = giltInfo ?? throw new ArgumentNullException(nameof(giltInfo), "Gilt information cannot be null.");
	}

	public string? Name => _giltInfo.Name;
	public decimal? Coupon => _giltInfo.Coupon;
	public DateTime? Maturity => _giltInfo.Maturity;
	public string Epic => _giltInfo.Epic ?? throw new InvalidOperationException("Epic cannot be null.");

	public async Task<IBondQuoteData> GetQuoteAsync()
	{
		return await _quoteRepo.BondValuation(Epic);
	}

	public async Task<string?> GetAssetClassAsync()
	{
		return (await GetQuoteAsync()).AssetClass;
	}
		
	public async Task<decimal?> GetBidAsync()
	{
		return (await GetQuoteAsync()).Bid;
	}

	public async Task<decimal?> GetBidQtyAsync()
	{
		return (await GetQuoteAsync()).BidQty;
	}

	public async Task<decimal?> GetChangeAsync()
	{
		return (await GetQuoteAsync()).Change;
	}

	public async Task<decimal?> GetChangePercentAsync()
	{
		return (await GetQuoteAsync()).ChangePercent;
	}

	public async Task<decimal?> GetCloseAsync()
	{
		return (await GetQuoteAsync()).Close;
	}

	public async Task<string?> GetCurrencyAsync()
	{
		return (await GetQuoteAsync()).Currency;
	}

	public async Task<string?> GetEpicAsync()
	{
		return (await GetQuoteAsync()).Epic;
	}

	public async Task<decimal?> GetHighAsync()
	{
		return (await GetQuoteAsync()).High;
	}

	public async Task<string?> GetIsinAsync()
	{
		return (await GetQuoteAsync()).Isin;
	}

	public async Task<decimal?> GetLastPriceAsync()
	{
		return (await GetQuoteAsync()).LastPrice;
	}

	public async Task<string?> GetLastTradedAsync()
	{
		return (await GetQuoteAsync()).LastTraded;
	}

	public async Task<decimal?> GetLowAsync()
	{
		return (await GetQuoteAsync()).Low;
	}

	public async Task<string?> GetMarketAsync()
	{
		return (await GetQuoteAsync()).Market;
	}

	public async Task<decimal?> GetMidAsync()
	{
		return (await GetQuoteAsync()).Mid;
	}

	public async Task<decimal?> GetOfferAsync()
	{
		return (await GetQuoteAsync()).Offer;
	}

	public async Task<decimal?> GetOfferQtyAsync()
	{
		return (await GetQuoteAsync()).OfferQty;
	}

	public async Task<decimal?> GetOpenAsync()
	{
		return (await GetQuoteAsync()).Open;
	}

	public async Task<string?> GetSectorAsync()
	{
		return (await GetQuoteAsync()).Sector;
	}

	public async Task<string?> GetSecurityTypeAsync()
	{
		return (await GetQuoteAsync()).SecurityType;
	}

	public async Task<string?> GetSymbolAsync()
	{
		return (await GetQuoteAsync()).Symbol;
	}

	public async Task<int?> GetTradeCountAsync()
	{
		return (await GetQuoteAsync()).TradeCount;
	}

	public async Task<decimal?> GetTradeSizeAsync()
	{
		return (await GetQuoteAsync()).TradeSize;
	}

	public async Task<decimal?> GetVolumeAsync()
	{
		return (await GetQuoteAsync()).Volume;
	}

	public override string ToString()
	{
		return $"{Name} ({Epic}) - {Coupon}% coupon, matures on {Maturity?.ToShortDateString()}";
	}
}