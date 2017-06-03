using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Infrastructure.Core
{
    public interface IUnitOfWork
    {
        void Commit();
        bool TransactionCommit();
        Task<bool> CommitAsync();
    }
}
