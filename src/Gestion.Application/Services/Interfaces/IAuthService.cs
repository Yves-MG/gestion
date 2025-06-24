using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Application.Dtos.Users.Request;
using Gestion.Application.Dtos.Users.Response;
using Gestion.Core.Services.Implementations;

namespace Gestion.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<AuthResp>> RegisterAsync(Register register);
        Task<ServiceResult<AuthResp>> LoginAsync(Login login);
        Task<ServiceResult<AuthResp>> RefreshTokenAsync(string refreshToken);
    }
}
