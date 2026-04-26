using CommonService.Other;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ModelService.CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static CommonService.Other.UtilityManager;

namespace FileUploaderMicroService.Service
{

    public interface IFileUploaderService
    {
        #region old code
        string SaveFile(IFormFile formFile, string path = "", string filename = "");
        bool RemoveFile(string path);
        #endregion
        #region new code
        ServiceResponse<List<string>> FileUploadForLinux(List<IFormFile> aFormFile, bool isThumbnail);
        ServiceResponse<List<string>> FileUploadForWin(List<IFormFile> aFormFile, bool isThumbnail);

        ServiceResponse<object> DeleteFile(List<string> paths);
        ServiceResponse<List<string>> GetFilesPath(string path);
        object CreateNewPath(string tableName, string column, string PIdColumn);
        object GenerateThumbnailByDirectories(string folderName, string fileName, bool isTransf = false);
        #endregion
    }
    public class FileUploaderService : UtilityManager, IFileUploaderService
    {
        private IWebHostEnvironment _env;
        private readonly FileUploader _fileUploader;

        public FileUploaderService(IWebHostEnvironment env, FileUploader fileUploader)
        {
            _env = env;
            _fileUploader = fileUploader;
        }
        #region old code
        public string SaveFile(IFormFile formFile, string path = "", string filename = "")
        {
            try
            {
                return uploadFile(formFile, path, filename);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploaderService.cs", "SaveFile"));
                throw ex;
            }
        }

        public bool RemoveFile(string path)
        {
            try
            {
                return Removefile(path);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploaderService.cs", "Removefile"));
                throw ex;
            }
        }

