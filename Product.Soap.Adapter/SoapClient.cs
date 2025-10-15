using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace Product.Soap.Adapter;

public class SoapClient
{
    private readonly HttpClient _httpClient;
    private readonly string _soapEndpoint;

    public SoapClient(string soapEndpoint)
    {
        _httpClient = new HttpClient();
        _soapEndpoint = soapEndpoint;
    }

    public ProductDetails GetProductDetails(string productId)
    {
        // Build the SOAP envelope
        var soapEnvelope = BuildSoapEnvelope(productId);

        // Create the HTTP request
        var request = new HttpRequestMessage(HttpMethod.Post, _soapEndpoint)
        {
            Content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml")
        };

        // Add SOAP headers
        request.Headers.Add("SOAPAction", "\"GetProductDetails\"");

        // Make the SOAP call
        var response = _httpClient.Send(request);
        response.EnsureSuccessStatusCode();

        // Read and process the response
        var responseContent = response.Content.ReadAsStringAsync().Result;
        return ParseSoapResponse(responseContent);
    }

    private string BuildSoapEnvelope(string productId)
    {
        // Basic SOAP envelope for the GetProductDetails method
        return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""
               xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
               xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
    <soap:Body>
        <GetProductDetails xmlns=""http://tempuri.org/"">
            <productId>{productId}</productId>
        </GetProductDetails>
    </soap:Body>
</soap:Envelope>";
    }

    private ProductDetails ParseSoapResponse(string soapResponse)
    {
        // Parse the SOAP XML response
        var doc = XDocument.Parse(soapResponse);
        XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
        XNamespace ns = "http://tempuri.org/";

        var body = doc.Root?.Element(soap + "Body");
        var response = body?.Element(ns + "GetProductDetailsResponse");
        var result = response?.Element(ns + "GetProductDetailsResult");

        if (result == null)
        {
            throw new InvalidOperationException("Invalid SOAP response");
        }

        return new ProductDetails
        {
            ProductId = result.Element(ns + "ProductId")?.Value,
            Name = result.Element(ns + "Name")?.Value,
            Description = result.Element(ns + "Description")?.Value,
            Price = decimal.Parse(result.Element(ns + "Price")?.Value ?? "0")
        };
    }
}
