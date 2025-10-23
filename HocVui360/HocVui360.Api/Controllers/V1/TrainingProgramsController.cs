using HocVui360.Api.Controllers.V1.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HocVui360.Api.Controllers.V1;

[Route("api/[controller]")]
public class TrainingProgramsController : BaseController
{
    [HttpGet("Active")]
    public async Task<IActionResult> GetActiveTrainingPrograms()
    {
        var query = _parameter.IdentityAppDbContext.TrainingPrograms.AsQueryable();

        query = query.Where(x => x.IsActive);

        query = query
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt);

        var models = await query
            .Select(x => new TrainingProgramItemModel()
            {
                TrainingProgramId = x.Id,
                TrainingProgramName = x.TrainingProgramName,
            })
            .ToListAsync();

        return Ok(models);
    }
}
