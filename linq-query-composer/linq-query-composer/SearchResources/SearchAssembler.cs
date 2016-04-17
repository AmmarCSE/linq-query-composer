namespace Linq.Query.Composer.LambdaFilters.FilterAssembler
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Linq.Query.Composer.GridResources;
    using Linq.Query.Composer.Helpers;
    using Linq.Query.Composer.LambdaFilters.LamdaFilterResources.FilterModels;

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
