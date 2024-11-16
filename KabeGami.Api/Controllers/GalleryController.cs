using KabeGami.Application.Galleries.Commands.CreateGallery;
using KabeGami.Application.Galleries.Commands.DeleteGallery;
using KabeGami.Application.Galleries.Queries.GetGalleries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KabeGami.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GalleryController(
    ISender sender) 
        : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    [Route("CreateGallery")]
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
    [Route("DeleteGallery")]
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

    [HttpGet]
    [Route("GetGalleries")]
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

    //[HttpPut]
    //[Route("UpdateGalleryImageIds")]
    //public async Task<ActionResult> UpdateGalleryImageIds(
    //    Guid galleryGuid,
    //    List<Guid> imageGuids)
    //{
    //    var command = new UpdateGalleryImagesCommand(galleryGuid, imageGuids);
    //    var res = await _sender.Send(command);
    //    if (res.IsError)
    //    {
    //        return Problem(res.FirstError.ToString());
    //    }
    //    return Ok(res.Value);
    //}
}
