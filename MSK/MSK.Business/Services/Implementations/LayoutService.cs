using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Data.Configurations;
using System.Net.WebSockets;

namespace MSK.Business.Services.Implementations
{
    public class LayoutService
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IElectionService _electionService;

        public LayoutService(ISettingService settingService,
            IHttpContextAccessor httpContextAccessor, UserManager<User> userManager
            ,IElectionService electionService)
        {
            this._settingService = settingService;
            this._httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
            this._electionService = electionService;
        }
        public async Task<IEnumerable<SettingGetDto>> GetSettings()
        {
            var settingService = await _settingService.GetAllAsync(s => s.IsDeleted == false);
            return settingService;
        }
        public async Task<User> GetUser()
        {
            User user = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }
            return user;
        }
        public async Task<Dictionary<string,Array>> CalculateElectionsVotes()
        {
            int electionVotes = 0;
            var elections =await  _electionService.GetAll(null ,"Candidates");
            List<Election> eList = elections.ToList();
            int[] votesArrey = new int[eList.Count];
            string[] electionNames = new string[eList.Count]; 
            for(int i=0;i<eList.Count;i++)
            {
                foreach(var candiate in eList[i].Candidates)
                {
                     electionVotes = electionVotes + candiate.VotedCount;
                }
                votesArrey[i] = electionVotes;
                electionNames[i]= eList[i].Name;

            }
            Dictionary<string, Array> arrays = new Dictionary<string, Array>()
            {
                {"ElectionVotes" ,votesArrey},
                {"NameOfElections" ,electionNames},


            };


            return arrays;

        }
        public async Task<Dictionary<string, Array>> CalculateCandiateVotes(int id)
        {
            var election = await _electionService.Get(e=>e.Id==id, "Candidates");
         
            double[] votesArrey = new double[election.Candidates.Count];
            string[] candidateNames = new string[election.Candidates.Count];
            for (int i = 0; i < election.Candidates.Count; i++)
            {
                
                    votesArrey[i] = election.Candidates[i].VotedCount;
                
                candidateNames[i] = election.Candidates[i].FullName;

            }
            Dictionary<string, Array> arrays = new Dictionary<string, Array>()
            {
                {"CandidateVotes" ,votesArrey},
                {"NameOfCandidates" ,candidateNames},


            };


            return arrays;

        }
    }
}
