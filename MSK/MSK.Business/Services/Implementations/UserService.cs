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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async  Task<User> Get(Expression<Func<User, bool>>? predicate, params string[]? includes)
        {
            return await _userRepository.Get(predicate, includes) is not null ?
                 await _userRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async  Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>>? predicate, params string[]? includes)
        {
            return await _userRepository.GetAll(predicate, includes) is not null ?
               await _userRepository.GetAll(predicate, includes) :
               throw new EntityNotFoundException($"The entity with the ID equal to" +
               $" {predicate} was not found in the database.");
        }

        
    }
}
