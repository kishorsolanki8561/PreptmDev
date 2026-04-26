using CommonService.JWT;
using CommonService.Other.AppConfig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ModelService.CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static CommonService.Other.UtilityManager;

namespace CommonService.Other
{
    public class FileUploader
    {
        private HttpClient _fileHttp { get; set; }
        private WebClient _webClient { get; set; }
        private readonly JWTAuthManager _jWTAuthManager;
        private IWebHostEnvironment _env;
        public FileUploader(JWTAuthManager jWTAuthManager, IWebHostEnvironment env)
        {
            _fileHttp = new HttpClient();
            _webClient = new WebClient();
            _fileHttp.BaseAddress = new Uri(AppConfigFactory.Configs.filesUrls.UpdateUrl);
            _jWTAuthManager = jWTAuthManager;
            _env = env;
            //_env.WebRootPath = AppConfigFactory.Configs.filesUrls.WebRootPath;
        }
        public string PostFile(FileUploadModel fileModel)
        {
            try
            {
                MultipartFormDataContent form = new MultipartFormDataContent();
                byte[] data;
                using (var br = new BinaryReader(fileModel.file.OpenReadStream()))
                    data = br.ReadBytes((int)fileModel.file.OpenReadStream().Length);
                ByteArrayContent bytes = new ByteArrayContent(data);
                form.Add(bytes, "file", fileModel.file.FileName);
                _fileHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jWTAuthManager.Token);
                var Newresponse = _fileHttp.PostAsync($"FileUploader/FileUpload?path= " + fileModel.path.Trim() + " &filename=" + fileModel.filename + "", form).GetAwaiter().GetResult();
                Newresponse.EnsureSuccessStatusCode();
                return Newresponse.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploader.cs", "PostFile"));
                throw ex;
            }

            //return (JsonConvert.DeserializeObject<JObject>(Newjson)).ToString();
        }


        public string UploadFiles(List<IFormFile> files, bool isThumbnail = true)
        {
            try
            {
                MultipartFormDataContent form = new MultipartFormDataContent();
                foreach (var file in files)
                {
                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                        data = br.ReadBytes((int)file.OpenReadStream().Length);
                    ByteArrayContent bytes = new ByteArrayContent(data);
                    form.Add(bytes, "file", file.FileName);
                }
                //_fileHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jWTAuthManager.Token);
                var Newresponse = _fileHttp.PostAsync($"FileUploader/Upload?isThumbnail=true", form).GetAwaiter().GetResult();
                Newresponse.EnsureSuccessStatusCode();
                return Newresponse.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploader.cs", "UploadFiles"));
                throw ex;
            }

            //return (JsonConvert.DeserializeObject<JObject>(Newjson)).ToString();
        }

        public MemoryStream PostGoogleFile(string url)
        {
            try
            {
                _webClient = new WebClient();
                byte[] bytes;
                // Download data from the URL
                try
                {
                    bytes = _webClient.DownloadData(url);
                }
                catch (WebException webEx) when ((webEx.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                // Create a memory stream from the downloaded bytes
                MemoryStream ms = new MemoryStream(bytes);
                return ms;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploader.cs", "PostGoogleFile"));
                throw ex;
            }
        }
        public bool DeleteFile(string filePath)
        {
            try
            {
                _fileHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jWTAuthManager.Token);
                var Newresponse = _fileHttp.GetAsync($"FileUploader/RemoveFile?path=" + filePath.ToString()).GetAwaiter().GetResult();
                Newresponse.EnsureSuccessStatusCode();
                string Newjson = Newresponse.Content.ReadAsStringAsync().Result;
                return (bool)(JsonConvert.DeserializeObject<bool>(Newjson));
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploader.cs", "DeleteFile"));
                throw ex;
            }
        }

        public List<string> GetFilesPath(string path)
        {
            try
            {
                path = Path.GetFileName(path);
                var data = GetDirectories(path);
                var result = GetPathsFromJson(JsonConvert.SerializeObject(data));
                if (result is not null && result.Count() > 0)
                {
                    return result;
                }
                else
                {
                    _env.WebRootPath = "";
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("FileUploader.cs", "GetFilesPath"));
                return null;
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
    }
    public class DirectoriesPath
    {
        public string DirectoryPath { get; set; }
        public List<DirectoriesPath> ChildDirectories { get; set; } = new List<DirectoriesPath>();
        public List<FilesPath> Files { get; set; }
    }
    public class FilesPath
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }

    }
}
