using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MSK.Business.Services.Implementations;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

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
            services.AddScoped<ILegislationService, LegislationService>();
            services.AddScoped<IDecisionService, DecisionService>();
            services.AddScoped<ISubDecisionService, SubDecisionService>();
            services.AddScoped<ISubInstructionService, SubInstructionService>();
            services.AddScoped<IInstructionService, InstructonService>();
            services.AddScoped<IAccredationService ,AccredationService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IInfoService, InfoService>();
            services.AddScoped<ICalendarPlanService, CalendarPlanService>();
            services.AddScoped<IReferendumService, ReferendumService>();
            services.AddScoped<IVoterService, VoterService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IElectionService, ElectionService>();

            services.AddScoped<VoteControlService>();

            services.AddScoped<SignInManager<Voter>>();










        }
    }
}
