using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Gestion.Core.Entities;

namespace Gestion.Infrastructure.Seed
{
    public class FakeDataResidentGenerator
    {
        public static Faker<Resident> ResidentFaker => new Faker<Resident>()
        .RuleFor(r => r.FirstName, f => f.Name.FirstName())
        .RuleFor(r => r.LastName, f => f.Name.LastName())
        .RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber())
        .RuleFor(r => r.BirthDate, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))  // Un résident adulte
        .RuleFor(r => r.Email, f => f.Internet.Email())
        .RuleFor(r => r.Address, f => f.Address.FullAddress())
        .RuleFor(r => r.City, f => f.Address.City())
        .RuleFor(r => r.Genre, f => f.PickRandom("Male", "Female"))
        .RuleFor(r => r.Nationality, f => f.Address.Country())
        .RuleFor(r => r.EntryDate, f => f.Date.Past(1))
        .RuleFor(r => r.ExitDate, f => f.Date.Future(1))
        .RuleFor(r => r.AdministrativeStatus, f => f.PickRandom("Active", "Inactive", "Pending"))
        .RuleFor(r => r.ProfilePhotoPath, f => f.Image.PicsumUrl())
        .RuleFor(r => r.RoomId, f =>Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"))  // Ici tu pourrais utiliser des GUID valides si tu as des chambres dans ta base
        .RuleFor(r => r.UserAddId, f => null)  // Si tu souhaites associer un utilisateur créateur
        .RuleFor(r => r.SocialUserId, f => null);  // Si tu as des utilisateurs sociaux
                                                             // Générer une liste de résidents
        public static List<Resident> GenerateResidents(int count)
        {
            return ResidentFaker.Generate(count);
        }
    }
}
