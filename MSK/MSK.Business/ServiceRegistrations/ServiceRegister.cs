using Microsoft.Extensions.DependencyInjection;
using MSK.Business.Services.Implementations;
using MSK.Business.Services.Interfaces;

namespace Pigga.Business.ServiceRegistrations
{
    public static class ServiceRegister
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<LayoutService>();
            services.AddScoped<IHomeSlideService, HomeSlideService>();
            services.AddScoped<IPressNewService, PressNewService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<INationalAttributeService, NationalAttributeService>();






        }
    }
}
