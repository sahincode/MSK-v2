using Microsoft.Extensions.Hosting;
using MSK.Core.enums;
using MSK.Core.Models;
using MSK.Core.Repositories;
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
        private readonly IElectionRepository _electionRepository;

        public ElectionStatusService( IServiceProvider serviceProvider ,IElectionRepository electionRepository)
        {
            this._serviceProvider = serviceProvider;
            this._electionRepository = electionRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAndCloseElections();

                await Task.Delay(TimeSpan.FromMilliseconds(30) , stoppingToken);
            }
        }
        private async Task CheckAndCloseElections()
        {
            var electionsToOpen = _electionRepository.Table.Where(e => e.ElectionStatus == ElectionStatus.Pending && e.StartDate <= DateTime.UtcNow.AddHours(4)).ToList();
             foreach(var electionToOpen in electionsToOpen)
            {
                OpenElection(electionToOpen);
            }
            var electionsToClose = _electionRepository.Table.Where
                (e => e.ElectionStatus == Core.enums.ElectionStatus.Open && e.StartDate <= DateTime.UtcNow.AddHours(4)).ToList();
                foreach(var election in electionsToClose)
            {
                CloseElection(election);
            }
             await _electionRepository.CommitAsync();
        }
        private void CloseElection(Election election)
        {
            election.ElectionStatus = ElectionStatus.Closed;
            _electionRepository.CommitAsync();
        }
        private void OpenElection(Election election)
        {
            election.Status = ElectionStatus.Open;
            election.StartDate = DateTime.UtcNow;
            _electionRepository.CommitAsync();
        }
    }
}
