using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CurrencyStorage: ICurrencyStorage
    {
        public event Action<IList<CurrencyPairs>>? PairsUpdated;
        private readonly List<CurrencyPairs> _pairs = new();
        public IList<CurrencyPairs> Pairs => _pairs;

        public void Load(IList<CurrencyPairs> pairs)
        {
            _pairs.AddRange(pairs);
            PairsUpdated?.Invoke(_pairs);
        }

        public void Update()
        {
            PairsUpdated?.Invoke(_pairs);
        }
    }
}
