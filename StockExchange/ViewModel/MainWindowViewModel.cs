using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockExchange.ViewModel
{
    public class MainWindowViewModel: INotifyPropertyChanged, IDisposable
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ICurrencyStorage _storage;

        public ObservableCollection<CurrencyPairs> Pairs { get; } = new();


        public MainWindowViewModel(ICurrencyStorage storage)
        {
            _storage = storage;
            _storage.PairsUpdated += OnPairsUpdate;
        }

        private void OnPairsUpdate(IList<CurrencyPairs> pairs)
        {
            if (!Pairs.Any())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var pair in pairs)
                    {
                        Pairs.Add(pair);
                    }
                });
            }
        }

        public void Dispose()
        {
            _storage.PairsUpdated -= OnPairsUpdate;
        }
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
