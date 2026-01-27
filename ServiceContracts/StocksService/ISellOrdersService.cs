using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ISellOrdersService
    {
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
