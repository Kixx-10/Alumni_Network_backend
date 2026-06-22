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

            // 🚀 ၁။ လမ်းကြောင်းကို wwwroot/uploads ဖြစ်အောင် ကွက်တိပြင်ဆင်ခြင်း
            var baseRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(baseRoot, "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // 🚀 ၂။ မင်းရဲ့ ကွန်ပျူတာ IP ကို ယူခြင်း (ဒါမှ Flutter က လှမ်းဖတ်နိုင်မှာပါ)
            // တကယ်လို့ Request.Host က localhost ဖြစ်နေရင် မင်းရဲ့ IPv4 IP အဖြစ် ပြောင်းပေးဖို့ စီစဉ်ထားပါတယ်
            var currentHost = Request.Host.ToString();
            if (currentHost.Contains("localhost") || currentHost.Contains("127.0.0.1"))
            {
                currentHost = "192.168.60.76:5123"; // 🎯 မိမိ .NET Core HTTP Port နံပါတ်ကို သေချာထည့်ပေးပါဦး (ဥပမာ- :5123)
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

                    // 🚀 ၃။ ဖုန်းကနေ လှမ်းဖတ်လို့ရမယ့် URL လမ်းကြောင်းအမှန် တည်ဆောက်ခြင်း
                    var fileUrl = $"{Request.Scheme}://{currentHost}/uploads/{fileName}";
                    uploadedUrls.Add(fileUrl);
                }
            }

            string commaSeparatedUrls = string.Join(",", uploadedUrls);
            return Ok(new { mediaUrls = commaSeparatedUrls });
        }
    }
}