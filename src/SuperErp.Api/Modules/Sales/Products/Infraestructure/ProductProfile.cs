using AutoMapper;
using SuperErp.Sales.Domain;

namespace SuperErp.Sales
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, SuperErp.Sales.Model.Product>()
                    .ReverseMap();
        }
    }
}
