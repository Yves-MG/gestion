using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Core.Entities
{
    public class Courrier: BaseEntity
    {
       
        public string? Subject { get; set; }
        public string? Status { get; set; } // e.g., Sent, Received, Pending
        public Guid? ResidentId { get; set; }
        public Resident? Resident { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string? DeliverySignaturePath { get; set; }
        public Guid? DeliveryPersonId { get; set; } // e.g., the name of the person who delivered the mail
        public User? DeliveryPerson { get; set; } 

    }
}
