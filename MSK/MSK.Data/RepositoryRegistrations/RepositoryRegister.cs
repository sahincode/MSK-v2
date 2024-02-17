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
            services.AddScoped<IHomeSlideRepository,HomeSlideRepository>();
            services.AddScoped<IPressNewRepository, PressNewRepository>();
            services.AddScoped<IHistorRepository,HistorRepository>();
            services.AddScoped<INationalAttributeRepository, NationalAttributeRepository>();
            services.AddScoped<IAccredationRepository, AccredationRepository>();
            services.AddScoped<ILegislationRepository, LegislationRepository>();
            services.AddScoped<IDecisionRepository, DecisionRepository>();
            services.AddScoped<ISubDecisionRepository, SubDecisionRepository>();
            services.AddScoped<ISubInstructionRepository, SubInstructionRepository>();
            services.AddScoped<IInstructionRepository, InstructionRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IInfoRepository, InfoRepository>();
            services.AddScoped<ICalendarPlanRepository, CalendarPlanRepository>();
            services.AddScoped<IReferendumRepository, ReferendumRepository>();
            services.AddScoped<IVoterRepository, VoterRepository>();

















        }
    }
}
