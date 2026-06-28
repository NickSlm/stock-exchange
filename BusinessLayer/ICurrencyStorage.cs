using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ICurrencyStorage
    {
        event Action<IList<CurrencyPairs>>? PairsUpdated;
        IList<CurrencyPairs> Pairs { get; }
        void Load(IList<CurrencyPairs> pairs);
        void Update();

    }
}
