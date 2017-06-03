using MUMScrum.Infrastructure.Core;
using MUMScrum.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Repository.Repository
{
    public class ReleaseBacklogRepository : RepositoryBase<ReleaseBacklog>, IReleaseBacklogRepository
    {
        public ReleaseBacklogRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
    public interface IReleaseBacklogRepository : IRepository<ReleaseBacklog>
    {

    }
}
