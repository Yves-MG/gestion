using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Application.Dtos.DataTable;
using Gestion.Core.Entities;


namespace Gestion.Core.Interfaces
{
    public  interface IResidentRepository : IEntityRepository<Resident>
    {
        Task<DataTable<List<Resident>>> GetAll(GetTableDataParam param);
    }
}
