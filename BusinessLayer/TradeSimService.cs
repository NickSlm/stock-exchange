using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DataLayer.Interface;
using System.Collections.ObjectModel;
using DataLayer.Models;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusinessLayer
{
    public class TradeSimService: BackgroundService
    {
        private readonly IDbService _dbService;
        private readonly ICurrencyStorage _storage;


        public TradeSimService(IDbService dbService, ICurrencyStorage storage )
        {
            _dbService = dbService;
            _storage = storage;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            await LoadPairs();

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));
            while (await timer.WaitForNextTickAsync(token))
            {
                await UpdatePairs();
            }
        }

        public async Task LoadPairs()
        {
            var pairs = await _dbService.PullData();
            foreach (var pair in pairs)
                pair.CurrentValue = (pair.MinValue + pair.MaxValue) / 2;
            _storage.Load(pairs);

        }

        private async Task UpdatePairs()
        {
            foreach (var pair in _storage.Pairs)
            {
                pair.CurrentValue = Math.Round(pair.CurrentValue + (decimal)(Random.Shared.NextDouble() * 0.1 - 0.05), 4);
                if (pair.CurrentValue > pair.MaxValue)
                {
                    pair.MaxValue = pair.CurrentValue;
                }
                if (pair.CurrentValue < pair.MinValue)
                {
                    pair.MinValue = pair.CurrentValue;
                }
            }
            await _dbService.UpdateMinMax(_storage.Pairs);
            _storage.Update();
        }
    }
}
