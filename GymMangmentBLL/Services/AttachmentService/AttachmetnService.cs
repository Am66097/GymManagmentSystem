using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;




namespace GymMangmentBLL.Services.AttachmentService
{
    public class AttachmetnService : IAttachmentService
    {
        public AttachmetnService(IWebHostEnvironment webHost)
        {
            this._webHost = webHost;
        }
        private readonly string[] allowedExtentions = { ".jpg", ".jpeg", ".png" };
        private readonly long maxSizeForFile = 5 * 1024 * 1024; // 5MB
        private readonly IWebHostEnvironment _webHost;

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {



                if (folderName is null || file is null || file.Length == 0) return null;

                if (file.Length > maxSizeForFile) return null;

                var extention = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtentions.Contains(extention)) return null;

                var folderPath = Path.Combine(_webHost.WebRootPath, "images", folderName);
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + extention;

                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);

                return fileName;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Upload File To Folder {folderName} : {ex}");
                return null;
            }


        }
        public bool Delete(string fileName, string folderName)
        {

            try
            {
                if(string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName)) 
                    return false;

                var fullPath = Path.Combine(_webHost.WebRootPath,"images",folderName,fileName);
                if(File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Delete File With {fileName} : {ex}");
                return false;
            }
        }

    }
}
