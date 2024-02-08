using MSK.Business.DTOs.ContactModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateAsync(ContactCreateDto entity);
        Task UpdateAsync(ContactUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Contact> GetById(int? id);
        Task<Contact> Get(Expression<Func<Contact, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Contact>> GetAll(Expression<Func<Contact, bool>>? predicate, params string[]? include);
    }
}
