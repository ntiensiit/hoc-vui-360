using Microsoft.AspNetCore.Mvc;

namespace HocVui360.Api.Controllers.V1;

[Route("api/[controller]")]
public class CartsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetByCurrentUser()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddCartItemToCart()
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCartItemInCart()
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveCartItemFromCart()
    {
        return Ok();
    }
}
