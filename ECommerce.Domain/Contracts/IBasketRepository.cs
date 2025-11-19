using ECommerce.Domain.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAync(string basketId);
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan TimeToLive = default);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
