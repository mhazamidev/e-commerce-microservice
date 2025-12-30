using CustomerService.Applilcation.Customers.Query;
using CustomerService.Domain.Customers.Entities;

namespace CustomerService.Applilcation.Core.AutoMapper;

public class CustomerMapper : MapperBase
{
    public CustomerMapper()
    {
        CreateMap<Customer, ActiveCustomersDto>()
            .ForMember(x => x.Id, o => o.MapFrom(s => s.Id.Value))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.Email.Value))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.Name.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.Name.LastName));

        CreateMap<Customer, GetCustomerDto>()
            .ForMember(x => x.Id, o => o.MapFrom(s => s.Id.Value))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.Email.Value))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.Name.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.Name.LastName))
            .ForMember(d => d.Addresses, o => o.MapFrom(s => s.Addresses));

        CreateMap<Address, AddressDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id.Value))
            .ForMember(d => d.IsPrimary, o => o.MapFrom(s => s.IsPrimary));
    }
}
