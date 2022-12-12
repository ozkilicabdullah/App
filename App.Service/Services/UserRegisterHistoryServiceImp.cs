
using App.Core.Models;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;

namespace App.Service.Services
{
    public class UserRegisterHistoryServiceImp : GenericServiceImp<UserRegisterHistory>, IUserRegisterHistoryService
    {
        public UserRegisterHistoryServiceImp(IGenericRepository<UserRegisterHistory> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
