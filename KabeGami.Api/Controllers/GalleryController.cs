using KabeGami.Application.Galleries.Commands.AddImageIdsToSubGallery;
using KabeGami.Application.Galleries.Commands.CreateGallery;
using KabeGami.Application.Galleries.Commands.CreateSubGallery;
using KabeGami.Application.Galleries.Commands.DeleteGallery;
using KabeGami.Application.Galleries.Commands.DeleteSubGallery;
using KabeGami.Application.Galleries.Commands.SetSubGalleryIdToGallery;
using KabeGami.Application.Galleries.Queries.GetGalleries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KabeGami.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class GalleryController(
    ISender sender) 
        : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("Galleries")]
    public async Task<ActionResult> GetGalleries()
    {
        var query = new GetGalleriesQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpPost]
    public async Task<ActionResult> CreateGallery(string name)
    {
        var command = new CreateGalleryCommand(name);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteGallery(Guid galleryGuid)
    {
        var command = new DeleteGalleryCommand(galleryGuid);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpPut("SetSubGalleryId")]
    public async Task<ActionResult> SetSubGalleryId(
        Guid galleryGuid,
        Guid subGalleryGuid)
    {
        var command = new SetSubGalleryIdToGalleryCommand(galleryGuid, subGalleryGuid);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpPost("SubGallery")]
    public async Task<ActionResult> CreateSubGallery(
        Guid galleryGuid,
        string name,
        string shortcutKey,
        string cronSchedule)
    {
        var command = new CreateSubGalleryCommand(galleryGuid, name, shortcutKey, cronSchedule);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpDelete("SubGallery")]
    public async Task<ActionResult> DeleteSubGallery(
        Guid galleryGuid,
        Guid subGalleryGuid)
    {
        var command = new DeleteSubGalleryCommand(galleryGuid, subGalleryGuid);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

    [HttpPut("SubGallery/AddImageIds_alter")]
    public async Task<ActionResult> AddImages(Guid galleryGuid, Guid subGalleryGuid, string directoryPath)
    {
        var imageGuids = new List<Guid>();
        string[] files = Directory.GetFiles(directoryPath);
        foreach (string file in files)
        {
            imageGuids.Add(Guid.Parse(Path.GetFileNameWithoutExtension(file)));
        }

        var command = new AddImageIdsToSubGalleryCommand(
            galleryGuid,
            subGalleryGuid,
            imageGuids);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.FirstError.ToString());
        }

        return Ok(res.Value);
    }

}

