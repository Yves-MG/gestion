using Gestion.Application.Dtos.DataTable;
using Gestion.Application.Dtos.Residents;
using Gestion.Application.Dtos.Users.Request;
using Gestion.Application.Services.Interfaces;
using Gestion.Core.Entities;
using Gestion.Core.Interfaces;
using Gestion.Core.Services.Implementations;
using Gestion.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentService _residentService;
       // private readonly IEntityRepository<Resident> _residentRepository;
        private readonly IResidentRepository _residentRepository;

        public ResidentController(IResidentService residentService,
            IResidentRepository residentRepository)
            //IEntityRepository<Resident> residentRepository)
        {
            _residentService = residentService;
            _residentRepository = residentRepository;
        }

        /// <summary>
        [HttpPost("create")]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResident([FromBody] CreateResidentDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Fix: Ensure RoomId is not null or empty before converting to Guid  
                if (string.IsNullOrEmpty(registerDto.RoomId))
                {
                    return BadRequest(new { message = "RoomId cannot be null or empty." });
                }
                await _residentService.CreateResidentAsync(registerDto);

                return Ok(new { message = "Resident created successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log l'erreur ici si tu as un logger  
                return StatusCode(500, new { message = "An error occurred while creating the resident.", details = ex.Message });
            }
        }

        [HttpPost("all")]
        //[Authorize]

        public async Task<ActionResult<DataTable<List<Resident>>>> GetAll([FromBody] GetTableDataParam param)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _residentRepository.GetAll(param);
                return Ok(result); // ✅ Ici on encapsule correctement
            }
            catch (Exception ex)
            {
                // Log l'erreur ici si tu as un logger  
                return StatusCode(500, new { message = "An error occurred while retrieving residents.", details = ex.Message });
            }
            /*  // Uncomment this block if you want to use the commented code
           /* try
            {
                List<Resident> residents = await _residentRepository.GetAllAsync();
                return ServiceResult<List<Resident>>.Success("Residents retrieved successfully", residents);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<Resident>>.Failure($"An error occurred: {ex.Message}");
            }*/
        }
    }
}
