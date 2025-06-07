using BondDesk.Domain.Interfaces;

namespace BondDesk.GiltsInIssueRepo;

public class GiltSymbols : IBondSymbolRepo
{
	public IEnumerable<string> GetAllSymbols()
	{
		yield return "T25";
		yield return "TY25";
		yield return "T26";
		yield return "TG26";
		yield return "T26A";
		yield return "T27A";
		yield return "TS27";
		yield return "TG27";
		yield return "TR27";
		yield return "TN28";
		yield return "TE28";
		yield return "TS28";
		yield return "TG28";
		yield return "TR28";
		yield return "TG29";
		yield return "TS29";
		yield return "TR29";
		yield return "T30";
		yield return "TG30";
		yield return "TR30";
		yield return "TG31";
		yield return "T31";
		yield return "TG32";
		yield return "TR32";
		yield return "TR33";
		yield return "TG33";
		yield return "T34";
		yield return "TS34";
		yield return "TR34";
		yield return "T35";
		yield return "TG35";
		yield return "T4Q";
		yield return "TG37";
		yield return "TG38";
		yield return "TR38";
		yield return "TR39";
		yield return "T39";
		yield return "TG40";
		yield return "T40";
		yield return "TG41";
		yield return "T42";
		yield return "TR43";
		yield return "TG44";
		yield return "T45";
		yield return "TG46";
		yield return "T46";
		yield return "TG47";
		yield return "TG49";
		yield return "T49";
		yield return "TG50";
		yield return "T51A";
		yield return "T52";
		yield return "TG53";
		yield return "T53";
		yield return "T54";
		yield return "TR54";
		yield return "TR4Q";
		yield return "TG57";
		yield return "TR60";
		yield return "TG61";
		yield return "TR63";
		yield return "TG65";
		yield return "TR68";
		yield return "TG71";
		yield return "TR73";
	}
}
