using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.ContactModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Implementations
{
    public class ContactService :IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
      
        

        public ContactService(IContactRepository contactRepository,
            IMapper mapper
            )
        {
            this._contactRepository = contactRepository;
            this._mapper = mapper;
            
        }

        public async Task CreateAsync(ContactCreateDto entity)
        {


            Contact contact= _mapper.Map<Contact>(entity);

            await _contactRepository.CreateAsync(contact);
            await _contactRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {


            var contact = await this.GetById(id);
            if (contact is null) throw new NullEntityException("", $"Contact model does not exist in database with {id} id");

            _contactRepository.Delete(contact);

            await _contactRepository.CommitAsync();
        }

        public async Task<Contact> Get(Expression<Func<Contact, bool>>? predicate, params string[]? includes)
        {
            return await _contactRepository.Get(predicate, includes) is not null ?
                await _contactRepository.Get(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Contact>> GetAll(Expression<Func<Contact
            , bool>>? predicate, params string[]? includes)
        {
            return await _contactRepository.GetAll(predicate, includes) is not null ?
               await _contactRepository.GetAll(predicate, includes) :
               throw new EntityNotFoundException($"The entity with the ID equal to" +
               $" {predicate} was not found in the database.");
        }





        public async Task<Contact> GetById(int? id)
        {
            return await _contactRepository.Get(c => c.Id == id);
        }
        public async Task ToggleDelete(int id)
        {


            var contact = await this.GetById(id);
            if (contact is null) throw new NullEntityException("", $"Contact model does not exist in database with {id} id");
            contact.IsDeleted = !contact.IsDeleted;


            await _contactRepository.CommitAsync();
        }

        public async Task UpdateAsync(ContactUpdateDto contactUpdateDto)
        {
           
            var contact = await this.GetById(contactUpdateDto.Id);

            if (contact is null) throw new NullEntityException("", "Contact model does not exist in database with {id} id");


            contact.Info= contactUpdateDto.Info;


            await _contactRepository.CommitAsync();
        }
    }
}
