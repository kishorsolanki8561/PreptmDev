using DBInfrastructure.ConfigModel.Translation.DBInterface;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInfrastructure
{
    public interface IDBPreptmContext: DBInterfaceDTO
    {
        DbContext DbContext { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
