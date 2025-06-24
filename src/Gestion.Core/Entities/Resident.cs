using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Core.Entities
{
    public class Resident:BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public  string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Genre { get; set; }
        public required string Nationality { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public required string AdministrativeStatus { get; set; }
        public string? ProfilePhotoPath { get; set; }
        public Guid? RoomId { get; set; }
        public Rooms? Room { get; set; }
        public ICollection<Courrier>? Courriers { get; set; }
        public ICollection<AdministrativeDocument>? AdministrativeDocuments { get; set; }
        public ICollection<AdministrativeTask>? AdministrativeTasks { get; set; }
        public ICollection<WashingMachine>? WashingMachines { get; set; }
        public ICollection<Absence>? Absences { get; set; }
        public ICollection<Notifications>? Notifications { get; set; }

        public Guid? UserAddId { get; set; }
        public User? UserAdd { get; set; }

        public Guid? SocialUserId { get; set; }
        public User? SocialUser { get; set; }
    }
}
