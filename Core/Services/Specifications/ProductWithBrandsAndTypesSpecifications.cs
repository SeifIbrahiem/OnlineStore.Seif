using Domain.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }

        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParameters specParams) 
            : base(
                  p =>

                  (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search.ToLower())) &&

                  (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId)
                  &&
                  (!specParams.TypeId.HasValue || p.TypeId == specParams.TypeId)
                  )
         
        {
            ApplyIncludes();

            ApplySorting(specParams.Sort);

            ApplyPagination(specParams.PageIndex,specParams.PageSize);
        }
        private void ApplyIncludes()
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);

        }

        private void ApplySorting(string? sort)
        {

            if (!string.IsNullOrEmpty(sort))
            {

                switch (sort.ToLower())
                {

                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "Priceasc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "Pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            else { AddOrderBy(p => p.Name); }
        }

    }
}
