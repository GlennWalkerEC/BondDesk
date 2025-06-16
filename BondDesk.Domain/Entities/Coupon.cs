using BondDesk.Domain.Interfaces.Providers;
using BondDesk.Domain.Statics;

namespace BondDesk.Domain.Entities;

public record Coupon(DateTime Date, decimal Principal, decimal Rate) {

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

	public decimal CalculatePresentValue(IDateTimeProvider dateTimeProvider, decimal discountRate)
	{
		var futureValue = Principal * Rate;

		var years = Convert.ToDecimal((PaymentDate - dateTimeProvider.GetToday()).TotalDays / 365.2425);		

		var compoundingPerYear = 1; // Annual compounding

		decimal ratePerPeriod = discountRate / compoundingPerYear;
		decimal totalPeriods = years * compoundingPerYear;
		decimal factor = DecimalFunctions.DecimalPow(1 + ratePerPeriod, totalPeriods);

		return futureValue / factor;
	}
}
