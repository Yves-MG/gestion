using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Application.Dtos.Residents;

namespace Gestion.Application.Services.Interfaces
{
    
    public interface IResidentService
    {
        Task CreateResidentAsync(CreateResidentDto residentDto);
        
    }
}
