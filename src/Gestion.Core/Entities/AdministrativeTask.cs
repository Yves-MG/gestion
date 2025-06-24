using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Core.Entities
{
    public class AdministrativeTask:BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } // En cours / Terminée
        //public DateTime? DueDate { get; set; }
        public Guid ResidentId { get; set; }
        public required Resident Resident { get; set; }

        public Guid? AssignedToId { get; set; } // e.g., the name of the person to whom the task is assigned
        public User? AssignedTo { get; set; } // e.g., the name of the person to whom the task is assigned

    }
}
