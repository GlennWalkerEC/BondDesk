using BondDesk.Domain.Interfaces.Entities;

namespace BondDesk.Domain.Interfaces.Services;
public interface IGiltsService
{
	public decimal AssumedReinvestmentRate { get; }
	IAsyncEnumerable<IBondEntity> GetGiltsAsync();
}
