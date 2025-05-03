using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager (IUnitOfWork unitOfWork , IMapper mapper , IBasketRepository  basketRepository) : IServiceManager
    {
       public IProductService ProductService {get;} = new ProductService(unitOfWork , mapper);

       public IBasketServices BasketServices { get; } = new BasketService (basketRepository , mapper);
    }
}
