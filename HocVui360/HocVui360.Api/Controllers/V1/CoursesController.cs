using HocVui360.Api.Controllers.V1.Models.Requests;
using HocVui360.Api.Controllers.V1.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HocVui360.Api.Controllers.V1;

[Route("api/[controller]")]
public class CoursesController : BaseController
{
    [HttpGet("TrainingProgram/{id}")]
    public async Task<IActionResult> GetByTrainingProgram([FromRoute] string id)
    {
        var query = _parameter.IdentityAppDbContext.TrainingPrograms.AsQueryable();

        query = query.Where(x => x.Id == id);

        query = query
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt);

        var models = await query
            .SelectMany(x => x.Courses)
            .Select(x => new CourseItemModel()
            {
                CourseId = x.Id,
                CourseName = x.CourseName,
                SeoName = x.SeoName,
            })
            .ToListAsync();

        return Ok(models);
    }

    [HttpGet]
    public async Task<IActionResult> GetByCriteria([FromQuery] CourseCriteriaModel criteriaModel)
    {
        var query = _parameter.IdentityAppDbContext.Courses.AsQueryable();

        if (!string.IsNullOrEmpty(criteriaModel.CourseName))
        {
            query = query.Where(x => x.CourseName.Contains(criteriaModel.CourseName));
        }

        if (criteriaModel.Start.HasValue)
        {
            query = query.Skip(criteriaModel.Start.Value);
        }

        if (criteriaModel.Length.HasValue)
        {
            query = query.Take(criteriaModel.Length.Value);
        }

        var models = await query
            .Select(x => new CourseItemModel()
            {
                CourseId = x.Id,
                CourseName = x.CourseName,
                SeoName = x.SeoName,
            })
            .ToListAsync();

        return Ok(models);
    }

    [HttpGet("FeaturedCourse/{id}")]
    public async Task<IActionResult> GetByFeaturedCourse([FromRoute] string id)
    {
        var query = _parameter.IdentityAppDbContext.FeaturedCourses.AsQueryable();

        query = query.Where(x => x.Id == id);

        var models = await query
            .SelectMany(x => x.Courses)
            .Select(x => new CourseItemModel()
            {
                CourseId = x.Id,
                CourseName = x.CourseName,
                SeoName = x.SeoName,
            })
            .ToListAsync();

        return Ok(models);
    }

    [HttpGet("{seoName}")]
    public async Task<IActionResult> GetBySeoName([FromRoute] string seoName)
    {
        var course = await _parameter.IdentityAppDbContext.Courses
            .Where(x => x.SeoName == seoName)
            .SingleOrDefaultAsync();

        var model = new CourseDetailModel();

        return Ok(model);
    }
}
