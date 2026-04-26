using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.MDL.Translation
{
    public class ArticleTags_MDL
    {
        public ArticleTags_MDL() { }

        public int Id { get; set; }
        public int TagsId { get; set; }
        public int ArticleId { get; set; }

    }
}
