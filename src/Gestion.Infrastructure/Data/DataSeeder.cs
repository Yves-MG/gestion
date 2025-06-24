using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Infrastructure.Seed;

namespace Gestion.Infrastructure.Data
{
    public class DataSeeder
    {
        private readonly GestionDbContext _context;
        public DataSeeder(GestionDbContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            Console.WriteLine("🟡 Seeding en cours...");
            //if (!_context.Resident.Any())
            //{
                var fakeResidents = FakeDataResidentGenerator.GenerateResidents(100);
                _context.Resident.AddRange(fakeResidents);
                await _context.SaveChangesAsync();
            //}
        }
    }
}
