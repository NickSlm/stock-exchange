using DataLayer.Context;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Service
{
    public class DbService: IDbService
    {


        private readonly ExchangeContext _exchangeContext;

        public DbService(ExchangeContext exchangeContext)
        {
            _exchangeContext = exchangeContext;
        }

        public async Task<IList<CurrencyPairs>> PullData()
        {

            var pairs = await _exchangeContext.CurrencyPairs.Include(p => p.BaseCurrency).Include(p => p.QuoteCurrency).ToListAsync();

            return pairs;
        }
        public async Task UpdateMinMax()
        {
            await _exchangeContext.SaveChangesAsync();
        }

    }
}
