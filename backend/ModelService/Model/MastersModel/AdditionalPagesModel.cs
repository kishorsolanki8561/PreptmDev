using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class AdditionalPagesModel
    {
        public int Id { get; set; }
        public byte PageType { get; set; }
        public string Content { get; set; }
        public string? ContentHindi { get; set; }
        public string? ContentJson { get; set; }
        public string? ContentHindiJson { get; set; }
    }

    public class AdditionalPagesViewModel
    {
        public int Id { get; set; }
        public byte PageType { get; set; }
        public string Content { get; set; }
        public string? ContentHindi { get; set; }
        public string? ContentJson { get; set; }
        public string? ContentHindiJson { get; set; }
    }

    public class AdditionalPagesListModel
    {
        public int Id { get; set; }
        public byte PageType { get; set; }
        public string? Content { get; set; }
        public string? ContentHindi { get; set; }
    }
}
