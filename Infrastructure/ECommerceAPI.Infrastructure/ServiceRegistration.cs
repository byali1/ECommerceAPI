using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Enums;
using ECommerceAPI.Infrastructure.Services;
using ECommerceAPI.Infrastructure.Services.Storage;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : BaseStorage, IStorage 
        {
            serviceCollection.AddScoped<IStorage, T>();
        }




    }
}
