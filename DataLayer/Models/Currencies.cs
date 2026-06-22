using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Currencies
    {
        [Key]
        public int CurrencyId { get; set; }
        public string Country { get; set; }
        public string CurrencyName { get; set; }
        public string Abbreviation { get; set; }
    }
}