        public string uploadFile(IFormFile aFormFile, string path = "", string fileName = "")
        {
            try
            {
                //string lDirectory = _env.WebRootPath;
                //string setPath = "/" + path.Trim() + "\\";
                //string lnewPath = _env.WebRootPath + "\\" + setPath;
                //if (!Directory.Exists(lDirectory))
                //    Directory.CreateDirectory(lDirectory);
                //if (!Directory.Exists(lnewPath))
                //    Directory.CreateDirectory(lnewPath);
                //using (var stream = new FileStream(lnewPath + (!string.IsNullOrEmpty(fileName) ? fileName : aFormFile.FileName), FileMode.Create))
                //{
                //    aFormFile.CopyToAsync(stream).GetAwaiter().GetResult();
                //    var savePath = "/" + setPath + (!string.IsNullOrEmpty(fileName) ? fileName : aFormFile.FileName);

                //    return savePath = savePath.Replace("\\", "/").Replace("//","/");
                //}

                string lDirectory = _env.WebRootPath;
                string setPath = "/" + path.Trim() + "\\";
                setPath = setPath.Replace("\\", "/").Replace("//", "/");
                string lnewPath = _env.WebRootPath + "\\" + setPath;
                lnewPath = lnewPath.Replace("\\", "/").Replace("//", "/");
                if (!Directory.Exists(lDirectory))
                    Directory.CreateDirectory(lDirectory);
                if (!Directory.Exists(lnewPath))
                    Directory.CreateDirectory(lnewPath);
                using (var stream = new FileStream(lnewPath + (!string.IsNullOrEmpty(fileName) ? fileName : aFormFile.FileName), FileMode.Create))
                {
                    aFormFile.CopyToAsync(stream).GetAwaiter().GetResult();
                    //var savePath = "/" + setPath + (!string.IsNullOrEmpty(fileName) ? fileName : aFormFile.FileName);
                    var savePath = setPath + (!string.IsNullOrEmpty(fileName) ? fileName : aFormFile.FileName);

                    return savePath;//= savePath.Replace("\\", "/").Replace("//","/");
                }
            }
            catch (Exception e)
            {
                Log.Error(e, CommonFunction.Errorstring("FileUploaderService.cs", "uploadFile"));
                throw e;
            }

        }
        private bool Removefile(string path)
        {
            try
            {
                string lDirectory = _env.WebRootPath + path;

                if (File.Exists(lDirectory))
                {
                    File.Delete(lDirectory);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploaderService.cs", "Removefile"));
                throw ex;
            }

        }

        #endregion

        #region new code
        public ServiceResponse<List<string>> FileUploadForLinux(List<IFormFile> aFormFile, bool isThumbnail)
        {
            try
            {
                StringBuilder meaages = new StringBuilder();
                StringBuilder erormeaages1 = new StringBuilder();
                meaages.Append("Images are corrupted -{");
                for (int i = 0; i < aFormFile.Count; i++)
                {
                    if (aFormFile[i].ContentType.Split("/")[1] !=  "svg+xml" && IsFormFileCorrupted(aFormFile[i]) && aFormFile[i].ContentType.Split("/")[0] == "image")
                    {
                        if (i > 0)
                            erormeaages1.Append(", ");
                        erormeaages1.Append(aFormFile[i].FileName);
                    }
                }
                if (erormeaages1 is not null && erormeaages1.Length > 0 && !string.IsNullOrEmpty(erormeaages1.ToString()))
                {
                    meaages.Append(erormeaages1.ToString());
                    meaages.Append("}");
                    return SetResultStatus<List<string>>(new List<string>(), meaages.ToString(), false, "", "", 0, StatusCodes.Status409Conflict);
                }

                List<string> paths = new List<string>();
                for (int i = 0; i < aFormFile.Count; i++)
                {
                    if(aFormFile[i].ContentType.Split("/")[1] == "svg+xml")
                        isThumbnail = false;
                    DateTime date = DateTime.Now;
                    string fileName = "";
                    string contentPath = _env.WebRootPath; //content
                    if (!Directory.Exists(contentPath))
                        Directory.CreateDirectory(contentPath);

                    string OriginalAttachment = "/" + "OriginalAttachment" + "\\";

                    string yearFolderPath = "/" + date.Year.ToString() + "\\"; // year 

                    if (!Directory.Exists(ConvertLinuxDirectoryPath(contentPath + yearFolderPath + OriginalAttachment)))
                        Directory.CreateDirectory(ConvertLinuxDirectoryPath(contentPath + yearFolderPath + OriginalAttachment));

                    string Th1200x628Path = contentPath + yearFolderPath + "/" + "Th1200x628" + "\\"; // Thumbnail 1200*628 

                    if (!Directory.Exists(ConvertLinuxDirectoryPath(Th1200x628Path)))
                        Directory.CreateDirectory(ConvertLinuxDirectoryPath(Th1200x628Path));

                    string Th360x180Path = contentPath + yearFolderPath + "/" + "Th360x180" + "\\"; // Thumbnail 1200*628 

                    if (!Directory.Exists(ConvertLinuxDirectoryPath(Th360x180Path)))
                        Directory.CreateDirectory(ConvertLinuxDirectoryPath(Th360x180Path));


                    string Th120x68Path = contentPath + yearFolderPath + "/" + "Th120x68" + "\\"; // Thumbnail 1200*628 

                    if (!Directory.Exists(ConvertLinuxDirectoryPath(Th120x68Path)))
                        Directory.CreateDirectory(ConvertLinuxDirectoryPath(Th120x68Path));

                    string rootPath = contentPath + yearFolderPath + OriginalAttachment;

                    string useFileName = isThumbnail ? Path.Combine(Path.GetDirectoryName(aFormFile[i].FileName.Trim()), Path.GetFileNameWithoutExtension(aFormFile[i].FileName.Trim())) + ".webp" : aFormFile[i].FileName.Trim();
                    var newUniquePath = GetUniqueFilePath(ConvertLinuxDirectoryPath(rootPath) + useFileName);

                    if (isThumbnail && aFormFile[i].ContentType.Split("/")[0] == "image")
                    {
                        fileName = Path.GetFileName(newUniquePath);
                        fileName = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName));
                        fileName = fileName + ".webp";
                    }
                    else
                    {
                        fileName = Path.GetFileName(newUniquePath);
                    }


                    using (var stream = new FileStream(ConvertLinuxDirectoryPath(rootPath) + fileName, FileMode.Create))
                    {
                        aFormFile[i].CopyToAsync(stream).GetAwaiter().GetResult();
                        string newPath = ConvertLinuxDirectoryPath(rootPath) + "/" + fileName;
                        var savePath = "/" + newPath;
                        fileName = savePath = savePath.Replace(contentPath, string.Empty).Replace("\\", "/").Replace("//", "/");
                    }


                    paths.Add(fileName);
                    if (isThumbnail && aFormFile[i].ContentType.Split("/")[0] == "image")
                    {
                        string originalImagePath = ConvertLinuxDirectoryPath(rootPath) + Path.GetFileName(fileName);

                        Th1200x628Path = ConvertLinuxDirectoryPath(Th1200x628Path) + Path.GetFileName(fileName);
                        Console.WriteLine(Th1200x628Path);
                        GenerateThumbnail(originalImagePath, Th1200x628Path, 1200, 628);

                        Th360x180Path = ConvertLinuxDirectoryPath(Th360x180Path) + Path.GetFileName(fileName);
                        Console.WriteLine(Th360x180Path);
                        GenerateThumbnail(originalImagePath, Th360x180Path, 360, 180);

                        Th120x68Path = ConvertLinuxDirectoryPath(Th120x68Path) + Path.GetFileName(fileName);
                        Console.WriteLine(Th120x68Path);
                        GenerateThumbnail(originalImagePath, Th120x68Path, 120, 68);
                    }
                }

                return SetResultStatus<List<string>>(paths, MessageStatus.FileUpload, true);
            }
            catch (Exception e)
            {
                Log.Error(e, CommonFunction.Errorstring("FileUploaderService.cs", "uploadFile"));
                return SetResultStatus<List<string>>(null, MessageStatus.Error, false, e.Message);
            }
        }

