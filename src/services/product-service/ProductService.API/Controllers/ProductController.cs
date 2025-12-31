using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.Controllers.Base;
using ProductService.Applilcation.Core.DTO;
using ProductService.Applilcation.Products.Command;
using ProductService.Applilcation.Products.Query;

namespace ProductService.API.Controllers;

public class ProductController : BaseController
{
    public ProductController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddProductCommand product)
        => await Response(product);


    [HttpPut]
    [ProducesResponseType(typeof(IEnumerable<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Edit([FromBody] UpdateProductCommand product)
        => await Response(product);


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetActives()
        => await Response(new GetActiveProductsQuery());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
        => await Response(new GetProductByIdQuery(id));


    [HttpGet("{name}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByName([FromRoute] string name)
        => await Response(new GetProductByNameQuery(name));
}
