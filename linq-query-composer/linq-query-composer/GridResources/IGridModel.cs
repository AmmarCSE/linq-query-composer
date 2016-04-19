namespace Linq.Query.Composer.GridResources
{
    using System.Collections.Generic;
    using System.Linq;
    using Linq.Query.Composer.LambdaFilters.LamdaFilterResources.FilterModels;

    public interface IGridModel 
    {
        dynamic GetRawGridData(List<FilterSearchItem> searchItems);
        dynamic SelectGridDataForGridModel(
            IQueryable queryable, List<FilterSearchItem> searchItems, int pageIndex, out int pageCount);
    }
}
