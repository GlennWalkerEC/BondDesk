﻿using BondDesk.Domain.Interfaces.Models;
using System.Text.Json.Serialization;

namespace BondDesk.GiltsInIssueRepo.Models;
internal class GiltInfo : IGiltInfo
{
	[JsonPropertyName("code")]
	public string Epic { get; set; }

	[JsonPropertyName("description")]
	public string Name { get; set; }

    public decimal Coupon { get; set; }
    public DateTime MaturityDate { get; set; }
    public DateTime IssueDate { get; }

    public GiltInfo(string epic, string name, decimal coupon, DateTime maturity, DateTime issueDate)
    {
        Epic = epic;
        Name = name;
        Coupon = coupon;
        MaturityDate = maturity;
        IssueDate = issueDate;
    }
}
