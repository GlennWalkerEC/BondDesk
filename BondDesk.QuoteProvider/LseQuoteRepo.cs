using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Repos;
using Portfolio.QuoteProvider.Models;
using System.Diagnostics;

namespace Portfolio.QuoteProvider;

public class LseQuoteRepo : IQuoteRepo
{
	private static HttpClient? client;

	public LseQuoteRepo()
	{
		client ??= new HttpClient();
	}

	public async Task<IBondQuoteData> BondValuation(string symbol)
	{
		try
		{
			Debug.WriteLine($"Fetching LSE: {symbol}");
			var response = await client!.GetStringAsync($"https://api.londonstockexchange.com/api/gw/lse/instruments/alldata/{symbol}");

			var refinitive = System.Text.Json.JsonSerializer.Deserialize<Refinitive>(response);

			Debug.WriteLine($"Success LSE: {symbol} = {refinitive.LastPrice / 100}");
			return refinitive;
		}
		catch(Exception ex)
		{
			Console.WriteLine($"{symbol} = !SKIPPED!");
			throw new Exception($"Failed to fetch data for symbol: {symbol}", ex);
		}
	}
}
