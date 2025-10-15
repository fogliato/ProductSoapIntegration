using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Interfaces;
using Product.Domain.Services;
using Product.Soap.Adapter;
using Product.Soap.Adapter.Interfaces;

namespace Product.IoC;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        #region Services Domain
        services.AddScoped<IProductDomainService, ProductDomainService>();
        #endregion Services Domain


        #region Adapters
        services.AddScoped<IProductDetailsInterface, ProductSoapAdapter>();
        #endregion
    }
}
