using BondDesk.Domain.Entities;
using BondDesk.Domain.Interfaces.Entities;
using BondDesk.Domain.Interfaces.Providers;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Interfaces.Services;

namespace BondDesk.BondProvider;

public class GiltsService : IGiltsService
{
	protected readonly IQuoteRepo _lseRepo;
	protected readonly IGiltRepo _giltRepo;
	protected readonly IDateTimeProvider _dateTimeProvider;

	protected static decimal AssumedReinvestmentRate = 0.02832m; // TODO: Move to Config

	public GiltsService(IQuoteRepo lseRepo, IGiltRepo giltRepo, IDateTimeProvider dateTimeProvider)
	{
		_lseRepo = lseRepo ?? throw new ArgumentNullException(nameof(lseRepo));
		_giltRepo = giltRepo ?? throw new ArgumentNullException(nameof(giltRepo));
		_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(_dateTimeProvider));
	}

	public async IAsyncEnumerable<IBondEntity> GetGiltsAsync()
	{
		foreach (var giltInfo in _giltRepo.GetAllGilts())
		{
			var bond = new Bond(_lseRepo, giltInfo, _dateTimeProvider, AssumedReinvestmentRate);
			Task.Run(bond.GetValuation); // Eagerly get valuation
			yield return bond;
		}
	}
}
