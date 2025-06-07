using BondDesk.Domain.Interfaces.Models;

namespace Portfolio.QuoteProvider.Models;

internal class BondQuoteData : IBondQuoteData
{
    private readonly IBondQuoteData _innerBond;

    public BondQuoteData(IBondQuoteData bond)
    {
        _innerBond = bond;
    }

    public string? AssetClass => _innerBond.AssetClass;
    public decimal? Bid => _innerBond.Bid;
    public decimal? BidQty => _innerBond.BidQty;
    public decimal? Change => _innerBond.Change;
    public decimal? ChangePercent => _innerBond.ChangePercent;
    public decimal? Close => _innerBond.Close;
    public string? Currency => _innerBond.Currency;
    public string? Epic => _innerBond.Epic;
    public decimal? High => _innerBond.High;
    public string? Isin => _innerBond.Isin;
    public decimal? LastPrice => _innerBond.LastPrice;
    public string? LastTraded => _innerBond.LastTraded;
    public decimal? Low => _innerBond.Low;
    public string? Market => _innerBond.Market;
    public decimal? Mid => _innerBond.Mid;
    public decimal? Offer => _innerBond.Offer;
    public decimal? OfferQty => _innerBond.OfferQty;
    public decimal? Open => _innerBond.Open;
    public string? Sector => _innerBond.Sector;
    public string? SecurityType => _innerBond.SecurityType;
    public string? Symbol => _innerBond.Symbol;
    public int? TradeCount => _innerBond.TradeCount;
    public decimal? TradeSize => _innerBond.TradeSize;
    public decimal? Volume => _innerBond.Volume;
}