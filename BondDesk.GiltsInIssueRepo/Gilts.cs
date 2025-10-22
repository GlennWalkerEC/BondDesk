using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.GiltsInIssueRepo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BondDesk.GiltsInIssueRepo;

public class Gilts : IGiltRepo
{
	private static HttpClient? client;

	public Gilts()
	{
		client ??= new HttpClient();
	}

	public async IAsyncEnumerable<IGiltInfo> GetAllGiltsAsync()
	{
		// Sourced from https://giltsyield.com/bond/ as of June 2024
		//yield return new GiltInfo("T25", "GB00BTHH2R79", "2% Treasury Gilt 2025", 2.00m, new DateTime(2025, 9, 7), new DateTime(2015, 3, 20));
		//yield return new GiltInfo("TY25", "GB00BPCJD880", "3½% Treasury Gilt 2025", 3.50m, new DateTime(2025, 10, 22), new DateTime(2023, 1, 18));
		yield return new GiltInfo("T26", "GB00BL68HJ26", "0 1/8% Treasury Gilt 2026", 0.125m, new DateTime(2026, 1, 30), new DateTime(2020, 6, 3));
		yield return new GiltInfo("TG26", "GB00BYZW3G56", "1½% Treasury Gilt 2026", 1.50m, new DateTime(2026, 7, 22), new DateTime(2016, 2, 18));
		yield return new GiltInfo("T26A", "GB00BNNGP668", "0 3/8% Treasury Gilt 2026", 0.375m, new DateTime(2026, 10, 22), new DateTime(2021, 3, 3));
		yield return new GiltInfo("T27A", "GB00BL6C7720", "4 1/8% Treasury Gilt 2027", 4.125m, new DateTime(2027, 1, 29), new DateTime(2022, 10, 13));
		yield return new GiltInfo("TS27", "GB00BPSNB460", "3¾% Treasury Gilt 2027", 3.75m, new DateTime(2027, 3, 7), new DateTime(2024, 1, 11));
		yield return new GiltInfo("TG27", "GB00BDRHNP05", "1¼% Treasury Gilt 2027", 1.25m, new DateTime(2027, 7, 22), new DateTime(2017, 3, 15));
		yield return new GiltInfo("TR27", "GB00B16NNR78", "4¼% Treasury Gilt 2027", 4.25m, new DateTime(2027, 12, 7), new DateTime(2006, 9, 6));
		yield return new GiltInfo("TN28", "GB00BMBL1G81", "0 1/8% Treasury Gilt 2028", 0.125m, new DateTime(2028, 1, 31), new DateTime(2020, 6, 12));
		yield return new GiltInfo("TE28", "GB00BSQNRC93", "4 3/8% Treasury Gilt 2028", 4.375m, new DateTime(2028, 3, 7), new DateTime(2024, 11, 14));
		yield return new GiltInfo("TS28", "GB00BMF9LG83", "4½% Treasury Gilt 2028", 4.50m, new DateTime(2028, 6, 7), new DateTime(2023, 6, 21));
		yield return new GiltInfo("TG28", "GB00BFX0ZL78", "1 5/8% Treasury Gilt 2028", 1.625m, new DateTime(2028, 10, 22), new DateTime(2018, 3, 16));
		yield return new GiltInfo("TR28", "GB0002404191", "6% Treasury Stock 2028", 6.00m, new DateTime(2028, 12, 7), new DateTime(1998, 1, 29));
		yield return new GiltInfo("TG29", "GB00BLPK7227", "0½% Treasury Gilt 2029", 0.50m, new DateTime(2029, 1, 31), new DateTime(2021, 9, 2));
		yield return new GiltInfo("TS29", "GB00BQC82B83", "4 1/8% Treasury Gilt 2029", 4.125m, new DateTime(2029, 7, 22), new DateTime(2024, 5, 1));
		yield return new GiltInfo("TR29", "GB00BJMHB534", "0 7/8% Treasury Gilt 2029", 0.875m, new DateTime(2029, 10, 22), new DateTime(2019, 6, 19));
		yield return new GiltInfo("T30", "GB00BSQNRD01", "4 3/8% Treasury Gilt 2030", 4.375m, new DateTime(2030, 3, 7), new DateTime(2025, 1, 9));
		yield return new GiltInfo("TG30", "GB00BL68HH02", "0 3/8% Treasury Gilt 2030", 0.375m, new DateTime(2030, 10, 22), new DateTime(2020, 5, 13));
		yield return new GiltInfo("TR30", "GB00B24FF097", "4¾% Treasury Gilt 2030", 4.75m, new DateTime(2030, 12, 7), new DateTime(2007, 10, 3));
		yield return new GiltInfo("TG31", "GB00BMGR2809", "0¼% Treasury Gilt 2031", 0.25m, new DateTime(2031, 7, 31), new DateTime(2020, 11, 13));
		yield return new GiltInfo("T31", "GB00BPSNBF73", "4% Treasury Gilt 2031", 4.00m, new DateTime(2031, 10, 22), new DateTime(2024, 2, 29));
		yield return new GiltInfo("TG32", "GB00BM8Z2T38", "1% Treasury Gilt 2032", 1.00m, new DateTime(2032, 1, 31), new DateTime(2021, 12, 2));
		yield return new GiltInfo("TR32", "GB0004893086", "4¼% Treasury Stock 2032", 4.25m, new DateTime(2032, 6, 7), new DateTime(2000, 5, 25));
		yield return new GiltInfo("TR33", "GB00BMV7TC88", "3¼% Treasury Gilt 2033", 3.25m, new DateTime(2033, 1, 31), new DateTime(2023, 1, 11));
		yield return new GiltInfo("TG33", "GB00BM8Z2S21", "0 7/8% Green Gilt 2033", 0.875m, new DateTime(2033, 7, 31), new DateTime(2021, 9, 22));
		yield return new GiltInfo("T34", "GB00BPJJKN53", "4 5/8% Treasury Gilt 2034", 4.625m, new DateTime(2034, 1, 31), new DateTime(2023, 10, 12));
		yield return new GiltInfo("TS34", "GB00BQC82C90", "4¼% Treasury Gilt 2034", 4.25m, new DateTime(2034, 7, 31), new DateTime(2024, 6, 12));
		yield return new GiltInfo("TR34", "GB00B52WS153", "4½% Treasury Gilt 2034", 4.50m, new DateTime(2034, 9, 7), new DateTime(2009, 6, 17));
		yield return new GiltInfo("T35", "GB00BT7J0027", "4½% Treasury Gilt 2035", 4.50m, new DateTime(2035, 3, 7), new DateTime(2025, 2, 12));
		yield return new GiltInfo("TG35", "GB00BMGR2916", "0 5/8% Treasury Gilt 2035", 0.625m, new DateTime(2035, 7, 31), new DateTime(2020, 9, 9));
		yield return new GiltInfo("T35V", "GB00BTXS1K06", "4¾% Treasury Gilt 2035", 4.75m, new DateTime(2035, 10, 22), new DateTime(2025, 9, 3));
		yield return new GiltInfo("T4Q", "GB0032452392", "4¼% Treasury Stock 2036", 4.25m, new DateTime(2036, 3, 7), new DateTime(2003, 2, 27));
		yield return new GiltInfo("TG37", "GB00BZB26Y51", "1¾% Treasury Gilt 2037", 1.75m, new DateTime(2037, 9, 7), new DateTime(2016, 11, 9));
		yield return new GiltInfo("TG38", "GB00BQC4R999", "3¾% Treasury Gilt 2038", 3.75m, new DateTime(2038, 1, 29), new DateTime(2022, 11, 9));
		yield return new GiltInfo("TR38", "GB00B00NY175", "4¾% Treasury Stock 2038", 4.75m, new DateTime(2038, 12, 7), new DateTime(2004, 4, 23));
		yield return new GiltInfo("TR39", "GB00BLPK7334", "1 1/8% Treasury Gilt 2039", 1.125m, new DateTime(2039, 1, 31), new DateTime(2021, 7, 14));
		yield return new GiltInfo("T39", "GB00B3KJDS62", "4¼% Treasury Gilt 2039", 4.25m, new DateTime(2039, 9, 7), new DateTime(2009, 3, 5));
		yield return new GiltInfo("TG40", "GB00BQC82D08", "4 3/8% Treasury Gilt 2040", 4.375m, new DateTime(2040, 1, 31), new DateTime(2024, 9, 4));
		yield return new GiltInfo("T40", "GB00B6460505", "4¼% Treasury Gilt 2040", 4.25m, new DateTime(2040, 12, 7), new DateTime(2010, 6, 30));
		yield return new GiltInfo("TG41", "GB00BJQWYH73", "1¼ % Treasury Gilt 2041", 1.25m, new DateTime(2041, 10, 22), new DateTime(2020, 1, 22));
		yield return new GiltInfo("T42", "GB00B1VWPJ53", "4½% Treasury Gilt 2042", 4.50m, new DateTime(2042, 12, 7), new DateTime(2007, 6, 6));
		yield return new GiltInfo("TR43", "GB00BPJJKP77", "4¾% Treasury Gilt 2043", 4.75m, new DateTime(2043, 10, 22), new DateTime(2023, 11, 16));
		yield return new GiltInfo("TG44", "GB00B84Z9V04", "3¼% Treasury Gilt 2044", 3.25m, new DateTime(2044, 1, 22), new DateTime(2012, 10, 24));
		yield return new GiltInfo("T45", "GB00BN65R313", "3½% Treasury Gilt 2045", 3.50m, new DateTime(2045, 1, 22), new DateTime(2014, 6, 25));
		yield return new GiltInfo("TG46", "GB00BNNGP775", "0 7/8% Treasury Gilt 2046", 0.875m, new DateTime(2046, 1, 31), new DateTime(2021, 1, 20));
		yield return new GiltInfo("T46", "GB00B128DP45", "4¼% Treasury Gilt 2046", 4.25m, new DateTime(2046, 12, 7), new DateTime(2006, 5, 12));
		yield return new GiltInfo("TG47", "GB00BDCHBW80", "1½% Treasury Gilt 2047", 1.50m, new DateTime(2047, 7, 22), new DateTime(2016, 9, 21));
		yield return new GiltInfo("TG49", "GB00BFWFPP71", "1¾% Treasury Gilt 2049", 1.75m, new DateTime(2049, 1, 22), new DateTime(2018, 9, 12));
		yield return new GiltInfo("T49", "GB00B39R3707", "4¼% Treasury Gilt 2049", 4.25m, new DateTime(2049, 12, 7), new DateTime(2008, 9, 3));
		yield return new GiltInfo("TG50", "GB00BMBL1F74", "0 5/8% Treasury Gilt 2050", 0.625m, new DateTime(2050, 10, 22), new DateTime(2020, 6, 10));
		yield return new GiltInfo("T51A", "GB00BLH38158", "1¼% Treasury Gilt 2051", 1.25m, new DateTime(2051, 7, 31), new DateTime(2021, 4, 28));
		yield return new GiltInfo("T52", "GB00B6RNH572", "3¾% Treasury Gilt 2052", 3.75m, new DateTime(2052, 7, 22), new DateTime(2011, 9, 28));
		yield return new GiltInfo("TG53", "GB00BM8Z2V59", "1½% Green Gilt 2053", 1.50m, new DateTime(2053, 7, 31), new DateTime(2021, 10, 22));
		yield return new GiltInfo("T53", "GB00BPCJD997", "3¾% Treasury Gilt 2053", 3.75m, new DateTime(2053, 10, 22), new DateTime(2023, 1, 25));
		yield return new GiltInfo("T54", "GB00BPSNBB36", "4 3/8% Treasury Gilt 2054", 4.375m, new DateTime(2054, 7, 31), new DateTime(2024, 1, 24));
		yield return new GiltInfo("TR54", "GB00BJLR0J16", "1 5/8% Treasury Gilt 2054", 1.625m, new DateTime(2054, 10, 22), new DateTime(2019, 5, 15));
		yield return new GiltInfo("TR4Q", "GB00B06YGN05", "4¼% Treasury Gilt 2055", 4.25m, new DateTime(2055, 12, 7), new DateTime(2005, 5, 27));
		yield return new GiltInfo("T56", "GB00BT7J0241", "5 3/8% Treasury Gilt 2056", 5.375m, new DateTime(2056, 1, 31), new DateTime(2025, 5, 21));
		yield return new GiltInfo("TG57", "GB00BD0XH204", "1¾% Treasury Gilt 2057", 1.75m, new DateTime(2057, 7, 22), new DateTime(2017, 1, 25));
		yield return new GiltInfo("TR60", "GB00B54QLM75", "4% Treasury Gilt 2060", 4.00m, new DateTime(2060, 1, 22), new DateTime(2009, 10, 22));
		yield return new GiltInfo("TG61", "GB00BMBL1D50", "0½% Treasury Gilt 2061", 0.50m, new DateTime(2061, 10, 22), new DateTime(2020, 5, 20));
		yield return new GiltInfo("TR63", "GB00BMF9LF76", "4% Treasury Gilt 2063", 4.00m, new DateTime(2063, 10, 22), new DateTime(2023, 5, 17));
		yield return new GiltInfo("TG65", "GB00BYYMZX75", "2½% Treasury Gilt 2065", 2.50m, new DateTime(2065, 7, 22), new DateTime(2015, 10, 21));
		yield return new GiltInfo("TR68", "GB00BBJNQY21", "3½% Treasury Gilt 2068", 3.50m, new DateTime(2068, 7, 22), new DateTime(2013, 6, 26));
		yield return new GiltInfo("TG71", "GB00BFMCN652", "1 5/8% Treasury Gilt 2071", 1.625m, new DateTime(2071, 10, 22), new DateTime(2018, 5, 16));
		yield return new GiltInfo("TR73", "GB00BLBDX619", "1 1/8% Treasury Gilt 2073", 1.125m, new DateTime(2073, 10, 22), new DateTime(2022, 2, 9));
	}
}