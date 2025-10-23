using HocVui360.Api.Controllers.V1.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HocVui360.Api.Controllers.V1;

[Route("api/[controller]")]
public class OpeningSchedulesController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetComingOpeningCourseDates()
    {
        var query = from schedule in _parameter.IdentityAppDbContext.OpeningSchedules
                    join featuredCourse in _parameter.IdentityAppDbContext.FeaturedCourses
                    on schedule.FeaturedCourseId equals featuredCourse.Id
                    where schedule.OpeningDate >= DateTime.Now
                    select new ComingOpeningCourseItemModel()
                    {
                        CommingOpeningCourseId = schedule.Id,
                        CommingOpeningCourseName = featuredCourse.FeaturedCourseName,
                        OpeningDate = schedule.OpeningDate,
                        FeaturedCourseId = featuredCourse.Id,
                    };

        var models = await query.ToListAsync();

        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComingOpeningCourseDateDetails([FromRoute] string id)
    {
        var comingOpeningCourseDate = await _parameter.IdentityAppDbContext.OpeningSchedules
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        var model = new ComingOpeningCourseDetailModel();

        return Ok(model);
    }
}
