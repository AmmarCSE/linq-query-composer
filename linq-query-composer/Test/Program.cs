using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Agency.BusnissLogic.Models.SearchModels.GridModels;
using Linq.Query.Composer.Model;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var searchItems = new List<FilterItem>();
            ReservationGridModel gridModel = new ReservationGridModel();
            IQueryable queryableSource = gridModel.GetRawGridData(searchItems);

            int PageCount, pageIndex = 0;
            dynamic gridData = gridModel.SelectGridDataForGridModel(
                queryableSource, searchItems, pageIndex, out PageCount);
        }
    }
}
