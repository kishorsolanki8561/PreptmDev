using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelService.CommonModel
{
    public partial class PagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public string[] ColumnNames { get; set; }

        public int PageSize { get; set; }
        public string[] HeaderNames { get; set; }
        public long TotalRecords { get; set; }

        public string HelpDocUrl { get; set; }

        //constructer to initialize values
        public PagedData()
        {
            string[] collection = new string[typeof(T).GetProperties().Length];
            int i = 0;
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                collection[i] = p.Name;
                i++;
            }
            ColumnNames = collection;

            CurrentPage = 1;

        }
        //for more customization use below function.

        public static PagedData<T> ReturnCustomizeData(PagedData<T> data, int PageSize, long TotalRecords, string[] columnNames = null, string[] headersName = null, string helpDocUrl = null, int page = 1)
        {
            //This property will create serial no based on paging
            data.PageSize = PageSize;
            data.CurrentPage = page;
            //By default all the properties of the class will be appear as column name, you can customize as given below
            data.ColumnNames = columnNames != null ? columnNames : data.ColumnNames;
            //By default database column name will be the header name, you can customize as given below
            data.HeaderNames = headersName != null ? headersName : data.HeaderNames;
            data.TotalRecords = TotalRecords;
            data.HelpDocUrl = helpDocUrl;
            data.NumberOfPages = PageSize == 101 ? 1 : Convert.ToInt32(Math.Ceiling((double)TotalRecords / PageSize));
            return data;
        }
    }

}
