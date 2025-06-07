using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BondDesk.Api.Controllers;
public class BondController : Controller
{
	public ActionResult Index()
	{
		return View();
	}

	public ActionResult Create()
	{
		return View();
	}
}