        public ServiceResponse<List<string>> FileUploadForWin(List<IFormFile> aFormFile, bool isThumbnail)
        {
            try
            {
                StringBuilder meaages = new StringBuilder();
                StringBuilder erormeaages1 = new StringBuilder();
                meaages.Append("Images are corrupted -{");
                for (int i = 0; i < aFormFile.Count; i++)
                {
                    if (IsFormFileCorrupted(aFormFile[i]) && aFormFile[i].ContentType.Split("/")[0] == "image")
                    {
                        erormeaages1.Append(aFormFile[i].FileName);
                    }
                }
                if (erormeaages1 is not null && erormeaages1.Length > 0 && !string.IsNullOrEmpty(erormeaages1.ToString()))
                {
                    meaages.Append(erormeaages1.ToString());
                    meaages.Append("}");
                    return SetResultStatus<List<string>>(new List<string>(), meaages.ToString(), false);
                }

                List<string> paths = new List<string>();
                for (int i = 0; i < aFormFile.Count; i++)
                {
                    DateTime date = DateTime.Now;
                    string fileName = "";
                    string contentPath = _env.WebRootPath; //content
                    if (!Directory.Exists(contentPath))
                        Directory.CreateDirectory(contentPath);
                    string OriginalAttachment = "/" + "OriginalAttachment" + "\\";
                    string yearFolderPath = "/" + date.Year.ToString() + "\\"; // year 

                    if (!Directory.Exists(contentPath + yearFolderPath + OriginalAttachment))
                        Directory.CreateDirectory(contentPath + yearFolderPath + OriginalAttachment);

                    string Th1200x628Path = contentPath + yearFolderPath + "Th1200x628" + "\\"; // Thumbnail 1200*628 

                    if (!Directory.Exists(Th1200x628Path))
                        Directory.CreateDirectory(Th1200x628Path);


                    string Th360x180Path = contentPath + yearFolderPath + "Th360x180" + "\\"; // Thumbnail 1200*628 

                    if (!Directory.Exists(Th360x180Path))
                        Directory.CreateDirectory(Th360x180Path);


                    string Th120x68Path = contentPath + yearFolderPath + "Th120x68" + "\\"; // Thumbnail 1200*628 

                    if (!Directory.Exists(Th120x68Path))
                        Directory.CreateDirectory(Th120x68Path);

                    string rootPath = contentPath + yearFolderPath + OriginalAttachment;
                    string useFileName = isThumbnail ? Path.Combine(Path.GetDirectoryName(aFormFile[i].FileName), Path.GetFileNameWithoutExtension(aFormFile[i].FileName)) + ".webp" : aFormFile[i].FileName;
                    var newUniquePath = GetUniqueFilePath(rootPath + useFileName);
                    if (isThumbnail)
                    {
                        fileName = Path.GetFileName(newUniquePath);
                        fileName = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName));
                        fileName = fileName + ".jpg";
                    }
                    else
                    {
                        fileName = Path.GetFileName(newUniquePath);
                    }

                    using (var stream = new FileStream(rootPath + fileName, FileMode.Create))
                    {
                        aFormFile[i].CopyToAsync(stream).GetAwaiter().GetResult();
                        string newPath = rootPath + "/" + fileName;
                        var savePath = "/" + newPath;
                        fileName = savePath = savePath.Replace(contentPath, "").Replace("\\", "/").Replace("//", "/");
                    }

                    paths.Add(fileName);

                    if (isThumbnail && aFormFile[i].ContentType.Split("/")[0] == "image")
                    {
                        string originalImagePath = rootPath + Path.GetFileName(fileName);

                        Th1200x628Path = Th1200x628Path + Path.GetFileName(fileName);
                        GenerateThumbnail(originalImagePath, Th1200x628Path, 1200, 628);

                        Th360x180Path = Th360x180Path + Path.GetFileName(fileName);
                        GenerateThumbnail(originalImagePath, Th360x180Path, 360, 180);

                        Th120x68Path = Th120x68Path + Path.GetFileName(fileName);
                        GenerateThumbnail(originalImagePath, Th120x68Path, 120, 68);
                    }
                }
                return SetResultStatus<List<string>>(paths, MessageStatus.FileUpload, true);
            }
            catch (Exception e)
            {
                Log.Error(e, CommonFunction.Errorstring("FileUploaderService.cs", "uploadFile"));
                return SetResultStatus<List<string>>(null, MessageStatus.Error, false, e.Message);
            }
        }

        public static string GetUniqueFilePath(string filePath)
        {
            if (File.Exists(filePath))
            {
                string folderPath = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                int number = 0;

                Match regex = Regex.Match(fileName, @"^(.+) \((\d+)\)$");

                if (regex.Success)
                {
                    fileName = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }

                do
                {
                    number++;
                    string newFileName = $"{fileName}({number}){fileExtension}";
                    filePath = Path.Combine(folderPath, newFileName);
                }
                while (File.Exists(filePath));
            }

            return filePath;
        }

        public ServiceResponse<object> DeleteFile(List<string> paths)
        {
            try
            {
                var deletedPath = new { SuccessDeleted = new List<string>(), FailedDeleted = new List<string>() };
                if (paths.Count == 0)
                {
                    return SetResultStatus<object>(null, MessageStatus.FileExists, false, "");
                }
                foreach (var path in paths)
                {
                    var fileName = Path.GetFileName(path);
                    var prevDirectories = GetFilesPath(path).Data;
                    if (prevDirectories is not null && prevDirectories.Count() > 0)
                    {
                        foreach (var item in prevDirectories)
                        {
                            var deletePath = _env.WebRootPath + item;
                            if (File.Exists(deletePath))
                            {
                                File.Delete(deletePath);
                            }
                        }
                        deletedPath.SuccessDeleted.Add(path);
                    }
                    else
                    {
                        deletedPath.FailedDeleted.Add(path);
                    }
                }
                return SetResultStatus<object>(deletedPath, deletedPath.FailedDeleted.Count() > 0 ? MessageStatus.FileExists : MessageStatus.FileDeleted, deletedPath.FailedDeleted.Count() > 0 ? false : true, "");
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploaderService.cs", "Removefile"));
                return SetResultStatus<object>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private static string ConvertLinuxDirectoryPath(string path)
        {
            return path.Replace("\\", "/").Replace("//", "/");
        }

        private void GenerateThumbnail(string originalImagePath, string thumbnailPath, int thumbnailWidth, int thumbnailHeight)
        {
            try
            {
                //originalImagePath = ConvertLinuxDirectoryPath(originalImagePath
                using (var image = Image.Load(originalImagePath))
                {
                    // Resize the image
                    int imageWidth = image.Width;
                    int imageHeight = image.Height;

                    var encoder = new PngEncoder()
                    {
                        CompressionLevel = PngCompressionLevel.BestCompression // Choose compression level
                    };

                    if (imageWidth > thumbnailWidth || imageHeight > thumbnailHeight)
                    {
                        image.Mutate(i => i.Resize(new ResizeOptions
                        {
                            Size = new Size(thumbnailWidth, thumbnailHeight),
                            Mode = ResizeMode.Max,
                        }));
                    }

                    // Save the image as JPEG
                    thumbnailPath = Path.ChangeExtension(thumbnailPath, ".png");
                    using (var outputStream = File.OpenWrite(thumbnailPath))
                    {
                        image.SaveAsPng(outputStream, encoder);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploaderService.cs", "GenerateThumbnail"));
            }

        }


        private List<string> CheckExistsFile(string path)
        {
            try
            {
                path = Path.GetFileName(path);
                var data = GetDirectories(path);
                var result = GetPathsFromJson(JsonConvert.SerializeObject(data));
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploaderService.cs", "GetFiles"));
                return null;
            }
        }

        public ServiceResponse<List<string>> GetFilesPath(string path)
        {
            try
            {
                path = Path.GetFileName(path);
                var data = GetDirectories(path);
                var result = GetPathsFromJson(JsonConvert.SerializeObject(data));
                if (result is not null && result.Count() > 0)
                {
                    return SetResultStatus<List<string>>(result, MessageStatus.Success, true, "");
                }
                else
                {
                    return SetResultStatus<List<string>>(null, MessageStatus.NoRecord, true, "");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploaderService.cs", "GetFilesPath"));
                return SetResultStatus<List<string>>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        private List<DirectoriesPath> GetDirectories(string fileName = "")
        {
            try
            {

                var prevDirectories = Directory.GetDirectories(_env.WebRootPath).Select(s => new DirectoriesPath() { DirectoryPath = s }).ToList();
                return GetDirectories(prevDirectories, fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DirectoriesPath> GetDirectories(List<DirectoriesPath> paths, string fileName = "")
        {
            try
            {
                return paths.Where(f => Directory.Exists(f.DirectoryPath)).Select(s => new DirectoriesPath()
                {
                    DirectoryPath = s.DirectoryPath,
                    Files = GetFiles(s.DirectoryPath, fileName),
                    FolderName = new DirectoryInfo(s.DirectoryPath).Name,
                    ChildDirectories = GetDirectories(Directory.GetDirectories(s.DirectoryPath).Select(s => new DirectoriesPath() { DirectoryPath = s }).ToList(), fileName)
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private List<FilesPath> GetFiles(string path, string fileName = "")
        {
            try
            {
                return Directory.GetFiles(path).Where(f => File.Exists(f)).Select(s => new FilesPath()
                {
                    FileName = Path.GetFileName(s),
                    Name = Path.GetFileNameWithoutExtension(Path.GetFileName(s)),
                    Path = s
                }).Where(s => (s.Name == Path.GetFileNameWithoutExtension(fileName) && !string.IsNullOrEmpty(fileName)) || string.IsNullOrEmpty(fileName)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<string> GetPathsFromJson(string jsonString)
        {
            try
            {
                var paths = new List<string>();
                var jsonArray = JArray.Parse(jsonString);

                foreach (var directory in jsonArray)
                {
                    CollectPaths(directory, paths);
                }

                return paths;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CollectPaths(JToken directory, List<string> paths)
        {
            try
            {
                var files = directory["Files"];
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var path = file["Path"].ToString().Replace(_env.WebRootPath.ToString(), string.Empty).Replace("\\", "/");
                        paths.Add(path);
                    }
                }

                var childDirectories = directory["ChildDirectories"];
                if (childDirectories != null)
                {
                    foreach (var childDirectory in childDirectories)
                    {
                        CollectPaths(childDirectory, paths);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public object CreateNewPath(string tableName, string column, string PIdColumn)
        {
            IDictionary<string, object> Data = new Dictionary<string, object>();
            List<string> failedPath = new List<string>();
            List<string> Success = new List<string>();
            List<string> FileIsExistsButNotSuccess = new List<string>();
            List<string> AlreadyMaigratedPath = new List<string>();
            List<string> OtherFiles = new List<string>();
            string path = "";
            try
            {
                var result = QueryList<Tables>(@"select " + PIdColumn + " as Id," + column + " as Path from " + tableName + " Where " + "ISNULL(" + column + ",'') !=''", null);
                if (result.Data != null && result.Data.Count() > 0)
                {
                    string json = JsonConvert.SerializeObject(result.Data);
                    var jsonpath = GetUniqueFilePath(_env.WebRootPath + "\\" + tableName + "_" + column + ".json");
                    File.WriteAllText(jsonpath, json);
                }
                foreach (var item in result.Data)
                {
                    if (!string.IsNullOrEmpty(item.Path))
                    {
                        if (ImageExtensions.Contains(Path.GetExtension(item.Path).ToUpperInvariant()))
                        {
                            if (item.Path.Contains("file.preptm.com") || item.Path.Contains("sfile.preptm.com"))
                            {
                                item.Path = item.Path.Replace("https://file.preptm.com/Content", "");
                                item.Path = item.Path.Replace("https://sfile.preptm.com/Content", "");
                            }
                            path = _env.WebRootPath + item.Path;
                            //string isExistsPath = _env.WebRootPath + "\\" + "2024" + "\\" + "OriginalAttachment" + "\\" + Path.GetFileNameWithoutExtension(item.Path) + ".webp";
                            if (path.Contains("OriginalAttachment") && path.Contains("2024"))
                            {
                                AlreadyMaigratedPath.Add(item.Path);
                            }
                            else if (File.Exists(ConvertLinuxDirectoryPath(path)) && !path.Contains("2024"))
                            {
                                using (var stream = System.IO.File.OpenRead(ConvertLinuxDirectoryPath(path)))
                                {
                                    var ssdf = Path.GetExtension(stream.Name);
                                    List<IFormFile> files = new List<IFormFile>();
                                    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                                    {
                                        Headers = new HeaderDictionary(),
                                        ContentType = Path.GetExtension(stream.Name) == ".pdf" ? "pdf" : "image" // You can set a default or specific content type here
                                    };
                                    files.Add(file);
                                    List<string> newPath = FileUploadForLinux(files, true).Data;
                                    if (newPath is not null && newPath.Count() > 0 && !string.IsNullOrEmpty(newPath[0]))
                                    {
                                        Success.Add(newPath[0]);
                                        //DB call
                                        Execute("UPDATE " + tableName + " SET " + column + "='" + newPath[0] + "' WHERE " + PIdColumn + "=" + item.Id, null, null);
                                    }
                                    else
                                    {
                                        FileIsExistsButNotSuccess.Add(item.Path);
                                    }
                                }
                            }
                            else
                            {
                                failedPath.Add(item.Path);
                            }
                        }
                    }
                }
                Data.Add("Success", Success);
                Data.Add("FileIsNotExists", failedPath);
                Data.Add("FileIsExistsButNotMigrated", FileIsExistsButNotSuccess);
                Data.Add("OldJsonData", result.Data);
                Data.Add("AlreadyMaigratedPath", AlreadyMaigratedPath);
                return Data;
            }
            catch (Exception ex)
            {
                Data.Add("Exception", path.Replace(_env.WebRootPath, string.Empty));
                return ex.Message;
            }
        }

        public object GenerateThumbnailByDirectories(string folderName, string fileName, bool isTransf = false)
        {
            List<string> Corruptedfiles = new List<string>();
            IDictionary<string, object> Data = new Dictionary<string, object>();
            try
            {
                var Directories = GetDirectories();
                var groupedFiles = new Dictionary<string, List<FilesPath>>();
                TraverseDirectories(Directories, groupedFiles);
                var groupeddata = groupedFiles.Where(s => (s.Key.ToString() == folderName || string.IsNullOrEmpty(folderName)) && (string.IsNullOrEmpty(fileName) || s.Value.Any(s => s.FileName == fileName && s.Name == Path.GetFileNameWithoutExtension(fileName))))
                            .Select(v => new { key = v.Key, Value = v.Value.Where(p => string.IsNullOrEmpty(fileName) || p.FileName == folderName || p.Name == Path.GetFileNameWithoutExtension(fileName)).ToList() }).ToList();
                if (groupeddata is not null && groupeddata.Count() > 0)
                {
                    Data.Add("List", groupeddata);
                    foreach (var item in groupeddata)
                    {
                        foreach (var vitem in item.Value)
                        {
                            if (ImageExtensions.Contains(Path.GetExtension(vitem.Path).ToUpperInvariant()) && isTransf)
                            {
                                if (!IsImageCorrupted(vitem.Path))
                                {
                                    string Th1200x628Path = Path.ChangeExtension(vitem.Path.Replace(item.key, "Th1200x628"), ".png");
                                    GenerateThumbnail(vitem.Path, Th1200x628Path, 1200, 628);

                                    string Th360x180Path = Path.ChangeExtension(vitem.Path.Replace(item.key, "Th360x180"), ".png");
                                    GenerateThumbnail(vitem.Path, Th360x180Path, 360, 180);

                                    string Th120x68Path = Path.ChangeExtension(vitem.Path.Replace(item.key, "Th120x68"), ".png");
                                    GenerateThumbnail(vitem.Path, Th120x68Path, 360, 180);
                                }
                                else
                                {
                                    Corruptedfiles.Add(vitem.Path);
                                }
                            }
                        }
                    }
                    if (Corruptedfiles is not null && Corruptedfiles.Count > 0)
                    {
                        string json = JsonConvert.SerializeObject(Corruptedfiles);
                        var jsonpath = GetUniqueFilePath(_env.WebRootPath + "\\" + "CorruptedFiles" + ".json");
                        File.WriteAllText(jsonpath, json);
                        Data.Add("Corruptedfiles", Corruptedfiles);
                    }
                }
                return Data;
            }
            //
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploaderService.cs", "GenerateThumbnailByDirectories"));
                return ex.Message;
            }
        }
        public void TraverseDirectories(List<DirectoriesPath> directories, Dictionary<string, List<FilesPath>> groupedFiles)
        {
            foreach (var directory in directories)
            {
                if (!groupedFiles.ContainsKey(directory.FolderName))
                {
                    groupedFiles[directory.FolderName] = new List<FilesPath>();
                }
                groupedFiles[directory.FolderName].AddRange(directory.Files);

                TraverseDirectories(directory.ChildDirectories, groupedFiles);
            }
        }
        public readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG", ".WEBP" };

        public bool IsFormFileCorrupted(IFormFile formFile)
        {
            try
            {
                using (var stream = formFile.OpenReadStream())
                using (var image = Image.Load(stream))
                {
                    // Attempt some basic operation to ensure the image is fully loaded
                    image.Mutate(x => x.Resize(1, 1));
                }
                return false; // Image loaded successfully
            }
            catch (Exception)
            {
                return true; // Exception occurred, image is likely corrupted
            }
        }

        public bool IsImageCorrupted(string imagePath)
        {
            try
            {
                using (var image = Image.Load(imagePath))
                {
                    // Attempt some basic operation to ensure the image is fully loaded
                    image.Mutate(x => x.Resize(1, 1));
                }
                return false; // Image loaded successfully
            }
            catch (Exception)
            {
                return true; // Exception occurred, image is likely corrupted
            }
        }
    }
}
public class DirectoriesPath
{
    public string DirectoryPath { get; set; }
    public string FolderName { get; set; }
    public List<DirectoriesPath> ChildDirectories { get; set; } = new List<DirectoriesPath>();
    public List<FilesPath> Files { get; set; }
}
public class FilesPath
{
    public string Path { get; set; }
    public string FileName { get; set; }
    public string Name { get; set; }

}
public class Tables
{
    public string Path { get; set; }
    public int Id { get; set; }
}

