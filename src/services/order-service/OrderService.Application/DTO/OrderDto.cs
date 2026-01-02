namespace OrderService.Application.DTO;

public record OrderDto(Guid Id, Guid CustomerId, string Status, decimal TotalAmount, string Currency, List<OrderDetailDto> Details);
