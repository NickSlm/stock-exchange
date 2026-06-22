using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface IDbService
    {
        Task<IList<CurrencyPairs>> PullData();
        Task UpdateMinMax();
    }
}
