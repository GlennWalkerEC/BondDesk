using BondDesk.Api.Models;
using BondDesk.Domain.Entities;
using BondDesk.Domain.Interfaces.Entities;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

	[HttpGet]
	[SwaggerOperation(Summary = "Get all gilts", Description = "Returns a list of all UK government bonds (gilts).")]
	[ProducesResponseType(typeof(IEnumerable<BondDTO>), 200)]
	public async Task<ActionResult<IEnumerable<BondDTO>>> GetAllGilts()
	{
		var gilts = new List<BondDTO>();
		await foreach (var bond in _giltsService.GetGiltsAsync())
		{
			gilts.Add(MapToDTO(bond));
		}
		return Ok(gilts);
	}

	private static BondDTO MapToDTO(IBondEntity bond)
	{
		return new BondDTO
		{
			Coupon = bond.Coupon * 100,
			MaturityDate = bond.MaturityDate,
			Epic = bond.Epic,
			DirtyPrice = bond.DirtyPrice,
			CurrentYield = bond.CurrentYield * 100,
			AccruedInterest = bond.AccruedInterest,
			Convexity = bond.Convexity,
			ModifiedDuration = bond.ModifiedDuration,
			PresentValueOverDirty = bond.PresentValueOverDirty,
			YieldToMaturity = bond.YieldToMaturity * 100,
			MarketSize = bond.MarketSize
		};
	}
}
