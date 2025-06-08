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
    public DateTime Maturity { get; set; }

    public GiltInfo(string epic, string name, decimal coupon, DateTime maturity)
    {
        Epic = epic;
        Name = name;
        Coupon = coupon;
        Maturity = maturity;
    }
}
