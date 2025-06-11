using BondDesk.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondDesk.GiltsInIssueRepo.Models;
internal class GiltInfo : IGiltInfo
{
    public string Epic { get; set; }
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
