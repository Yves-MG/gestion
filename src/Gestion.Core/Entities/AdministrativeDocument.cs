using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Core.Entities
{
    public class AdministrativeDocument : BaseEntity
    {
        public string? DocumentName { get; set; } // e.g., "Birth Certificate", "Passport"
        public string? Description { get; set; } 
        public string? DocumentPath { get; set; } // Path to the document file
        public Guid? ResidentId { get; set; }
        public Resident? Resident { get; set; }
        public Guid? UploadedById { get; set; } // e.g., the name of the person who uploaded the document
        public User? UploadedBy { get; set; }
        public string? Type { get; set; } // CNI, CAF, AME, etc.
        public string? FilePath { get; set; }
        public DateTime? ExpiryDate { get; set; }
       
    }
    
}
