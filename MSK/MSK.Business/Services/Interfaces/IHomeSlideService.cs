using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Core.Models;
using System.Linq.Expressions;

namespace MSK.Business.Services.Interfaces
{
    public interface IHomeSlideService
    {
        Task CreateAsync(HomeSlideCreateDto entity);
        Task UpdateAsync(HomeSlideUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<HomeSlide> GetById(int? id);
        Task<HomeSlide> Get(Expression<Func<HomeSlide, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<HomeSlide>> GetAll(Expression<Func<HomeSlide, bool>>? predicate, params string[]? include);
    }
}
