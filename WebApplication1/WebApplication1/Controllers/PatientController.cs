using Microsoft.AspNetCore.Mvc;

using WebApplication1.DTOs;

using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]

[ApiController]

public class PatientController : ControllerBase

{

    private readonly IHospitalService _hospitalService;

    public PatientController(IHospitalService hospitalService)

    {

        _hospitalService = hospitalService;

    }

    [HttpGet]

    public async Task<IActionResult> GetPatients([FromQuery] string? search)

    {

        var patients = await _hospitalService.GetPatientsAsync(search);

        return Ok(patients);

    }

    [HttpPost("{pesel}/beds")]

    public async Task<IActionResult> AssignBed(string pesel, [FromBody] BedAssignmentPostDto dto)

    {

        if (!ModelState.IsValid)

        {

            return BadRequest(ModelState);

        }

        var result = await _hospitalService.AssignBedAsync(pesel, dto);
        

        if (!result)

        {

            return NotFound(new { message = "Patient not found or no available beds found for the specified period." });

        }

        return StatusCode(StatusCodes.Status201Created, new { message = "Bed assigned successfully." });

    }

}