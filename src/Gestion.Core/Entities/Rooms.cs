using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Core.Entities
{
    public class Rooms: BaseEntity
    {
        public required string RoomNumber { get; set; }
        public required string RoomType { get; set; }
        public required Guid CreatedUserId { get; set; }
        public required User CreatedUser { get; set; }
      
        public string? Description { get; set; }
        public ICollection<Resident> Residents { get; set; } = new List<Resident>();
   
    }
}
