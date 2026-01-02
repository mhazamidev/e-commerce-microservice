using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Controllers.Base;
using OrderService.Application.DTO;
using OrderService.Application.Orders.Command;
using OrderService.Application.Orders.Query;

namespace OrderService.API.Controllers;

public class OrderController : BaseController
{
    public OrderController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
        => await Response(new GetOrderQuery(id));


    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddOrderCommand order)
        => await Response(order);


    [HttpPost("item/{orderId:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddOrderItemCommand item)
        => await Response(item);


    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Pay([FromBody] PayOrderCommand order)
        => await Response(order);
}
