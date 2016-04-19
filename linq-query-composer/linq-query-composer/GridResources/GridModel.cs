namespace Linq.Query.Composer.GridResources
{
    using System.Collections.Generic;
    using System.Linq;

    using Linq.Query.Composer.LambdaFilters;
    using Linq.Query.Composer.LambdaFilters.LamdaFilterResources.FilterModels;

    public class GridModel<TEntity, TModel> : IGridModel
        where TEntity : class
    {
        public dynamic GetRawGridData(List<FilterSearchItem> searchItems)
        {
            return new GridDataBuilder()
                .GetRawGridDataForGridModel<TEntity, TModel>(searchItems);
        }

        public dynamic SelectGridDataForGridModel(
            IQueryable queryable, List<FilterSearchItem> searchItems, int pageIndex, out int pageCount)
        {
            return new GridDataBuilder().SelectGridDataForGridModel<TEntity, TModel>(
                queryable, searchItems, pageIndex, out pageCount);
        }
    }
}
