namespace Linq.Query.Composer.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class DataModel<TEntity, TModel> : IDataModel
        where TEntity : class
    {
        public dynamic GetRawGridData(List<FilterItem> searchItems)
        {
            return new Operator()
                .GetRawGridDataForGridModel<TEntity, TModel>(searchItems);
        }

        public dynamic SelectGridDataForGridModel(
            IQueryable queryable, List<FilterItem> searchItems, int pageIndex, out int pageCount)
        {
            return new Operator().SelectGridDataForGridModel<TEntity, TModel>(
                queryable, searchItems, pageIndex, out pageCount);
        }
    }
}
