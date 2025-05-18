using CW_4_s24856.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_4_s24856.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PatientsController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        try
        {
            var result = await _service.GetPatientDetailsAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}