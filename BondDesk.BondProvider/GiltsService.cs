using BondDesk.Domain.Entities;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Interfaces.Services;

namespace BondDesk.BondProvider;

public class GiltsService : IGiltsService
{
	protected readonly IQuoteRepo _lseRepo;
	protected readonly IGiltRepo _giltRepo;

	public GiltsService(IQuoteRepo lseRepo, IGiltRepo giltRepo)
	{
		_lseRepo = lseRepo ?? throw new ArgumentNullException(nameof(lseRepo));
		_giltRepo = giltRepo ?? throw new ArgumentNullException(nameof(giltRepo));
	}

	public async IAsyncEnumerable<Bond> GetGiltsAsync()
	{
		foreach (var giltInfo in _giltRepo.GetAllGilts())
		{
			yield return new Bond(_lseRepo, giltInfo);
		}
	}
}
