using Product.Soap.Adapter;

namespace Product.Domain.Interfaces;

public interface IProductDomainService
{
    ProductDetails GetProductDetails(string productId);
}
