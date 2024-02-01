using Microsoft.Extensions.DependencyInjection;
using MSK.Core.Repositories;
using MSK.Data.Repositories;

namespace MSK.Data.RepositoryRegistrations
{
    public static class RepositoryRegister
    {
        public static void RegisterRepos( this IServiceCollection services)
        {

            services.AddScoped<ISettingRepository, SettingRepository>();

        }
    }
}
