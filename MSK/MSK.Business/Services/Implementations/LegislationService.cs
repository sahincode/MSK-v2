using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.HistoryModelDTOs;
using MSK.Business.DTOs.LegislationModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Exceptions.OutOfRangesExceptions;
using MSK.Business.Exceptions.SizeExceptions;
using MSK.Business.InternalHelperServices;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Implementations
{
    public class LegislationService : ILegislationService
    {
        private readonly ILegislationRepository _legislationRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/pdf/legislation";

        public LegislationService(IMapper mapper, ILegislationRepository legislationRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._legislationRepository = legislationRepository;
            this._env = env;
        }
        public async Task CreateAsync(LegislationCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            Legislation Legislation = _mapper.Map<Legislation>(entity);
            if (entity.Pdf.ContentType != "application/pdf")
            {
                throw new OutOfRangePdfSizeException("Image", "only pdf file!");
            }
            if (entity.Pdf.Length > 104857600)
            {
                throw new OutOfRangePdfSizeException("Image", "please upload less than 100 mg");
            }
            Legislation.PdfUrl = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            await _legislationRepository.CreateAsync(Legislation);
            await _legislationRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            Legislation legislation = await _legislationRepository.Get(a => a.Id == id);
            if (legislation == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, legislation.PdfUrl));
            _legislationRepository.Delete(legislation);


        }

        public async Task<Legislation> Get(Expression<Func<Legislation, bool>>? predicate, params string[]? includes)
        {
            return await _legislationRepository.Get(predicate, includes) is not null ?
                 await _legislationRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Legislation>> GetAll(Expression<Func<Legislation, bool>>? predicate, params string[]? includes)
        {
            return await _legislationRepository.GetAll(predicate, includes) is not null ?
                await _legislationRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<Legislation> GetById(int? id)
        {
            return await _legislationRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var legislation = await this.GetById(id);
            if (legislation == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            legislation.IsDeleted = !legislation.IsDeleted;
            await _legislationRepository.CommitAsync();

        }

        public async Task UpdateAsync(LegislationUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedLegislation = await _legislationRepository.Get(a => a.Id == entity.Id);
            if (updatedLegislation == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedLegislation = _mapper.Map(entity, updatedLegislation);


            if (entity.Pdf is not null)
            {
                if (entity.Pdf.ContentType != "image/jpeg" && entity.Pdf.ContentType != "image/png")
                    throw new OutOfRangeImageSizeException("Image", "only pdf file!");
                if (entity.Pdf.Length > 104857600)
                    throw new OutOfRangeImageSizeException("Image", "please upload less than 100 mg");
                updatedLegislation.PdfUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Pdf);
            }

            await _legislationRepository.CommitAsync();

        }
    }
}
