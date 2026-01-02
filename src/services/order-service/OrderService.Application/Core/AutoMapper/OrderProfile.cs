using OrderService.Application.DTO;
using OrderService.Domain.Orders.Entities;

namespace OrderService.Application.Core.AutoMapper;

public class OrderProfile : MapperBase
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
           .ForMember(d => d.Id, o => o.MapFrom(s => s.Id.Value))
           .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.CustomerId.Value))
           .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.Name))
           .ForMember(d => d.TotalAmount, o => o.MapFrom(s => s.TotalAmount.Value))
           .ForMember(d => d.Currency, o => o.MapFrom(s => s.TotalAmount.Currency))
           .ForMember(d => d.Details, o => o.MapFrom(s => s.Details));

        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId.Value))
            .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.UnitPrice.Value));
    }
}
