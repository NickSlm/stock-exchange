using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ITradeSimService: INotifyPropertyChanged
    {
        ReadOnlyObservableCollection<CurrencyPairs> Pairs { get; }
        Task StartAsync(CancellationToken ct);
        Task LoadPairs();
    }
}
