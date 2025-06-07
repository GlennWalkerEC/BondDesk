using BondDesk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondDesk.Domain.Interfaces.Services;
public interface IGiltsService
{
	IAsyncEnumerable<Bond> GetGiltsAsync();
}
