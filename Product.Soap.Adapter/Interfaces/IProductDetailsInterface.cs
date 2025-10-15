using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Soap.Adapter.Interfaces;

public interface IProductDetailsInterface
{
    ProductDetails GetProductDetails(string productId);
}
