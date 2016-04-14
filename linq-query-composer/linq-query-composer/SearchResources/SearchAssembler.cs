namespace Travel.Agency.RazorGrid.LambdaFilters.FilterAssembler
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Travel.Agency.RazorGrid.GridResources;
    using Travel.Agency.RazorGrid.Helpers;
    using Travel.Agency.RazorGrid.LambdaFilters.LamdaFilterResources.FilterModels;

    public class SearchAssembler
    {
        public void AssembleSearch(IGridModel gridModel)
        {
            IQueryable queryableSource = gridModel.GetRawGridData(null);

            int PageCount;
            int pageIndex = 0;
            dynamic gridData = gridModel.SelectGridDataForGridModel(
                queryableSource, null, pageIndex, out PageCount);
        }
    }
}
