using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.Application.Dtos.Residents
{
    public class CreateResidentDto
    {
        public required string FirstName { get; set; }
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public required string Genre { get; set; }
        public string? Telephone { get; set; }
        public string? OthersInfo { get; set; }
        public DateTime Language { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? StatusMatrimonial { get; set; }
        public string? StatusResident { get; set; }
        public bool IsActive { get; set; }
        public string? StatusProfessionnel { get; set; }
        public bool IsHeberge { get; set; }
        public string StatusStatus { get; set; } = string.Empty;
        public string StatusDescription { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime DateOfDayBegin { get; set; }

        public string RoomId { get; set; } = string.Empty;
    }
}
