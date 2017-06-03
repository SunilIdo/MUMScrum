using MUMScrum.Infrastructure.Core;
using MUMScrum.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Service.Service.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<LoginUser> _usersRepository;
        public UsersService()
        {

        }
        public UsersService(IUnitOfWork UnitOfWork, IRepository<LoginUser> usersRepository)
        {
            _unitOfWork = UnitOfWork;
            _usersRepository = usersRepository;
        }
        public void Create(LoginUser user)
        {
            if (user == null) throw new ArgumentNullException("LoginUser");
            this._usersRepository.Add(user);
            this._unitOfWork.Commit();
        }

        public void Delete(LoginUser loginUser)
        {
            if (loginUser == null) throw new ArgumentNullException("LoginUser");
            this._usersRepository.Delete(loginUser);
            this._unitOfWork.Commit();
        }

        public IQueryable<LoginUser> GetAll()
        {
            return this._usersRepository.GetAll().OrderBy(x => x.Id).AsQueryable();
        }

        public LoginUser GetById(int Id)
        {
            return this._usersRepository.GetById(Id);
        }

        public void Update(LoginUser loginUser)
        {        
            this._usersRepository.Update(loginUser);
            this._unitOfWork.Commit();
        }
    }
}
