using Product.Domain.Interfaces;
using Product.Soap.Adapter;
using Product.Soap.Adapter.Interfaces;

namespace Product.Domain.Services;

public class ProductDomainService : IProductDomainService
{
    private readonly IProductDetailsInterface _productSoapAdapter;

    public ProductDomainService(IProductDetailsInterface productSoapAdapter)
    {
        _productSoapAdapter = productSoapAdapter;
    }

    /// <summary>
    /// Fetches product details using the SOAP adapter.
    /// Here we can add additional business logic if needed.
    /// </summary>
    /// <param name="productId">product id</param>
    /// <returns>product details</returns>
    /// <exception cref="ArgumentException">if the product id is invalid</exception>
    public ProductDetails GetProductDetails(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
            throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
        return _productSoapAdapter.GetProductDetails(productId);
    }
}
