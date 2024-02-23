using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSK.Business.Services.Implementations;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Data.DAL;

namespace Pigga.Business.ServiceRegistrations
{
    public static class ServiceRegister
    {
        public static void RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
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
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IAiService, AiService>();

           
            services.AddHostedService<ElectionStatusService>();
            
            services.AddScoped<VoteControlService>();
            services.AddScoped<SignInManager<Voter>>();
            services.AddScoped<SignInManager<User>>();

            services.Configure<SMTPConfigModel>(configuration.GetSection("SMTPConfig"));

        }
    }
}
