using System.Collections.Generic;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Extensions;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.Domain.Services
{
    public interface IUserService
    {
        IEnumerable<ViewUserDto> GetAllUsers();
        ViewUserDto GetUser(string userName);
    }

    internal class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ViewUserDto> GetAllUsers()
        {
            var users = repository.FindAll<User>();
            return Map.These(users).ToAListOf<ViewUserDto>();
        }

        public ViewUserDto GetUser(string userName)
        {
            return GetAllUsers().Find(u => u.Name.Equals(userName));
        }
    }
}