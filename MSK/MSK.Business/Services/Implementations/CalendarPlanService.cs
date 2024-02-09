using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.CalendarPlanModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Exceptions.SizeExceptions;
using MSK.Business.InternalHelperServices;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System.Linq.Expressions;

namespace MSK.Business.Services.Implementations
{
    public class CalendarPlanService :ICalendarPlanService
    {
        private readonly ICalendarPlanRepository _calendarPlanRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/pdf/calendarplan";

        public CalendarPlanService(IMapper mapper, ICalendarPlanRepository CalendarPlanRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._calendarPlanRepository = CalendarPlanRepository;
            this._env = env;
        }
        public async Task CreateAsync(CalendarPlanCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            CalendarPlan CalendarPlan = _mapper.Map<CalendarPlan>(entity);
            if (entity.Pdf.ContentType != "application/pdf")
            {
                throw new OutOfRangePdfSizeException("Pdf", "only pdf file!");
            }
            if (entity.Pdf.Length > 104857600)
            {
                throw new OutOfRangePdfSizeException("Pdf", "please upload less than 100 mg");
            }
            CalendarPlan.PdfUrl = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            await _calendarPlanRepository.CreateAsync(CalendarPlan);
            await _calendarPlanRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            CalendarPlan CalendarPlan = await _calendarPlanRepository.Get(a => a.Id == id);
            if (CalendarPlan == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, CalendarPlan.PdfUrl));
            _calendarPlanRepository.Delete(CalendarPlan);


        }

        public async Task<CalendarPlan> Get(Expression<Func<CalendarPlan, bool>>? predicate, params string[]? includes)
        {
            return await _calendarPlanRepository.Get(predicate, includes) is not null ?
                 await _calendarPlanRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<CalendarPlan>> GetAll(Expression<Func<CalendarPlan, bool>>? predicate, params string[]? includes)
        {
            return await _calendarPlanRepository.GetAll(predicate, includes) is not null ?
                await _calendarPlanRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<CalendarPlan> GetById(int? id)
        {
            return await _calendarPlanRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var CalendarPlan = await this.GetById(id);
            if (CalendarPlan == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            CalendarPlan.IsDeleted = !CalendarPlan.IsDeleted;
            await _calendarPlanRepository.CommitAsync();

        }

        public async Task UpdateAsync(CalendarPlanUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedCalendarPlan = await _calendarPlanRepository.Get(a => a.Id == entity.Id);
            if (updatedCalendarPlan == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedCalendarPlan = _mapper.Map(entity, updatedCalendarPlan);


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
                updatedCalendarPlan.PdfUrl = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            }

            await _calendarPlanRepository.CommitAsync();

        }
    }
}
