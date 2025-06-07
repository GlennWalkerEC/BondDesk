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

	/// <summary>
	/// Gets all gilts (UK government bonds).
	/// </summary>
	/// <returns>A list of Bond objects.</returns>
	[HttpGet]
	[SwaggerOperation(Summary = "Get all gilts", Description = "Returns a list of all UK government bonds (gilts).")]
	[ProducesResponseType(typeof(IEnumerable<Bond>), 200)]
	public async Task<ActionResult<IEnumerable<Bond>>> GetAllGilts()
	{
		var gilts = new List<Bond>();
		await foreach (var bond in _giltsService.GetGiltsAsync())
		{
			gilts.Add(bond);
		}
		return Ok(gilts);
	}
}
