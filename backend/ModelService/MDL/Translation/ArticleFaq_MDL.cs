using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class ArticleFaq_MDL
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Que { get; set; }
        public string Ans { get; set; }
        public string QueHindi { get; set; }
        public string AnsHindi { get; set; }
    }
}
