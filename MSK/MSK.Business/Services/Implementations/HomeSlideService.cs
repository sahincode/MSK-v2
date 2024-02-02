using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Exceptions.OutOfRangesExceptions;
using MSK.Business.InternalHelperServices;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System.Linq.Expressions;

namespace MSK.Business.Services.Implementations
{
    public class HomeSlideService :IHomeSlideService
    {
        private readonly IMapper _mapper;
        private readonly IHomeSlideRepository _homeSlideRepository;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/img/homeslide";

        public HomeSlideService(IMapper mapper, IHomeSlideRepository homeSlideRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._homeSlideRepository= homeSlideRepository;
            this._env = env;
        }
        public async Task CreateAsync(HomeSlideCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            HomeSlide homeSlide = _mapper.Map<HomeSlide>(entity);
            if (entity.Image.ContentType != "image/jpeg" && entity.Image.ContentType != "image/png")
            {
                throw new OutOfRangeImageSizeException("Image", "only png or jpeg file!");
            }
            if (entity.Image.Length > 2097152)
            {
                throw new OutOfRangeImageSizeException("Image", "please upload less than 2 mg");
            }
            homeSlide.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            await _homeSlideRepository.CreateAsync(homeSlide);
            await _homeSlideRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            HomeSlide HomeSlide = await _homeSlideRepository.Get(a => a.Id == id);
            if (HomeSlide == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, HomeSlide.ImageUrl));
            _homeSlideRepository.Delete(HomeSlide);


        }

        public async Task<HomeSlide> Get(Expression<Func<HomeSlide, bool>>? predicate, params string[]? includes)
        {
            return await _homeSlideRepository.Get(predicate, includes) is not null ?
                 await _homeSlideRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<HomeSlide>> GetAll(Expression<Func<HomeSlide, bool>>? predicate, params string[]? includes)
        {
            return await _homeSlideRepository.GetAll(predicate, includes) is not null ?
                await _homeSlideRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<HomeSlide> GetById(int? id)
        {
            return await _homeSlideRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var HomeSlide = await this.GetById(id);
            if (HomeSlide == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            HomeSlide.IsDeleted = !HomeSlide.IsDeleted;
            await _homeSlideRepository.CommitAsync();

        }

        public async Task UpdateAsync(HomeSlideUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedHomeSlide = await _homeSlideRepository.Get(a => a.Id == entity.Id);
            if (updatedHomeSlide == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedHomeSlide = _mapper.Map(entity, updatedHomeSlide);


            if (entity.Image is not null)
            {
                if (entity.Image.ContentType != "image/jpeg" && entity.Image.ContentType != "image/png")
                {
                    throw new OutOfRangeImageSizeException("Image", "only png or jpeg file!");
                }
                if (entity.Image.Length > 2097152)
                {
                    throw new OutOfRangeImageSizeException("Image", "please upload less than 2 mg");
                }
                updatedHomeSlide.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            }

            await _homeSlideRepository.CommitAsync();

        }

    }
}
