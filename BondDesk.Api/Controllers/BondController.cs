using BondDesk.Domain.Entities;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
//using BondDesk.Api.Models;

namespace BondDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BondController : ControllerBase
{
	private readonly IGiltsService _giltsService;

	public BondController(IGiltsService giltsService)
	{
		_giltsService = giltsService ?? throw new NullReferenceException(nameof(giltsService));
	}

	/// <summary>
	/// Gets all gilts (UK government bonds).
	/// </summary>
	/// <returns>A list of BondDTO objects.</returns>
	[HttpGet]
	[SwaggerOperation(Summary = "Get all gilts", Description = "Returns a list of all UK government bonds (gilts).")]
	[ProducesResponseType(typeof(IEnumerable<Bond>), 200)]
	public async Task<ActionResult<IEnumerable<Bond>>> GetAllGilts()
	{
		var gilts = new List<Bond>();
		await foreach (var bond in _giltsService.GetGiltsAsync())
		{
			gilts.Add(bond);
			//gilts.Add(new Bond
			//{
			//	Epic = bond.Epic,
			//	Name = bond.Name,
			//	Coupon = bond.Coupon,
			//	Maturity = bond.Maturity,
			//	FaceValue = bond.FaceValue,
			//	Tenor = bond.Tenor,
			//	DaysSinceLastCoupon = bond.DaysSinceLastCoupon,
			//	LastPrice = bond.LastPrice,
			//	DirtyPrice = bond.DirtyPrice,
			//	RunningYield = bond.RunningYield,
			//	YieldToMaturity = bond.YieldToMaturity
			//});
		}
		return Ok(gilts);
	}
}
