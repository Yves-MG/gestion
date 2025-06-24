using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Application.Dtos.DataTable
{
    public class GetTableDataParam
    {
        public int Page { get; set; } = 1;
        public int PageLength { get; set; } = 20;
        public List<string> Fields { get; set; } = new();
        public Dictionary<string, string> Filters { get; set; } = new();
        public SearchParam Search { get; set; } = new();
        public List<OrderByParam> IsOrderByAsc { get; set; } = new();
    }
}
