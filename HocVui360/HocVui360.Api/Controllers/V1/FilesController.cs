using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HocVui360.Api.Controllers.V1;

[Route("api/[controller]")]
public class FilesController : BaseController
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var file = await _parameter.IdentityAppDbContext.Files
            .FirstOrDefaultAsync(x => x.Name == name);
        if (file == null || file.Data == null)
            return NoContent();

        var stream = new MemoryStream(file.Data);
        return new FileStreamResult(stream, file.ContentType)
        {
            FileDownloadName = file.Name
        };
    }
}
