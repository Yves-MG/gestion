
using System.Reflection.Metadata;
using Gestion.Application.Dtos.Residents;
using Gestion.Application.Services.Interfaces;
using Gestion.Core.Entities;
using Gestion.Core.Interfaces;
namespace Gestion.Application.Services.Implementations
{
    public class ResidentService : IResidentService
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IResidentRepository _residentRepository;

        public ResidentService(IResidentRepository residentRepository, IRoomsRepository roomsRepository)
        {
            _residentRepository = residentRepository;
            _roomsRepository = roomsRepository;
        }

        public async Task CreateResidentAsync(CreateResidentDto residentDto)
        {
            Rooms? room = await _roomsRepository.GetByIdAsync(Guid.Parse(residentDto.RoomId));

            if (room == null)
            {
                throw new KeyNotFoundException("Room not found.");
            }
            Resident resident = new Resident
            {
                Id = Guid.NewGuid(),
                FirstName = residentDto.FirstName,
                LastName = residentDto.LastName?? string.Empty,
                Email = residentDto.Email,
                Nationality= "Francais",
                AdministrativeStatus = "Asile",
                Genre = residentDto.Genre,
                PhoneNumber = residentDto.Telephone,
                BirthDate = residentDto.DateOfBirth,
                EntryDate = residentDto.DateOfDayBegin,
                ExitDate = null,
                SocialUserId = null,
                RoomId = Guid.Parse(residentDto.RoomId),
                Room = room,
            };

            await _residentRepository.AddAsync(resident);
            await _residentRepository.SaveChangesAsync();
        }
    }
}
