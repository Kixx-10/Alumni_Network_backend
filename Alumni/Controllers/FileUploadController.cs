//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Alumni.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class FileUploadController : ControllerBase
//    {
//        private readonly IWebHostEnvironment _env;
//        public FileUploadController(IWebHostEnvironment env)
//        {
//            _env = env;
//        }

//        [HttpPost("upload-multiple")]
//        public async Task<IActionResult> UploadMultipleImages(List<IFormFile> files)
//        {
//            if (files == null || files.Count == 0)
//                return BadRequest("No files uploaded.");

//            var uploadedUrls = new List<string>();

//            var baseRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
//            var uploadsFolder = Path.Combine(baseRoot, "uploads");

//            if (!Directory.Exists(uploadsFolder))
//                Directory.CreateDirectory(uploadsFolder);

//            foreach (var file in files)
//            {
//                if (file != null && file.Length > 0)
//                {
//                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName ?? ".jpg");
//                    var filePath = Path.Combine(uploadsFolder, fileName);

//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await file.CopyToAsync(stream);
//                    }
//                    var relativePath = $"/uploads/{fileName}";
//                    uploadedUrls.Add(relativePath);
//                }
//            }
//            string commaSeparatedUrls = string.Join(",", uploadedUrls);
//            return Ok(new { mediaUrls = commaSeparatedUrls });
//        }
//    }
//}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

            // 1. Linux Container မှာ Safe ဖြစ်မယ့် WebRootPath သို့မဟုတ် Current Folder ကို ယူပါ
            var rootPath = !string.IsNullOrEmpty(_env.WebRootPath)
                ? _env.WebRootPath
                : Path.Combine(AppContext.BaseDirectory, "wwwroot");

            var uploadsFolder = Path.Combine(rootPath, "uploads");

            try
            {
                // 2. Folder မရှိရင် Create လုပ်ပါ
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        var ext = Path.GetExtension(file.FileName);
                        if (string.IsNullOrEmpty(ext)) ext = ".jpg";

                        var fileName = $"{Guid.NewGuid()}{ext}";
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        // 3. FileStream ကို explicit FileMode & FileAccess ဖြင့် Safe ဖွင့်ပါ
                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var relativePath = $"/uploads/{fileName}";
                        uploadedUrls.Add(relativePath);
                    }
                }

                string commaSeparatedUrls = string.Join(",", uploadedUrls);
                return Ok(new { mediaUrls = commaSeparatedUrls });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(500, new { message = "Server Permission Error during upload.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Upload failed.", error = ex.Message });
            }
        }
    }
}
