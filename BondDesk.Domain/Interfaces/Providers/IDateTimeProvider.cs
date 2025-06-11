using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondDesk.Domain.Interfaces.Providers;
public interface IDateTimeProvider
{
	DateTime GetToday();
}
