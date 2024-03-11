using Lista3_Crud.Application.interfaces;
using Lista3_Crud.Application.Services;
using Lista3_Crud.Domain.interfaces;
using Lista3_Crud.Domain.Interfaces;
using Lista3_Crud.Infra.Data.Connection;
using Lista3_Crud.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lista3_Crud.CrossCutting
{
    public static class DependencyInjection
    {
        public static void AddDependency(this IServiceCollection services)
        {
            AddInfraestructure(services);
            AddApplication(services);
        }

        private static void AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(ApplicationDbContext));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseItemRepository, PurchaseItemRepository>();
        }

        private static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
        }
    }
}