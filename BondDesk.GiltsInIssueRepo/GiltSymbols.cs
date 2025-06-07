using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.GiltsInIssueRepo.Models;
using System;
using System.Collections.Generic;

namespace BondDesk.GiltsInIssueRepo;

public class GiltSymbols : IGiltRepo
{
    public IEnumerable<IGiltInfo> GetAllGilts()
    {
        yield return new GiltInfo("T25", "Treasury 4½% 2025", 4.5m, new DateTime(2025, 9, 7));
        yield return new GiltInfo("TY25", "Treasury 0⅛% 2025", 0.125m, new DateTime(2025, 1, 31));
        yield return new GiltInfo("T26", "Treasury 1½% 2026", 1.5m, new DateTime(2026, 7, 22));
        yield return new GiltInfo("TG26", "Treasury 0⅝% 2026", 0.625m, new DateTime(2026, 10, 22));
        yield return new GiltInfo("T26A", "Treasury 2% 2026", 2.0m, new DateTime(2026, 9, 7));
        yield return new GiltInfo("T27A", "Treasury 1% 2027", 1.0m, new DateTime(2027, 4, 22));
        yield return new GiltInfo("TS27", "Treasury 0½% 2027", 0.5m, new DateTime(2027, 10, 22));
        yield return new GiltInfo("TG27", "Treasury 0⅛% 2027", 0.125m, new DateTime(2027, 1, 31));
        yield return new GiltInfo("TR27", "Treasury 2% 2027", 2.0m, new DateTime(2027, 9, 7));
        yield return new GiltInfo("TN28", "Treasury 0⅝% 2028", 0.625m, new DateTime(2028, 10, 22));
        yield return new GiltInfo("TE28", "Treasury 0⅛% 2028", 0.125m, new DateTime(2028, 1, 31));
        yield return new GiltInfo("TS28", "Treasury 0½% 2028", 0.5m, new DateTime(2028, 7, 22));
        yield return new GiltInfo("TG28", "Treasury 0⅝% 2028", 0.625m, new DateTime(2028, 10, 22));
        yield return new GiltInfo("TR28", "Treasury 2% 2028", 2.0m, new DateTime(2028, 9, 7));
        yield return new GiltInfo("TG29", "Treasury 0⅝% 2029", 0.625m, new DateTime(2029, 10, 22));
        yield return new GiltInfo("TS29", "Treasury 0½% 2029", 0.5m, new DateTime(2029, 7, 22));
        yield return new GiltInfo("TR29", "Treasury 2% 2029", 2.0m, new DateTime(2029, 9, 7));
        yield return new GiltInfo("T30", "Treasury 1½% 2030", 1.5m, new DateTime(2030, 7, 22));
        yield return new GiltInfo("TG30", "Treasury 0⅝% 2030", 0.625m, new DateTime(2030, 10, 22));
        yield return new GiltInfo("TR30", "Treasury 2% 2030", 2.0m, new DateTime(2030, 9, 7));
        yield return new GiltInfo("TG31", "Treasury 0⅝% 2031", 0.625m, new DateTime(2031, 10, 22));
        yield return new GiltInfo("T31", "Treasury 1½% 2031", 1.5m, new DateTime(2031, 7, 22));
        yield return new GiltInfo("TG32", "Treasury 0⅝% 2032", 0.625m, new DateTime(2032, 10, 22));
        yield return new GiltInfo("TR32", "Treasury 2% 2032", 2.0m, new DateTime(2032, 9, 7));
        yield return new GiltInfo("TR33", "Treasury 2% 2033", 2.0m, new DateTime(2033, 9, 7));
        yield return new GiltInfo("TG33", "Treasury 0⅝% 2033", 0.625m, new DateTime(2033, 10, 22));
        yield return new GiltInfo("T34", "Treasury 1½% 2034", 1.5m, new DateTime(2034, 7, 22));
        yield return new GiltInfo("TS34", "Treasury 0½% 2034", 0.5m, new DateTime(2034, 7, 22));
        yield return new GiltInfo("TR34", "Treasury 2% 2034", 2.0m, new DateTime(2034, 9, 7));
        yield return new GiltInfo("T35", "Treasury 1½% 2035", 1.5m, new DateTime(2035, 7, 22));
        yield return new GiltInfo("TG35", "Treasury 0⅝% 2035", 0.625m, new DateTime(2035, 10, 22));
        yield return new GiltInfo("T4Q", "Treasury 4¼% 2036", 4.25m, new DateTime(2036, 6, 7));
        yield return new GiltInfo("TG37", "Treasury 0⅝% 2037", 0.625m, new DateTime(2037, 10, 22));
        yield return new GiltInfo("TG38", "Treasury 0⅝% 2038", 0.625m, new DateTime(2038, 10, 22));
        yield return new GiltInfo("TR38", "Treasury 2% 2038", 2.0m, new DateTime(2038, 9, 7));
        yield return new GiltInfo("TR39", "Treasury 2% 2039", 2.0m, new DateTime(2039, 9, 7));
        yield return new GiltInfo("T39", "Treasury 1½% 2039", 1.5m, new DateTime(2039, 7, 22));
        yield return new GiltInfo("TG40", "Treasury 0⅝% 2040", 0.625m, new DateTime(2040, 10, 22));
        yield return new GiltInfo("T40", "Treasury 1½% 2040", 1.5m, new DateTime(2040, 7, 22));
        yield return new GiltInfo("TG41", "Treasury 0⅝% 2041", 0.625m, new DateTime(2041, 10, 22));
        yield return new GiltInfo("T42", "Treasury 1½% 2042", 1.5m, new DateTime(2042, 7, 22));
        yield return new GiltInfo("TR43", "Treasury 2% 2043", 2.0m, new DateTime(2043, 9, 7));
        yield return new GiltInfo("TG44", "Treasury 0⅝% 2044", 0.625m, new DateTime(2044, 10, 22));
        yield return new GiltInfo("T45", "Treasury 1½% 2045", 1.5m, new DateTime(2045, 7, 22));
        yield return new GiltInfo("TG46", "Treasury 0⅝% 2046", 0.625m, new DateTime(2046, 10, 22));
        yield return new GiltInfo("T46", "Treasury 1½% 2046", 1.5m, new DateTime(2046, 7, 22));
        yield return new GiltInfo("TG47", "Treasury 0⅝% 2047", 0.625m, new DateTime(2047, 10, 22));
        yield return new GiltInfo("TG49", "Treasury 0⅝% 2049", 0.625m, new DateTime(2049, 10, 22));
        yield return new GiltInfo("T49", "Treasury 1½% 2049", 1.5m, new DateTime(2049, 7, 22));
        yield return new GiltInfo("TG50", "Treasury 0⅝% 2050", 0.625m, new DateTime(2050, 10, 22));
        yield return new GiltInfo("T51A", "Treasury 1½% 2051", 1.5m, new DateTime(2051, 7, 22));
        yield return new GiltInfo("T52", "Treasury 1½% 2052", 1.5m, new DateTime(2052, 7, 22));
        yield return new GiltInfo("TG53", "Treasury 0⅝% 2053", 0.625m, new DateTime(2053, 10, 22));
        yield return new GiltInfo("T53", "Treasury 1½% 2053", 1.5m, new DateTime(2053, 7, 22));
        yield return new GiltInfo("T54", "Treasury 1½% 2054", 1.5m, new DateTime(2054, 7, 22));
        yield return new GiltInfo("TR54", "Treasury 2% 2054", 2.0m, new DateTime(2054, 9, 7));
        yield return new GiltInfo("TR4Q", "Treasury 4¼% 2055", 4.25m, new DateTime(2055, 6, 7));
        yield return new GiltInfo("TG57", "Treasury 0⅝% 2057", 0.625m, new DateTime(2057, 10, 22));
        yield return new GiltInfo("TR60", "Treasury 2% 2060", 2.0m, new DateTime(2060, 9, 7));
        yield return new GiltInfo("TG61", "Treasury 0⅝% 2061", 0.625m, new DateTime(2061, 10, 22));
        yield return new GiltInfo("TR63", "Treasury 2% 2063", 2.0m, new DateTime(2063, 9, 7));
        yield return new GiltInfo("TG65", "Treasury 0⅝% 2065", 0.625m, new DateTime(2065, 10, 22));
        yield return new GiltInfo("TR68", "Treasury 2% 2068", 2.0m, new DateTime(2068, 9, 7));
        yield return new GiltInfo("TG71", "Treasury 0⅝% 2071", 0.625m, new DateTime(2071, 10, 22));
        yield return new GiltInfo("TR73", "Treasury 2% 2073", 2.0m, new DateTime(2073, 9, 7));
    }
}