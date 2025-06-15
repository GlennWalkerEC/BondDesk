using BondDesk.Api.Models;
using BondDesk.Domain.Entities;
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
			gilts.Add(new BondDTO
			{
				Coupon = bond.Coupon,
				MaturityDate = bond.MaturityDate,
				Epic = bond.Epic,
				DirtyPrice = bond.DirtyPrice,
				RunningYield = bond.RunningYield,
				AccruedInterest = bond.AccruedInterest,
				Convexity = bond.Convexity,
				ModifiedDuration = bond.ModifiedDuration,
				YieldToWorst = bond.YieldToWorst,
				TotalReturn = bond.TotalReturn
			});
		}
		return Ok(gilts);
	}
}
