namespace TestProject.WebApi.Rest;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Repositories;
using TestProject.WebApi.Services;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly IFileManagementService _fileManagementService;
    private readonly IFileRepository _fileRepository;

    public FilesController(
        IFileManagementService fileManagementService,
        IFileRepository fileRepository
    )
    {
        _fileManagementService = fileManagementService;
        _fileRepository = fileRepository;
    }

    // GET api/files/download/fileId
    [HttpGet("download/{fileId}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(Guid fileId)
    {
        try
        {
            var file = await _fileRepository.GetById(fileId);

            if (file == null)
            {
                return NotFound();
            }

            var fileStream = await _fileManagementService.GetFileAsync(fileId);

            if (fileStream == null)
            {
                return NotFound();
            }

            // Create a MemoryStream to hold the file
            var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);

            // Set response headers for download
            Response.Headers.Append("Content-Disposition", $"attachment; filename={file.FileName}{file.FileExtension}");
            Response.Headers.Append("Content-Type", "application/octet-stream");
            Response.Headers.Append("Content-Length", memoryStream.Length.ToString());

            // Return the resulting file
            return File(memoryStream.ToArray(), "application/octet-stream");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST api/files/upload
    [HttpPost("upload")]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromForm] IFormFile file)
    {
        try
        {
            var savedFile = await _fileManagementService.SaveFileAsync(file);

            if (savedFile == null)
            {
                return BadRequest("File not saved");
            }

            return Ok(savedFile);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}