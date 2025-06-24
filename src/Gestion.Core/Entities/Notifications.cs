using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Core.Entities
{
    public class Notifications : BaseEntity
    {
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
      
        public Guid? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
        public Guid ResidentId { get; set; }
        public required Resident Resident { get; set; }
    }
    
}
