using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Model;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
       
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductAsync(ProductSpecificationsParameters specParams)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(specParams);

            //Get All products Throught productrepository
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);

             var specCount = new ProductWithCountSpecifications(specParams);

            var totalSpec = new ProductWithCountSpecifications(specParams);
            //spec.ispagination = false
            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(totalSpec);

           //   var count = products.Count();


            //Mapping IEnumerable <product> To IEnumerable<productResultDto> : Automapeer
           
            return new PaginationResponse<ProductResultDto>(specParams.PageIndex, specParams.PageSize,count, result);
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);
            var Product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if (Product is null) throw new ProductNotFoundExceptions(id);

            var result = mapper.Map<ProductResultDto>(Product);
            return result;

        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var result = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }
        public async Task<IEnumerable<TypesResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var result = mapper.Map<IEnumerable<TypesResultDto>>(types);
            return result;
        }

    }
}
