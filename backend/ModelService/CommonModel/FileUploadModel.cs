using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.CommonModel
{
    public class FileUploadModel
    {
        public IFormFile? file { get; set; }
        public string? path { get; set; }
        public string? filename { get; set; }
    }


}

   
