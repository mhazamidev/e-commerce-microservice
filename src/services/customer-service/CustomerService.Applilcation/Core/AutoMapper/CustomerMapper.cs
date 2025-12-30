using CustomerService.Applilcation.Customers.Command;
using CustomerService.Applilcation.Customers.Query;
using CustomerService.Domain.Customers.Entities;
using CustomerService.Domain.Customers.ValueObjects;

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
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.Name.LastName));
    }
}
