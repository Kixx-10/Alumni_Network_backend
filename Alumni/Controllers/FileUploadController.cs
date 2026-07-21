using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alumni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileUploadController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;

        public FileUploadController(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        [HttpPost("upload-multiple")]
        public async Task<IActionResult> UploadMultipleImages(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            var uploadedUrls = new List<string>();

            try
            {
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        using var stream = file.OpenReadStream();

                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(file.FileName, stream),
                            Folder = "alumni_network_uploads",
                            PublicId = Guid.NewGuid().ToString()
                        };

                        // Cloudinary သို့ Upload တင်ခြင်း
                        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                        if (uploadResult.Error != null)
                        {
                            return StatusCode(500, new { message = "Cloudinary Upload Failed", error = uploadResult.Error.Message });
                        }

                        // App/DB ထဲမှာ အသုံးပြုရန် Secure HTTPS Cloud URL ကို ယူပါ
                        uploadedUrls.Add(uploadResult.SecureUrl.ToString());
                    }
                }
                string commaSeparatedUrls = string.Join(",", uploadedUrls);
                return Ok(new { mediaUrls = commaSeparatedUrls });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Upload failed.", error = ex.Message });
            }
        }
    }
}



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

//            // 1. Linux Container မှာ Safe ဖြစ်မယ့် WebRootPath သို့မဟုတ် Current Folder ကို ယူပါ
//            var rootPath = !string.IsNullOrEmpty(_env.WebRootPath)
//                ? _env.WebRootPath
//                : Path.Combine(AppContext.BaseDirectory, "wwwroot");

//            var uploadsFolder = Path.Combine(rootPath, "uploads");

//            try
//            {
//                // 2. Folder မရှိရင် Create လုပ်ပါ
//                if (!Directory.Exists(uploadsFolder))
//                {
//                    Directory.CreateDirectory(uploadsFolder);
//                }

//                foreach (var file in files)
//                {
//                    if (file != null && file.Length > 0)
//                    {
//                        var ext = Path.GetExtension(file.FileName);
//                        if (string.IsNullOrEmpty(ext)) ext = ".jpg";

//                        var fileName = $"{Guid.NewGuid()}{ext}";
//                        var filePath = Path.Combine(uploadsFolder, fileName);

//                        // 3. FileStream ကို explicit FileMode & FileAccess ဖြင့် Safe ဖွင့်ပါ
//                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
//                        {
//                            await file.CopyToAsync(stream);
//                        }

//                        var relativePath = $"/uploads/{fileName}";
//                        uploadedUrls.Add(relativePath);
//                    }
//                }

//                string commaSeparatedUrls = string.Join(",", uploadedUrls);
//                return Ok(new { mediaUrls = commaSeparatedUrls });
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                return StatusCode(500, new { message = "Server Permission Error during upload.", error = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Upload failed.", error = ex.Message });
//            }
//        }
//    }
//}

