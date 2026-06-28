using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class CurrencyPairs: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        [Key]
        public int PairId { get; set; }
        public int BaseCurrencyId { get; set; }
        public int QuoteCurrencyId { get; set; }


        public Currencies BaseCurrency { get; set; }
        public Currencies QuoteCurrency { get; set; }

        private decimal _minValue { get; set; }
        private decimal _maxValue { get; set; }
        private decimal _currentValue { get; set; }

        public decimal MinValue
        {
            get => _minValue;
            set
            { 
                _minValue = value;
                OnPropertyChanged(nameof(MinValue));
            }
        }
        public decimal MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                OnPropertyChanged(nameof(MaxValue));
            }
        }

        [NotMapped]
        public decimal CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                OnPropertyChanged(nameof(CurrentValue));
            }
        }

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
