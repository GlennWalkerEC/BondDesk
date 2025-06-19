using BondDesk.Domain.Interfaces.Models;

namespace BondDesk.Domain.Interfaces.Repos;
public interface IGiltRepo
{
	IAsyncEnumerable<IGiltInfo> GetAllGiltsAsync();
}
