using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.GiltsInIssueRepo.Models;
using System;
using System.Collections.Generic;

namespace BondDesk.GiltsInIssueRepo;

public class Gilts : IGiltRepo
{
    public IEnumerable<IGiltInfo> GetAllGilts()
    {
        yield return new GiltInfo("T25", "2% Treasury Gilt 2025", 2.00m, new DateTime(2025, 9, 7));
        yield return new GiltInfo("TY25", "3½% Treasury Gilt 2025", 3.50m, new DateTime(2025, 10, 22));
        yield return new GiltInfo("T26", "0 1/8% Treasury Gilt 2026", 0.125m, new DateTime(2026, 1, 30));
        yield return new GiltInfo("TG26", "1½% Treasury Gilt 2026", 1.50m, new DateTime(2026, 7, 22));
        yield return new GiltInfo("T26A", "0 3/8% Treasury Gilt 2026", 0.375m, new DateTime(2026, 10, 22));
        yield return new GiltInfo("T27A", "4 1/8% Treasury Gilt 2027", 4.125m, new DateTime(2027, 1, 29));
        yield return new GiltInfo("TS27", "3¾% Treasury Gilt 2027", 3.75m, new DateTime(2027, 3, 7));
        yield return new GiltInfo("TG27", "1¼% Treasury Gilt 2027", 1.25m, new DateTime(2027, 7, 22));
        yield return new GiltInfo("TR27", "4¼% Treasury Gilt 2027", 4.25m, new DateTime(2027, 12, 7));
        yield return new GiltInfo("TN28", "0 1/8% Treasury Gilt 2028", 0.125m, new DateTime(2028, 1, 31));
        yield return new GiltInfo("TE28", "4 3/8% Treasury Gilt 2028", 4.375m, new DateTime(2028, 3, 7));
        yield return new GiltInfo("TS28", "4½% Treasury Gilt 2028", 4.50m, new DateTime(2028, 6, 7));
        yield return new GiltInfo("TG28", "1 5/8% Treasury Gilt 2028", 1.625m, new DateTime(2028, 10, 22));
        yield return new GiltInfo("TR28", "6% Treasury Stock 2028", 6.00m, new DateTime(2028, 12, 7));
        yield return new GiltInfo("TG29", "0½% Treasury Gilt 2029", 0.50m, new DateTime(2029, 1, 31));
        yield return new GiltInfo("TS29", "4 1/8% Treasury Gilt 2029", 4.12m, new DateTime(2029, 7, 22));
        yield return new GiltInfo("TR29", "0 7/8% Treasury Gilt 2029", 0.875m, new DateTime(2029, 10, 22));
        yield return new GiltInfo("T30", "4 3/8% Treasury Gilt 2030", 4.375m, new DateTime(2030, 3, 7));
        yield return new GiltInfo("TG30", "0 3/8% Treasury Gilt 2030", 0.375m, new DateTime(2030, 10, 22));
        yield return new GiltInfo("TR30", "4¾% Treasury Gilt 2030", 4.75m, new DateTime(2030, 12, 7));
        yield return new GiltInfo("TG31", "0¼% Treasury Gilt 2031", 0.25m, new DateTime(2031, 7, 31));
        yield return new GiltInfo("T31", "4% Treasury Gilt 2031", 4.00m, new DateTime(2031, 10, 22));
        yield return new GiltInfo("TG32", "1% Treasury Gilt 2032", 1.00m, new DateTime(2032, 1, 31));
        yield return new GiltInfo("TR32", "4¼% Treasury Stock 2032", 4.25m, new DateTime(2032, 6, 7));
        yield return new GiltInfo("TR33", "3¼% Treasury Gilt 2033", 3.25m, new DateTime(2033, 1, 31));
        yield return new GiltInfo("TG33", "0 7/8% Green Gilt 2033", 0.875m, new DateTime(2033, 7, 31));
        yield return new GiltInfo("T34", "4 5/8% Treasury Gilt 2034", 4.625m, new DateTime(2034, 1, 31));
        yield return new GiltInfo("TS34", "4¼% Treasury Gilt 2034", 4.25m, new DateTime(2034, 7, 31));
        yield return new GiltInfo("TR34", "4½% Treasury Gilt 2034", 4.50m, new DateTime(2034, 9, 7));
        yield return new GiltInfo("T35", "4½% Treasury Gilt 2035", 4.50m, new DateTime(2035, 3, 7));
        yield return new GiltInfo("TG35", "0 5/8% Treasury Gilt 2035", 0.625m, new DateTime(2035, 7, 31));
        yield return new GiltInfo("T4Q", "4¼% Treasury Stock 2036", 4.25m, new DateTime(2036, 3, 7));
        yield return new GiltInfo("TG37", "1¾% Treasury Gilt 2037", 1.75m, new DateTime(2037, 9, 7));
        yield return new GiltInfo("TG38", "3¾% Treasury Gilt 2038", 3.75m, new DateTime(2038, 1, 29));
        yield return new GiltInfo("TR38", "4¾% Treasury Stock 2038", 4.75m, new DateTime(2038, 12, 7));
        yield return new GiltInfo("TR39", "1 1/8% Treasury Gilt 2039", 1.125m, new DateTime(2039, 1, 31));
        yield return new GiltInfo("T39", "4¼% Treasury Gilt 2039", 4.25m, new DateTime(2039, 9, 7));
        yield return new GiltInfo("TG40", "4 3/8% Treasury Gilt 2040", 4.375m, new DateTime(2040, 1, 31));
        yield return new GiltInfo("T40", "4¼% Treasury Gilt 2040", 4.25m, new DateTime(2040, 12, 7));
        yield return new GiltInfo("TG41", "1¼ % Treasury Gilt 2041", 1.25m, new DateTime(2041, 10, 22));
        yield return new GiltInfo("T42", "4½% Treasury Gilt 2042", 4.50m, new DateTime(2042, 12, 7));
        yield return new GiltInfo("TR43", "4¾% Treasury Gilt 2043", 4.75m, new DateTime(2043, 10, 22));
        yield return new GiltInfo("TG44", "3¼% Treasury Gilt 2044", 3.25m, new DateTime(2044, 1, 22));
        yield return new GiltInfo("T45", "3½% Treasury Gilt 2045", 3.50m, new DateTime(2045, 1, 22));
        yield return new GiltInfo("TG46", "0 7/8% Treasury Gilt 2046", 0.875m, new DateTime(2046, 1, 31));
        yield return new GiltInfo("T46", "4¼% Treasury Gilt 2046", 4.25m, new DateTime(2046, 12, 7));
        yield return new GiltInfo("TG47", "1½% Treasury Gilt 2047", 1.50m, new DateTime(2047, 7, 22));
        yield return new GiltInfo("TG49", "1¾% Treasury Gilt 2049", 1.75m, new DateTime(2049, 1, 22));
        yield return new GiltInfo("T49", "4¼% Treasury Gilt 2049", 4.25m, new DateTime(2049, 12, 7));
        yield return new GiltInfo("TG50", "0 5/8% Treasury Gilt 2050", 0.625m, new DateTime(2050, 10, 22));
        yield return new GiltInfo("T51A", "1¼% Treasury Gilt 2051", 1.25m, new DateTime(2051, 7, 31));
        yield return new GiltInfo("T52", "3¾% Treasury Gilt 2052", 3.75m, new DateTime(2052, 7, 22));
        yield return new GiltInfo("TG53", "1½% Green Gilt 2053", 1.50m, new DateTime(2053, 7, 31));
        yield return new GiltInfo("T53", "3¾% Treasury Gilt 2053", 3.75m, new DateTime(2053, 10, 22));
        yield return new GiltInfo("T54", "4 3/8% Treasury Gilt 2054", 4.375m, new DateTime(2054, 7, 31));
        yield return new GiltInfo("TR54", "1 5/8% Treasury Gilt 2054", 1.625m, new DateTime(2054, 10, 22));
        yield return new GiltInfo("TR4Q", "4¼% Treasury Gilt 2055", 4.25m, new DateTime(2055, 12, 7));
        yield return new GiltInfo("TG57", "1¾% Treasury Gilt 2057", 1.75m, new DateTime(2057, 7, 22));
        yield return new GiltInfo("TR60", "4% Treasury Gilt 2060", 4.00m, new DateTime(2060, 1, 22));
        yield return new GiltInfo("TG61", "0½% Treasury Gilt 2061", 0.50m, new DateTime(2061, 10, 22));
        yield return new GiltInfo("TR63", "4% Treasury Gilt 2063", 4.00m, new DateTime(2063, 10, 22));
        yield return new GiltInfo("TG65", "2½% Treasury Gilt 2065", 2.50m, new DateTime(2065, 7, 22));
        yield return new GiltInfo("TR68", "3½% Treasury Gilt 2068", 3.50m, new DateTime(2068, 7, 22));
        yield return new GiltInfo("TG71", "1 5/8% Treasury Gilt 2071", 1.625m, new DateTime(2071, 10, 22));
        yield return new GiltInfo("TR73", "1 1/8% Treasury Gilt 2073", 1.125m, new DateTime(2073, 10, 22));
    }
}