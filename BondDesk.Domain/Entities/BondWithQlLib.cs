using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Providers;
using BondDesk.Domain.Interfaces.Repos;
using QLNet;
using static QLNet.Utils;


namespace BondDesk.Domain.Entities;
public class BondWithQlLib : Bond
{
	public BondWithQlLib(IQuoteRepo quoteRepo, IGiltInfo giltInfo, IDateTimeProvider dateTimeProvider)
		: base(quoteRepo, giltInfo, dateTimeProvider)
	{
		//// Create a schedule for coupon payments
		//Schedule schedule = new Schedule(issueDate, maturityDate, new Period(Frequency.Semiannual),
		//	new UnitedKingdom(UnitedKingdom.Market.GovernmentBond), BusinessDayConvention.Following,
		//	BusinessDayConvention.Following, DateGeneration.Rule.Backward, false);

		//// Define the bond
		//FixedRateBond gilt = new FixedRateBond(settlementDays, faceValue, schedule,
		//	new List<double> { couponRate }, new ActualActual(ActualActual.Convention.ISDA));

		//// Set up yield curve
		//Handle<YieldTermStructure> discountCurve = new Handle<YieldTermStructure>(
		//	new FlatForward(settlementDays, new QuoteHandle(new SimpleQuote(0.05)), new ActualActual(ActualActual.Convention.ISDA)));

		//// Pricing engine
		//IPricingEngine bondEngine = new DiscountingBondEngine(discountCurve);
		//gilt.setPricingEngine(bondEngine);

	}


}
