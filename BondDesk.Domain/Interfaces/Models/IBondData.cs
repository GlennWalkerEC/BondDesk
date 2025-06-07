namespace BondDesk.Domain.Interfaces.Models;

public interface IBondData
{
    string? AssetClass { get; }
    decimal? Bid { get; }
    decimal? BidQty { get; }
    decimal? Change { get; }
    decimal? ChangePercent { get; }
    decimal? Close { get; }
    string? Currency { get; }
    string? Epic { get; }
    decimal? High { get; }
    string? Isin { get; }
    decimal? LastPrice { get; }
    string? LastTraded { get; }
    decimal? Low { get; }
    string? Market { get; }
    decimal? Mid { get; }
    decimal? Offer { get; }
    decimal? OfferQty { get; }
    decimal? Open { get; }
    string? Sector { get; }
    string? SecurityType { get; }
    string? Symbol { get; }
    int? TradeCount { get; }
    decimal? TradeSize { get; }
    decimal? Volume { get; }
}
