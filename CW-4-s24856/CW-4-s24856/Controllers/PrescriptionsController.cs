using CW_4_s24856.DTOs;
using CW_4_s24856.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_4_s24856.Controllers;

[ApiController]
[Route("[Controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionsController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequestDto dto)
    {
        try
        {
            await _service.AddPrescriptionAsync(dto);
            return Ok("Prescription added.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}