using System.Collections.Generic;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.Domain.Services
{
    public interface ISnackService
    {
        IEnumerable<SnackDto> GetAllSnacks();
    }

    internal class SnackService : ISnackService
    {
        private readonly IRepository<Snack> snackRepository;

        public SnackService(IRepository<Snack> snackRepository)
        {
            this.snackRepository = snackRepository;
        }

        public IEnumerable<SnackDto> GetAllSnacks()
        {
            var snacks = snackRepository.FindAll();
            return Map.These(snacks).ToAListOf<SnackDto>();
        }
    }
}