using MUMScrum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Infrastructure.Core
{
    public interface IDatabaseFactory: IDisposable
    {
        ApplicationDbContext Get();
    }
}
