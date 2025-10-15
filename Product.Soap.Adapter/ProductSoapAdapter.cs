namespace Product.Soap.Adapter;

public class ProductSoapAdapter
{
    public ProductDetails GetProductDetails(string productId)
    {
        // Simulate fetching product details from a SOAP service
        return new ProductDetails
        {
            ProductId = productId,
            Name = "Sample Product",
            Description = "This is a sample product description.",
            Price = 19.99m
        };
         //TODO: remove this mock and implement the SOAP client
        //SoapClient soapClient = new SoapClient("https://www.soap.com/product/details");
        //return soapClient.GetProductDetails(productId);
    }
}
