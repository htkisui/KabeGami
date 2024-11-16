using KabeGami.Application.Images.Commands.CreateImage;
using KabeGami.Application.Images.Commands.CreateImages;
using KabeGami.Application.Images.Commands.DeleteImage;
using KabeGami.Application.Images.Commands.DeleteImages;
using KabeGami.Application.Images.Commands.EmptyImageTrash;
using KabeGami.Application.Images.Queries.GetImages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KabeGami.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImageController(
    ISender sender)
        : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    [Route("/AddImage")]
    public async Task<ActionResult> AddImage(string imageSourcePath)
    {
        //var command = new CreateImageCommand(imageSourcePath);
        //var res = await _sender.Send(command);
        //if (res.IsError)
        //{
        //    return Problem(res.FirstError.ToString());
        //}
        //return Ok(res.Value);
        return Ok();
    }

    [HttpPost]
    [Route("/AddImages")]
    public async Task<ActionResult> AddImages(string imagesSourcePath)
    {
        //var command = new CreateImagesCommand(imagesSourcePath);
        //var res = await _sender.Send(command);
        //if (res.IsError)
        //{
        //    return Problem(res.FirstError.ToString());
        //}
        //return Ok(res.Value);
        return Ok();
    }

    [HttpDelete]
    [Route("/EmptyImageTrash")]
    public async Task<ActionResult> EmptyImageTrash()
    {
        var command = new EmptyImageTrashCommand();
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }
        return Ok(res.Value);
    }

    [HttpGet]
    [Route("/GetImages")]
    public async Task<ActionResult> GetImages()
    {
        var query = new GetImagesQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }
        return Ok(res.Value);
    }

    [HttpDelete]
    [Route("/RemoveImage")]
    public async Task<ActionResult> RemoveImage(Guid imageGuid)
    {
        //var command = new DeleteImageCommand(imageGuid);
        //var res = await _sender.Send(command);
        //if (res.IsError)
        //{
        //    return Problem(res.FirstError.ToString());
        //}
        //return Ok(res.Value);
        return Ok();
    }

    [HttpDelete]
    [Route("/RemoveImages")]
    public async Task<ActionResult> RemoveImage(List<Guid> imageGuids)
    {
        //var command = new DeleteImagesCommand(imageGuids);
        //var res = await _sender.Send(command);
        //if (res.IsError)
        //{
        //    return Problem(res.FirstError.ToString());
        //}
        //return Ok(res.Value);
        return Ok();

    }
}
