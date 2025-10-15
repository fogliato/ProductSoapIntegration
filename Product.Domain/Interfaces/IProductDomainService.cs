using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Domain.Interfaces;

public interface IProductDomainService
{
    ProductDetails GetProductDetails(string productId);
}
