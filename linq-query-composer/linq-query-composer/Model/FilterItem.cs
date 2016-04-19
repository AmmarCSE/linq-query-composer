using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Query.Composer.Model
{
    public class FilterItem
    {
        public string SearchType { get; set; }
        public string SearchKey { get; set; }
        public string SearchData { get; set; }

        public FilterItem()
        {
        }
    }
}
