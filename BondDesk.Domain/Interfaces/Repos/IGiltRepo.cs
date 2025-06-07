using BondDesk.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondDesk.Domain.Interfaces.Repos;
public interface IGiltRepo
{
	IEnumerable<IGiltInfo> GetAllGilts();
}
