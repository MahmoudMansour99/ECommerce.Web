using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Services.Exceptions;
using ECommerce.Services_Abstractions;
using ECommerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var CustomerBasket = _mapper.Map<CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await  _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            return _mapper.Map<BasketDTO>(CreatedOrUpdatedBasket);
        }

        public async Task<bool> DeleteBasketAsync(string basketId) => await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<BasketDTO> GetBasketAsync(string basketId)
        {
            var Basket = await _basketRepository.GetBasketAync(basketId);
            if (Basket == null)
                throw new BasketNotFoundException(basketId);
            return _mapper.Map<BasketDTO>(Basket);
        }
    }
}
