using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Application.Dtos.DataTable;
using Gestion.Core.Entities;
using Gestion.Core.Interfaces;
using Gestion.Infrastructure.Data;
using Gestion.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Gestion.Infrastructure.Repositories
{
    public class ResidentRepository : EntityRepository<Resident>, IResidentRepository
    {
        private readonly GestionDbContext? GestionDbContext;
        public ResidentRepository(GestionDbContext context) : base(context)
        {
            GestionDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DataTable<List<Resident>>> GetAll(GetTableDataParam param)
        {
            if (GestionDbContext == null)
                throw new InvalidOperationException("DbContext is null");

            var page = param.Page > 0 ? param.Page : 1;
            var pageLength = param.PageLength > 0 ? param.PageLength : 20;

            var query = GestionDbContext.Resident.AsQueryable();

            // 🔍 Recherche
            /*  if (!string.IsNullOrWhiteSpace(param.Search?.Key) && !string.IsNullOrWhiteSpace(param.Search?.Value))
              {
                  query = query.Where(r =>
                      EF.Property<string>(r, param.Search.Key).ToLower().Contains(param.Search.Value.ToLower())
                  );
              }*/
            if (!string.IsNullOrWhiteSpace(param.Search?.Key) && !string.IsNullOrWhiteSpace(param.Search?.Value))
            {
                query = query.WhereStringContains(param.Search.Key, param.Search.Value);
            }

            // 🔎 Filtres
            if (param.Filters != null)
            {
                foreach (var filter in param.Filters)
                {
                    query = query.Where(r => EF.Property<string>(r, filter.Key) == filter.Value);
                }
            }

            // 🔁 Tri (avec fallback)
            if (param.IsOrderByAsc?.Any() == true)
            {
                var order = param.IsOrderByAsc.First();
                query = order.Value
                    ? query.OrderBy(r => EF.Property<object>(r, order.Key))
                    : query.OrderByDescending(r => EF.Property<object>(r, order.Key));
            }
            else
            {
                query = query.OrderBy(r => r.Id); // tri par défaut
            }

            // 📄 Pagination
            var totalCount = await query.CountAsync();
            var data = await query
                .Skip((page - 1) * pageLength)
                .Take(pageLength)
                .ToListAsync();
            data = data ?? new List<Resident>();

            //return new DataTable<Resident>(data, totalCount);
            return new DataTable<List<Resident>>([data], totalCount);
        }


    }
}
