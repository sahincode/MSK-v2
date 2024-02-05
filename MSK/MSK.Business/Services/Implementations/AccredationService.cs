using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.Exceptions.OutOfRangesExceptions;
using MSK.Business.Exceptions;
using MSK.Business.InternalHelperServices;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MSK.Data.Repositories;
using MSK.Business.Services.Interfaces;
using MSK.Business.DTOs.AccredationModelDTOs;

namespace MSK.Business.Services.Implementations
{
    public class AccredationService :IAccredationService
    {
        private readonly IMapper _mapper;
        private readonly IAccredationRepository _accredationRepository;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/pdf/accredation";

        public AccredationService(IMapper mapper, IAccredationRepository accredationRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._accredationRepository = accredationRepository;
            this._env = env;
        }
        public async Task CreateAsync(AccredationCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            Accredation accredation = _mapper.Map<Accredation>(entity);
            if (entity.PDFEn.ContentType != "application/pdf") throw new OutOfRangeImageSizeException("PDFEn", "only pdf file!");
            if (entity.PDFRu.ContentType != "application/pdf") throw new OutOfRangeImageSizeException("PDFRu", "only pdf file!");
            if (entity.PDFAz.ContentType != "application/pdf") throw new OutOfRangeImageSizeException("PDFAz", "only pdf file!");


            if (entity.PDFEn.Length > 2097152) throw new OutOfRangeImageSizeException("PDFEn", "please upload less than 2 mg");
            if (entity.PDFRu.Length > 2097152) throw new OutOfRangeImageSizeException("PDFRu", "please upload less than 2 mg");
            if (entity.PDFAz.Length > 2097152) throw new OutOfRangeImageSizeException("PDFRu", "please upload less than 2 mg");

            accredation.PDFUrlEn = await FileHelper.SaveImage(rootPath, passPath, entity.PDFEn);
            accredation.PDFUrlRu = await FileHelper.SaveImage(rootPath, passPath, entity.PDFEn);
            accredation.PDFUrlAz = await FileHelper.SaveImage(rootPath, passPath, entity.PDFEn);

            await _accredationRepository.CreateAsync(accredation);
            await _accredationRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            Accredation accredation = await _accredationRepository.Get(a => a.Id == id);
            if (accredation == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, accredation.PDFUrlAz));
            File.Delete(Path.Combine(rootPath, passPath, accredation.PDFUrlRu));
            File.Delete(Path.Combine(rootPath, passPath, accredation.PDFUrlEn));

            _accredationRepository.Delete(accredation);


        }

        public async Task<Accredation> Get(Expression<Func<Accredation, bool>>? predicate, params string[]? includes)
        {
            return await _accredationRepository.Get(predicate, includes) is not null ?
                 await _accredationRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Accredation>> GetAll(Expression<Func<Accredation, bool>>? predicate, params string[]? includes)
        {
            return await _accredationRepository.GetAll(predicate, includes) is not null ?
                await _accredationRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<Accredation> GetById(int? id)
        {
            return await _accredationRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var accredation = await this.GetById(id);
            if (accredation == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            accredation.IsDeleted = !accredation.IsDeleted;
            await _accredationRepository.CommitAsync();

        }

        public async Task UpdateAsync(AccredationUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedAccredation = await _accredationRepository.Get(a => a.Id == entity.Id);
            if (updatedAccredation == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedAccredation = _mapper.Map(entity, updatedAccredation);


            if (entity.PDFAz is not null)
            {
                if (entity.PDFAz.ContentType != "application/pdf")
                {
                    throw new OutOfRangeImageSizeException("PDFAz", "only pdf file!");
                }
                if (entity.PDFAz.Length > 2097152)
                {
                    throw new OutOfRangeImageSizeException("PDFAz", "please upload less than 2 mg");
                }
                updatedAccredation.PDFUrlAz = await FileHelper.SaveImage(rootPath, passPath, entity.PDFAz);


            }

            if (entity.PDFEn is not null)
            {
                if (entity.PDFEn.ContentType != "application/pdf")
                {
                    throw new OutOfRangeImageSizeException("PDFEn", "only pdf file!");
                }
                if (entity.PDFEn.Length > 2097152)
                {
                    throw new OutOfRangeImageSizeException("PDFEn", "please upload less than 2 mg");
                }
                updatedAccredation.PDFUrlAz = await FileHelper.SaveImage(rootPath, passPath, entity.PDFEn);
            }

            if (entity.PDFRu is not null)
            {
                if (entity.PDFRu.ContentType != "application/pdf")
                {
                    throw new OutOfRangeImageSizeException("PDFRu", "only pdf file!");
                }
                if (entity.PDFRu.Length > 2097152)
                {
                    throw new OutOfRangeImageSizeException("PDFRu", "please upload less than 2 mg");
                }
                updatedAccredation.PDFUrlRu = await FileHelper.SaveImage(rootPath, passPath, entity.PDFRu);
            }

            await _accredationRepository.CommitAsync();

        }
    }
}
