using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace DataLayer.Context
{
    public class ExchangeContext: DbContext
    {
        public ExchangeContext(DbContextOptions<ExchangeContext> options) : base(options)
        {

        }

        public DbSet<Currencies> Currencies { get; set; }
        public DbSet<CurrencyPairs> CurrencyPairs { get; set; }

    }
}
