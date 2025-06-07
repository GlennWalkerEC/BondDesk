using BondDesk.Domain.Interfaces.Models;
using System.Text.Json.Serialization;

namespace Portfolio.QuoteProvider.Models;

internal class Refinitive : IBondData
{
    [JsonPropertyName("assetclass")]
    public string? AssetClass { get; set; }

    [JsonPropertyName("bid")]
    public decimal? Bid { get; set; }

    [JsonPropertyName("bidqty")]
    public decimal? BidQty { get; set; }

    [JsonPropertyName("change")]
    public decimal? Change { get; set; }

    [JsonPropertyName("change_percent")]
    public decimal? ChangePercent { get; set; }

    [JsonPropertyName("close")]
    public decimal? Close { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("epic")]
    public string? Epic { get; set; }

    [JsonPropertyName("high")]
    public decimal? High { get; set; }

    [JsonPropertyName("isin")]
    public string? Isin { get; set; }

    [JsonPropertyName("lastprice")]
    public decimal? LastPrice { get; set; }

    [JsonPropertyName("lasttraded")]
    public string? LastTraded { get; set; }

    [JsonPropertyName("low")]
    public decimal? Low { get; set; }

    [JsonPropertyName("market")]
    public string? Market { get; set; }

    [JsonPropertyName("mid")]
    public decimal? Mid { get; set; }

    [JsonPropertyName("offer")]
    public decimal? Offer { get; set; }

    [JsonPropertyName("offerqty")]
    public decimal? OfferQty { get; set; }

    [JsonPropertyName("open")]
    public decimal? Open { get; set; }

    [JsonPropertyName("sector")]
    public string? Sector { get; set; }

    [JsonPropertyName("securitytype")]
    public string? SecurityType { get; set; }

    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("tradecount")]
    public int? TradeCount { get; set; }

    [JsonPropertyName("tradesize")]
    public decimal? TradeSize { get; set; }

    [JsonPropertyName("volume")]
    public decimal? Volume { get; set; }
}
