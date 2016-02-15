using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var searchItems = new List<FilterSearchItem>();

            IQueryable queryableSource = searchModel.GridModel.GetRawGridData(searchItems);

            int PageCount;
            dynamic gridData = searchModel.GridModel.SelectGridDataForGridModel(
                queryableSource, searchItems, pageIndex, out PageCount);
        }
    }
}
