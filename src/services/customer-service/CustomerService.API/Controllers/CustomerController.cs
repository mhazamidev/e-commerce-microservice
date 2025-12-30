using CustomerService.Applilcation.Customers.Command;
using CustomerService.Applilcation.Customers.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NETCareer.WebAPI.Controllers.Base;

namespace CustomerService.API.Controllers;

public class CustomerController : BaseController
{
    public CustomerController(IMediator mediator) : base(mediator)
    {
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActiveCustomersDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Customers(int currentPage, int pageSize)
        => await Response(new GetActiveCustomersQuery(pageSize, currentPage));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetCustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
       => await Response(new GetCustomerQuery(id));

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddCustomerCommand customer)
       => await Response(customer);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(GetCustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
       => await Response(new DeleteCustomerCommand(id));

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Edit([FromBody] UpdateCustomerCommand customer)
       => await Response(customer);
}
