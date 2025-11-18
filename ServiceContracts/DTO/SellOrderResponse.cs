using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse : IOrderResponse
    {
        public Guid SellOrderID { get; set; }

        [Required]
        public string? StockSymbol { get; set; }
        [Required]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public OrderType TypeOfOrder => OrderType.SellOrder;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not SellOrderResponse) return false;

            SellOrderResponse other = (SellOrderResponse)obj;

            return SellOrderID == other.SellOrderID && 
                StockSymbol == other.StockSymbol && 
                StockName == other.StockName && 
                DateAndTimeOfOrder == other.DateAndTimeOfOrder 
                && Quantity == other.Quantity 
                && Price == other.Price;
        }
    }

    public static class SellOrderExtentions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder SellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = SellOrder.SellOrderID,
                StockSymbol = SellOrder.StockSymbol,
                StockName = SellOrder.StockName,
                Price = SellOrder.Price,
                DateAndTimeOfOrder = SellOrder.DateAndTimeOfOrder,
                Quantity = SellOrder.Quantity,
                TradeAmount = SellOrder.Price * SellOrder.Quantity
            };
        }
    }
}
