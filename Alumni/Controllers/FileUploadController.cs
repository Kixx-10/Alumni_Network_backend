using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public FileUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload-multiple")]
        public async Task<IActionResult> UploadMultipleImages(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            var uploadedUrls = new List<string>();

            var baseRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(baseRoot, "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var currentHost = Request?.Host.Value ?? "localhost";

            if (currentHost.Contains("localhost") || currentHost.Contains("127.0.0.1"))
            {
                currentHost = "192.168.1.7:5123";
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    var fileUrl = $"{Request?.Scheme ?? "http"}://{currentHost}/uploads/{fileName}";
                    uploadedUrls.Add(fileUrl);
                }
            }

            string commaSeparatedUrls = string.Join(",", uploadedUrls);
            return Ok(new { mediaUrls = commaSeparatedUrls });
        }
    }
}