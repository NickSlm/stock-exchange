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
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer.Service
{
    public class DbService: IDbService
    {


        private readonly IServiceScopeFactory _scopeFactory;

        public DbService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<IList<CurrencyPairs>> PullData()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ExchangeContext>();
            var pairs =  await context.CurrencyPairs
            .Include(p => p.BaseCurrency)
            .Include(p => p.QuoteCurrency)
            .ToListAsync();

            return pairs;

        }
        public async Task UpdateMinMax(IList<CurrencyPairs> pairs)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ExchangeContext>();
            foreach (var pair in pairs)
            {
                var entity = await context.CurrencyPairs.FindAsync(pair.PairId);
                if (entity != null)
                {
                    entity.MinValue = pair.MinValue;
                    entity.MaxValue = pair.MaxValue;
                }
            }
            await context.SaveChangesAsync();
        }

    }
}
