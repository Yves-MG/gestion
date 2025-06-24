using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Core.Entities;
using Gestion.Core.Interfaces;
using Gestion.Infrastructure.Data;

namespace Gestion.Infrastructure.Repositories
{
    public class RoomsRepository : EntityRepository<Rooms>, IRoomsRepository
    {
        public RoomsRepository(GestionDbContext context) : base(context)
        {
        }
    }
}
