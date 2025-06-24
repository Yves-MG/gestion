using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gestion.Core.Entities
{
    public class DataTable<T> 
    {
        public DataTable() { }
        public DataTable(List<T> data, int totalCount)
        {
            Data = data ?? new List<T>();
            Total = totalCount;
        }

        [JsonProperty("total")]

        public int Total { get; set; }


        [JsonProperty("data")]

        public List<T>? Data { get; set; } = new List<T>();
    }
}
