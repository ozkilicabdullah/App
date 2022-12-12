using App.Core.Models;
using App.Core.Repositories;

namespace App.Repository.Repositories
{
    public class UserRegisterHistoryRepositoryImp : GenericRepositoryImp<UserRegisterHistory>, IUserRegisterHistoryRepository
    {
        public UserRegisterHistoryRepositoryImp(AppDbContext context) : base(context)
        {
        }
    }
}
