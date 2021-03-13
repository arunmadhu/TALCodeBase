using Microsoft.Extensions.DependencyInjection;
using PremiumDomain.Infrastructure;
using PremiumDomain.Infrastructure.Repository;

namespace PremiumDomain.Services
{
    public static class ServiceStartUp
    {
        public static void AddBussinessLogic(this IServiceCollection services)
        {
            //service layer
            services.AddTransient<IPremiumService, PremiumService>();

            //infrastructure layer
            services.AddTransient<IOccupationRepository, OccupationRepository>();

        }
    }
}
