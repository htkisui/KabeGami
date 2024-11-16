using KabeGami.Application.Homes.Commands.CreateKabe;
using KabeGami.Application.Homes.Commands.DeleteKabe;
using KabeGami.Application.Homes.Commands.SetDefaultKabe;
using KabeGami.Application.Homes.Queries.GetHome;
using KabeGami.Application.Homes.Queries.GetKabes;
using KabeGami.Application.Wallpapers.Commands.StartupWallpaper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KabeGami.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HomeController(
    ISender sender) 
        : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    [Route("CreateKabe")]
    public async Task<ActionResult> CreateKabe(
        string name,
        string combination,
        string cronSchedule,
        string galleryName)
    {
        //var command = new CreateKabeCommand(
        //    name,
        //    combination,
        //    cronSchedule,
        //    galleryName
        //    );
        //var res = await _sender.Send(command);
        //if (res.IsError)
        //{
        //    return Problem(res.FirstError.ToString());
        //}
        //return Ok(res.Value);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteKabe")]
    public async Task<ActionResult> DeleteKabe(Guid kabeGuid)
    {
        //var command = new DeleteKabeCommand(kabeGuid);
        //var res = await _sender.Send(command);
        //if (res.IsError)
        //{
        //    return Problem(res.FirstError.ToString());
        //}
        //return Ok(res.Value);
        return Ok();
    }

    [HttpGet]
    [Route("GetHome")]
    public async Task<ActionResult> GetHome()
    {
        var query = new GetHomeQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpGet]
    [Route("GetKabes")]
    public async Task<ActionResult> GetKabes()
    {
        var query = new GetKabesQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpPut]
    [Route("SetDefaultKabe")]
    public async Task<ActionResult> SetDefaultKabe(Guid kabeGuid)
    {
        //var command = new SetDefaultKabeCommand(kabeGuid);
        //var res = await _sender.Send(command);
        //if (res.IsError)
        //{
        //    return Problem(res.FirstError.ToString());
        //}
        //return Ok(res.Value);
        return Ok();
    }

    [HttpGet]
    [Route("StartupWallpaper")]
    public async Task<ActionResult> StartupWallpaper()
    {
        var command = new StartupWallpaperCommand();
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }
        return Ok(res.Value);
    }
}
