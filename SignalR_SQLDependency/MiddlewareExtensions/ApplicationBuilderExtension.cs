using Microsoft.AspNetCore.Builder;
using SignalR_SQLTableDependency.SubscribeTableDependencies;

namespace SignalR_SQLTableDependency.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder app, string connectionString)
            where T : ISubscribeTableDependency
        {
            var serviceProvider = app.ApplicationServices;
            var service = serviceProvider.GetService<T>();
            service.SubscribeTableDependency(connectionString);
        }

        //public static void UseProductTableDependency(this IApplicationBuilder applicationBuilder, string connectionString)
        //{
        //    var serviceProvider = applicationBuilder.ApplicationServices;
        //    var service = serviceProvider.GetService<SubscribeProductTableDependency>();
        //    service.SubscribeTableDependency(connectionString);
        //}

        //public static void UseSaleTableDependency(this IApplicationBuilder applicationBuilder, string connectionString)
        //{
        //    var serviceProvider = applicationBuilder.ApplicationServices;
        //    var service = serviceProvider.GetService<SubscribeSaleTableDependency>();
        //    service.SubscribeTableDependency(connectionString);
        //}
    }
}
