using MUMScrum.Infrastructure.Core;
using MUMScrum.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Repository.Repository
{
    public class UserstoryRepository: RepositoryBase<UserStory>,IUserstoryRepository
    {
        public UserstoryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
    public interface IUserstoryRepository : IRepository<UserStory>
    {

    }
}
