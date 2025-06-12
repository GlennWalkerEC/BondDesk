using BondDesk.Domain.Interfaces.Entities;

namespace BondDesk.Domain.Interfaces.Services;
public interface IGiltsService
{
	IAsyncEnumerable<IBondEntity> GetGiltsAsync();
}
