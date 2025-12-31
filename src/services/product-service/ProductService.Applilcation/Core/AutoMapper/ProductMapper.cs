using ProductService.Applilcation.Core.DTO;
using ProductWebApi.Domain.Entities;

namespace ProductService.Applilcation.Core.AutoMapper;

internal class ProductMapper : MapperBase
{
    public ProductMapper()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
    }
}
