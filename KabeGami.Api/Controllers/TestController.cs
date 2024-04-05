using KabeGami.Application.Images.Queries.GetImagesFromDirectory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KabeGami.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(ISender sender) 
    : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var command = new GetImagesFromDirectoryQuery(@"D:\Wallpaper\");
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }
}
