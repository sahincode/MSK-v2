using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.InfoModelDTOs;
using MSK.Business.Exceptions.SizeExceptions;
using MSK.Business.Exceptions;
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
    public class InfoService :IInfoService
    {
        private readonly IInfoRepository _infoRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/pdf/info";

        public InfoService(IMapper mapper, IInfoRepository InfoRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._infoRepository = InfoRepository;
            this._env = env;
        }
        public async Task CreateAsync(InfoCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            Info Info = _mapper.Map<Info>(entity);
            if (entity.Pdf.ContentType != "application/pdf")
            {
                throw new OutOfRangePdfSizeException("Pdf", "only pdf file!");
            }
            if (entity.Pdf.Length > 104857600)
            {
                throw new OutOfRangePdfSizeException("Pdf", "please upload less than 100 mg");
            }
            Info.PdfUrl = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            await _infoRepository.CreateAsync(Info);
            await _infoRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            Info Info = await _infoRepository.Get(a => a.Id == id);
            if (Info == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, Info.PdfUrl));
            _infoRepository.Delete(Info);


        }

        public async Task<Info> Get(Expression<Func<Info, bool>>? predicate, params string[]? includes)
        {
            return await _infoRepository.Get(predicate, includes) is not null ?
                 await _infoRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Info>> GetAll(Expression<Func<Info, bool>>? predicate, params string[]? includes)
        {
            return await _infoRepository.GetAll(predicate, includes) is not null ?
                await _infoRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<Info> GetById(int? id)
        {
            return await _infoRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var Info = await this.GetById(id);
            if (Info == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            Info.IsDeleted = !Info.IsDeleted;
            await _infoRepository.CommitAsync();

        }

        public async Task UpdateAsync(InfoUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedInfo = await _infoRepository.Get(a => a.Id == entity.Id);
            if (updatedInfo == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedInfo = _mapper.Map(entity, updatedInfo);


            if (entity.Pdf is not null)
            {
                if (entity.Pdf.ContentType != "application/pdf")
                {
                    throw new OutOfRangePdfSizeException("Pdf", "only pdf file!");
                }
                if (entity.Pdf.Length > 104857600)
                {
                    throw new OutOfRangePdfSizeException("Pdf", "please upload less than 100 mg");
                }
                updatedInfo.PdfUrl = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            }

            await _infoRepository.CommitAsync();

        }
    }
}
