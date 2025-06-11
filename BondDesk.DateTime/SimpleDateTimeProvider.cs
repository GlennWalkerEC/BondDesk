using BondDesk.Domain.Interfaces.Providers;

namespace BondDesk.DateTimeProvider;

public class SimpleDateTimeProvider : IDateTimeProvider
{
	public DateTime GetToday() => DateTime.Today;
}
