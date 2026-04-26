using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.Model.Front
{
    public class DeepLinkModel
    {
        public dynamicLinkInfo dynamicLinkInfo { get; set; } = new dynamicLinkInfo(); 
        public suffix suffix { get; set; } = new suffix();

    }
    public class dynamicLinkInfo
    {
        public string domainUriPrefix { get; set; }
        public string link { get; set; }
        public androidInfo androidInfo { get; set; } = new androidInfo();
        public iosInfo iosInfo { get; set; } = new iosInfo();
        public socialMetaTagInfo socialMetaTagInfo { get; set; }=  new socialMetaTagInfo();

    }
    public class androidInfo
    {
        public string androidPackageName { get; set; }
    }
    public class iosInfo
    {
        public string iosBundleId { get; set; }
    }

    public class ResponseDeepLinkModel
    {
        public string? shortLink { get; set; }
        public List<object> warning { get; set; }
        public string previewLink { get; set; }

    }
    public class socialMetaTagInfo
    {
        public string? socialTitle { get; set; }
        public string? socialDescription { get; set; }
        public string? socialImageLink { get; set; }
    }
    public class suffix
    {
        public string? option { get; set; }
    }
}
