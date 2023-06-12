using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
namespace BrokenGrenade.Web.App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UploadController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<UploadController> _logger;
    public UploadController(IConfiguration configuration, ILogger<UploadController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    public async Task Save(IList<IFormFile> uploadFiles)
    {
        try
        {
            foreach (var file in uploadFiles)
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.FileName));
                var filePath = Path.Combine("wwwroot",
                    _configuration.GetValue<string>("FileStoragePath") ?? throw new InvalidOperationException(),
                    fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException());
                if (System.IO.File.Exists(filePath))
                    continue;

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                Response.Headers.Add("name", fileName);
                Response.StatusCode = 200;
                Response.ContentType = "application/json; charset=utf-8";
                
                _logger.LogInformation("File {FileName} uploaded", fileName);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while uploading files");
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = e.Message;
        }
    }
}
