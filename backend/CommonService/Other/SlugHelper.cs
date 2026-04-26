using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonService.Other
{
    public class SlugHelper
    {
        // white space, em-dash, en-dash, underscore
        static readonly Regex WordDelimiters = new Regex(@"[\s—–_]", RegexOptions.Compiled);

        // characters that are not valid
        static readonly Regex InvalidChars = new Regex(@"[^a-z0-9\-]", RegexOptions.Compiled);

        // multiple hyphens
        static readonly Regex MultipleHyphens = new Regex(@"-{2,}", RegexOptions.Compiled);

        public static string ToUrlSlug(string value)
        {
            try
            {
                // convert to lower case
                value = value.ToLowerInvariant();

                // remove diacritics (accents)
                value = RemoveDiacritics(value);

                // ensure all word delimiters are hyphens
                value = WordDelimiters.Replace(value, "-");

                // strip out invalid characters
                value = InvalidChars.Replace(value, "");

                // replace multiple hyphens (-) with a single hyphen
                value = MultipleHyphens.Replace(value, "-");

                // trim hyphens (-) from ends
                return value.Trim('-');
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SlugHelper.cs", "ToUrlSlug"));
                throw ex;
            }
        }

        private static string RemoveDiacritics(string stIn)
        {
            try
            {
                string stFormD = stIn.Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder();

                for (int ich = 0; ich < stFormD.Length; ich++)
                {
                    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                    if (uc != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(stFormD[ich]);
                    }
                }
                return (sb.ToString().Normalize(NormalizationForm.FormC));
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SlugHelper.cs", "RemoveDiacritics"));
                throw ex;
            }
            
        }
    }
}
