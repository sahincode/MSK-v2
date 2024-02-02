using Microsoft.AspNetCore.Http;

namespace MSK.Business.InternalHelperServices
{
    public class FileHelper
    {
        public async static Task<string> SaveImage(string rootPath, string passPath, IFormFile image)
        {
            var folderImage = Path.Combine(rootPath, passPath);
            string imageName = null;
            if (!Directory.Exists(folderImage))
            {
                Directory.CreateDirectory(folderImage);

            }
            imageName = image.FileName.Length > 64 ?
            Guid.NewGuid().ToString() + image.FileName.Substring(image.FileName.Length - 64, 64).Replace(" ", "")
                : Guid.NewGuid().ToString() + image.FileName.Replace(" ", "");
            var fileFullPath = Path.Combine(folderImage, imageName);

            using (var FileStream = new FileStream(fileFullPath, FileMode.Create))
            {
                await image.CopyToAsync(FileStream);
            }

            return imageName;
        }
        public async static Task<string> SaveVideo(string filePath, string passPath, IFormFile video)
        {

            var FolderVideo = Path.Combine(filePath, passPath);
            string videoName = null;
            if (!Directory.Exists(FolderVideo))
            {

                Directory.CreateDirectory(FolderVideo);
            }
            videoName = video.FileName.Length > 64 ?
                Guid.NewGuid().ToString() + video.FileName.Substring(video.FileName.Length - 64, 64).Replace(" ", "")
                : Guid.NewGuid().ToString() + video.FileName.Replace(" ", "");

            var fileFullPath = Path.Combine(FolderVideo, videoName);

            using (var FileStream = new FileStream(fileFullPath, FileMode.Create))
            {
                await video.CopyToAsync(FileStream);
            }

            return videoName;
        }
    }
}
