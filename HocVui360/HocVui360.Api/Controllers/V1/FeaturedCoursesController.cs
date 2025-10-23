using HocVui360.Api.Controllers.V1.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HocVui360.Api.Controllers.V1;

[Route("api/[controller]")]
public class FeaturedCoursesController : BaseController
{
    [HttpGet("Active")]
    public async Task<IActionResult> GetActiveFeaturedCourses()
    {
        var query = _parameter.IdentityAppDbContext.FeaturedCourses.AsQueryable();

        query = query.Where(x => x.IsActive);

        query = query
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt);

        var models = await query
            .Select(x => new FeaturedCourseItemModel()
            {
                FeaturedCourseId = x.Id,
                FeaturedCourseName = x.FeaturedCourseName,
            })
            .ToListAsync();

        return Ok(models);
    }
}
