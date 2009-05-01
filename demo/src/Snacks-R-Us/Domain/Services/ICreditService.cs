using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Etensions;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.Domain.Services
{
    public interface ICreditService
    {
        ViewCreditDto GetCreditsForCurrentUser();
        ViewCreditDto GetCreditsForUser(string userId);
        void AddCredit(AddCreditDto addCreditDto);
    }

    internal class CreditService : ICreditService
    {
        private readonly IRepository repository;

        public CreditService(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewCreditDto GetCreditsForCurrentUser()
        {
            var user = repository.Find<User>(Query.CurrentUser);
            return Map.This(user).ToA<ViewCreditDto>();
        }

        public ViewCreditDto GetCreditsForUser(string userId)
        {
            var user = repository.Get<User>(userId.ToLong());
            return Map.This(user).ToA<ViewCreditDto>();
        }

        public void AddCredit(AddCreditDto addCreditDto)
        {
            var user = repository.Get<User>(addCreditDto.UserId.ToLong());
            user.AddCredits(addCreditDto.Amount.ToDouble());
        }
    }
}