using KabeGami.Application.KabeGamiCores.Commands.SetGalleryIdToKabeGamiCore;
using KabeGami.Application.KabeGamiCores.Commands.SetWallpaper;
using KabeGami.Application.KabeGamiCores.Commands.StartupWallpaper;
using KabeGami.Application.KabeGamiCores.Queries.GetKabeGamiCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KabeGami.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class KabeGamiCoreController(
    ISender sender) 
        : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult> GetKabeGamiCore()
    {
        var query = new GetKabeGamiCoreQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpGet("Startup")]
    public async Task<ActionResult> Startup()
    {
        var command = new StartupWallpaperCommand();
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpPost("ChangeWallpaper")]
    public async Task<ActionResult> ChangeWallpaper(Guid subGalleryGuid)
    {
        var command = new ChangeWallpaperCommand(subGalleryGuid);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpPut("SetGalleryId")]
    public async Task<ActionResult> SetGalleryId(Guid galleryGuid)
    {
        var command = new SetGalleryIdToKabeGamiCoreCommand(galleryGuid);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }
}
