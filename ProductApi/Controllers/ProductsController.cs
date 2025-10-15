using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    /// <summary>
    /// Gets product information by ID.
    /// </summary>
    /// <param name="productId">The unique identifier of the product</param>
    /// <returns>Product details</returns>
    /// <response code="200">Returns the product information</response>
    /// <response code="400">If the product ID is invalid</response>
    /// <response code="404">If the product is not found</response>
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            return BadRequest("Product ID cannot be empty");
        }

        return Ok(new { Message = $"Hello from ProductsController! Product ID: {productId}" });
    }

    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <returns>A list of all products</returns>
    /// <response code="200">Returns the list of products</response>
    [HttpGet]
    [ProducesResponseType(typeof(object[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = new[]
        {
            new { Id = "1", Name = "Product 1", Price = 10.99m },
            new { Id = "2", Name = "Product 2", Price = 15.99m },
            new { Id = "3", Name = "Product 3", Price = 8.99m }
        };

        return Ok(products);
    }
}
