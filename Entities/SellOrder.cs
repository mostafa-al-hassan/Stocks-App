using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SellOrder
    {
        public Guid SellOrderID;

        [Required]
        public string? StockSymbol;
        [Required]
        public string? StockName;

        public DateTime DateAndTimeOfOrder;

        [Range(1, 100000)]
        public uint Quantity;
        [Range(1, 10000)]
        public double Price;

    }
}
