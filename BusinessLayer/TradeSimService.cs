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

namespace BusinessLayer
{
    public class TradeSimService: ITradeSimService
    {
        private readonly IDbService _dbService;
        private PeriodicTimer _timer;
        private readonly ObservableCollection<CurrencyPairs> _pairs = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public TradeSimService(IDbService dbService)
        {
            _dbService = dbService;
            Pairs = new ReadOnlyObservableCollection<CurrencyPairs>(_pairs);

        }

        public ReadOnlyObservableCollection<CurrencyPairs> Pairs { get; }

        public async Task LoadPairs()
        {
            var pairs = await _dbService.PullData();
            foreach (var pair in pairs)
            {
                pair.CurrentValue = (pair.MinValue + pair.MaxValue) / 2;
                _pairs.Add(pair);
            }
        }

        public async Task StartAsync(CancellationToken token)
        {
            _timer = new PeriodicTimer(TimeSpan.FromSeconds(2));

            while (await _timer.WaitForNextTickAsync(token))
            {
                UpdatePairs();
            }
        }


        private async Task UpdatePairs()
        {
            foreach (var pair in _pairs)
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
            await _dbService.UpdateMinMax();

        }
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
