using MUMScrum.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUMScrum.Model.Model;
using MUMScrum.Repository;

namespace MUMScrum.Repository
{

    public class WorklogRepository : RepositoryBase<WorkLog>, IWorklogRepository
    {
        public WorklogRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
    public interface IWorklogRepository : IRepository<WorkLog>
    {

    }
}
