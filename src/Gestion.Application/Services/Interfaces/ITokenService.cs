using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Core.Entities;

namespace Gestion.Core.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User userName);
        string GenerateRefreshToken();

    }
}
