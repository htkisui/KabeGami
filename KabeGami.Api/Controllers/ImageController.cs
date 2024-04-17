using KabeGami.Application.Images.Commands.CreateImage;
using KabeGami.Application.Images.Commands.DeleteImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KabeGami.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class ImageController(
    ISender sender)
        : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<ActionResult> CreateImages(string directoryPath, string imageCategory, bool isSFW)
    {
        string[] files = Directory.GetFiles(directoryPath);
        foreach (string file in files)
        {
            var command = new CreateImageCommand(file, imageCategory, isSFW);
            var res = await _sender.Send(command);
            if (res.IsError)
            {
                return Problem(res.FirstError.ToString());
            }
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteImage(Guid imageGuid)
    {
        var command = new DeleteImageCommand(imageGuid);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            return Problem(res.IsError.ToString());
        }

        return Ok(res.Value);
    }
}
