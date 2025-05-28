using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Service;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromQuery] PrescriptionCreateDTO prescriptionData)
    {
        try
        {
            var prescription = await service.CreatePrescriptionAsync(prescriptionData);
            return Ok(prescription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}