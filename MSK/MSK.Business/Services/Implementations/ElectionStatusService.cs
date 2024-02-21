using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSK.Core.enums;
using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Implementations
{
    internal class ElectionStatusService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;



        public ElectionStatusService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;


        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAndCloseElections();

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        private async Task CheckAndCloseElections()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var electionsToOpen = _appDbContext.Elections.Where(e => e.ElectionStatus == ElectionStatus.Pending && e.StartDate <= DateTime.UtcNow.AddHours(4)).ToList();
                foreach (var electionToOpen in electionsToOpen)
                {
                    OpenElection(electionToOpen);
                }
                var electionsToClose = _appDbContext.Elections.Where
                    (e => e.ElectionStatus == Core.enums.ElectionStatus.Open && e.StartDate.AddMinutes(15) <= DateTime.UtcNow.AddHours(4)).ToList();
                foreach (var election in electionsToClose)
                {
                    CloseElection(election);
                }
                await _appDbContext.SaveChangesAsync();
            }
        }
        private async void CloseElection(Election election)
        {
            election.ElectionStatus = ElectionStatus.Closed;
            
        }
        private async void OpenElection(Election election)
        {
            election.ElectionStatus = ElectionStatus.Open;
            election.StartDate = DateTime.UtcNow.AddHours(4);
            
        }
    }
}
