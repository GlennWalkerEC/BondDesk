namespace BondDesk.Domain.Entities;

public record Coupon(DateTime Date, decimal Rate) {

	public DateTime PaymentDate
	{
		get
		{
			var weekDay = Date;
			while (weekDay.DayOfWeek == DayOfWeek.Saturday || weekDay.DayOfWeek == DayOfWeek.Sunday)
			{
				weekDay = weekDay.AddDays(1);
			}
			return weekDay;
		}
	}

}
