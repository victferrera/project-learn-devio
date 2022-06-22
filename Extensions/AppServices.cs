using AppMercadoBasico.Repository;
using AppMercadoBasico.ViewModels;

namespace AppMercadoBasico.Extensions
{
    public static class AppServices
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<ProductsRepository>();
            services.AddScoped<CustomersRepository>();
            services.AddScoped<ViewModel>();

            return services;
        }
    }
}
