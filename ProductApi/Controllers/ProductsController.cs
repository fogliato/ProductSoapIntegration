using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Domain.Interfaces;

namespace ProductApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductDomainService _productDomainService;

    public ProductsController(IProductDomainService productDomainService)
    {
        _productDomainService = productDomainService;
    }

    /// <summary>
    /// Gets product information by ID. Requires JWT authentication.
    /// </summary>
    /// <param name="productId">The unique identifier of the product</param>
    /// <returns>Product details</returns>
    /// <response code="200">Returns the product information</response>
    /// <response code="400">If the product ID is invalid</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="404">If the product is not found</response>
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            return BadRequest("Product ID cannot be empty");
        }

        return Ok(_productDomainService.GetProductDetails(productId));
    }
}
