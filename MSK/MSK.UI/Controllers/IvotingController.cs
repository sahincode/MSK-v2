﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs;
using MSK.Business.DTOs.CandidateModelDTOs;
using MSK.Business.DTOs.VoterModelDTOs;
using MSK.Business.InternalHelperServices;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;
using System.Security.Claims;

namespace MSK.UI.Controllers
{
    public class IvotingController : Controller
    {
        private readonly IVoterService _voterService;
        private readonly IWebHostEnvironment _env;
        private readonly SignInManager<Voter> _signInManager;
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;

        public IvotingController(IVoterService voterService,
            IWebHostEnvironment env, SignInManager<Voter> signInManager,
            ICandidateService candidateService ,IMapper mapper)
        {
            this._voterService = voterService;
            this._env = env;
            this._signInManager = signInManager;
            this._candidateService = candidateService;
            this._mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VoterLoginDto voterLoginDto)
        {

            string rootPath = _env.WebRootPath;
            var voter = await _voterService.Get(v => v.FinCode == voterLoginDto.FinCode);
            if (voter is null)
            {
                ModelState.AddModelError("", "Please add valid information!");
                return View();
            }
            var options = new RestClientOptions("https://api.edenai.run/v2/image/face_compare");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiMTBlZjY0NTQtMDU3Yy00N2Q5LWEyNjUtZjcyMjZlODVjMDdkIiwidHlwZSI6ImFwaV90b2tlbiJ9.OWlMB10GKISoKwNeFE60AH2Dx6c2biPwWfTmEYdKxm8");


            string filePath1 = Path.Combine(rootPath, $"assets/img/voter/{voter.ImageUrl}");


            string tempFile = await FileHelper.SaveImage(rootPath, "assets/temp", voterLoginDto.Image);
            string filePath2 = Path.Combine(rootPath, "assets/temp", tempFile);
            request.AddFile("file1", filePath1);
            request.AddFile("file2", filePath1);

            request.AddParameter("response_as_dict", true);
            request.AddParameter("attributes_as_list", true);
            request.AddParameter("show_original_response", true);
            request.AddParameter("providers", "amazon");
            var response = client.Post(request);
            string jsonResponse = response?.Content;
            try
            {
                var jsonObject = JObject.Parse(jsonResponse);
                Console.WriteLine(jsonObject);
                var amazon = jsonObject["amazon"]?["original_response"] as JObject;
                var faceMatchesArray = amazon?["FaceMatches"] as JArray;

                if (faceMatchesArray != null && faceMatchesArray.Count > 0)
                {
                    // Get the first FaceMatch
                    var firstFaceMatch = faceMatchesArray[0];

                    // Extract the Similarity property
                    var similarity = firstFaceMatch?["Similarity"]?.Value<double>();
                    if (similarity < 95)
                    {
                        ModelState.AddModelError("", "Invalid credential!");
                        return View();
                    }
                    else if (similarity > 95)
                    {
                        var result = await _signInManager.PasswordSignInAsync(voter, voterLoginDto.FinCode, false, false);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", "Invalid credential!");
                            return View();
                        }
                        else
                        {

                            return RedirectToAction("vote");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }


            if (filePath2 is not null)
            {
                System.IO.File.Delete(filePath2);
            }

            return View();
        }
        [VoterAuthorize]
        public IActionResult Vote()
        {
            var candidates = _candidateService.GetAll(c => c.IsDeleted == false, null).Result.ToList();
            List<CandidateLayoutDto> candidateLayoutDtos = new List<CandidateLayoutDto>();

            if(candidates is not null)
            {
                foreach(var candidate in candidates)
                {
                    CandidateLayoutDto candidateLayoutDto = _mapper.Map<CandidateLayoutDto>(candidate);
                    candidateLayoutDtos.Add(candidateLayoutDto);

                }
            }
            return View(candidateLayoutDtos);
        }
    }
}
