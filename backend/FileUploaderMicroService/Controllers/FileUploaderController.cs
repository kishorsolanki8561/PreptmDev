using CommonService.JWT;
using FileUploaderMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.CommonModel;
using System;

namespace FileUploaderMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileUploaderController : ControllerBase
    {
        private readonly IFileUploaderService _fileUploader;
        public FileUploaderController(IFileUploaderService fileUploader)
        {
            _fileUploader = fileUploader;
        }
        #region old code
        [HttpPost]
        public object FileUpload(IFormFile file, string path = "", string filename = "")
        {
            try
            {
                if (string.IsNullOrEmpty(filename))
                {
                    filename = DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString() + DateTime.UtcNow.Millisecond.ToString() + System.IO.Path.GetExtension(file.FileName);
                }
                if (string.IsNullOrEmpty(path))
                {
                    path = "/tempfile";
                }
                return _fileUploader.SaveFile(file, path, filename);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet]
        public object RemoveFile(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                    return _fileUploader.RemoveFile(path);
                else return false;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region new code
        [HttpPost]
        public object Upload(List<IFormFile> file, bool isThumbnail = true)
        {
            return _fileUploader.FileUploadForLinux(file, isThumbnail);
        }
        [HttpPost]
        public object DeleteFile(List<string> path)
        {
            return _fileUploader.DeleteFile(path);
        }

        [HttpGet]
        public object GetFiles(string path)
        {
            return _fileUploader.GetFilesPath(path);
        }

        [HttpGet]
        public object CreateNewPath(string tableName, string column, string PIdColumn)
        {
            return _fileUploader.CreateNewPath(tableName, column, PIdColumn);
        }

        [HttpGet]
        public object GenerateThumbnailByDirectories(string folderName, string fileName, bool isTransf= false)
        {
            return _fileUploader.GenerateThumbnailByDirectories(folderName, fileName, isTransf);
        }
        #endregion
    }
}
