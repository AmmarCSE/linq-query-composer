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
            ReservationDataModel gridModel = new ReservationDataModel();
            IQueryable queryableSource = gridModel.GetRawData(searchItems);

            int PageCount, pageIndex = 0;
            dynamic data = gridModel.SelectDataForDataModel(
                queryableSource, searchItems, pageIndex, out PageCount);
        }
    }
}
