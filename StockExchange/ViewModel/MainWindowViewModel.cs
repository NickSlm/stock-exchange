using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.ViewModel
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private readonly ITradeSimService _tradeSimService;


        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel(ITradeSimService tradeSimService)
        {
            _tradeSimService = tradeSimService;
        }

        public async Task LoadAsync()
        {
            await _tradeSimService.LoadPairs();
        }

        public ReadOnlyObservableCollection<CurrencyPairs> Pairs => _tradeSimService.Pairs;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
