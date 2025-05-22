using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;

[Route("api/[controller]")]
[ApiController]
public class VGovStatControllerAPI : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VGovStatControllerAPI(ApplicationDbContext context)
    {
        _context = context;
    }

  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VGovStatAPI>>> GetVGovStats()
    {
        var stats = await _context.VGovStat.ToListAsync();
        return Ok(stats);
    }

  
    [HttpGet("GetById")]
    public async Task<ActionResult<VGovStatAPI>> GetById([FromQuery] decimal id)
    {
        var stat = await _context.VGovStat.FirstOrDefaultAsync(v => v.GovId == id);

        if (stat == null)
        {
            return NotFound(new { message = "Record not found." });
        }

        return Ok(stat);
    }
}
