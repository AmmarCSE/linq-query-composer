namespace Linq.Query.Composer.LambdaFilters
{
    using System.Collections.Generic;
    using System.Linq;

    using Linq.Query.Composer.Helpers;
    using Linq.Query.Composer.LambdaFilters.FilterData;
    using Linq.Query.Composer.LambdaFilters.LamdaFilterResources.FilterModels;

    public class GridDataBuilder
    {
        private readonly int pageSize = 10;

        public IQueryable<TEntity> GetRawGridDataForGridModel<TEntity, TModel>(List<FilterSearchItem> searchItems)
            where TEntity : class
        {
            return FilterDataHelper.GetRawQueryable<TEntity>(
                searchItems, false, GridPropertyHelper.ExtractQuickSearchProperties<TModel>());
        }

        public List<TModel> SelectGridDataForGridModel<TEntity, TModel>(
            IQueryable queryableSource, List<FilterSearchItem> searchItems, int pageIndex, out int pageCount)
            where TEntity : class
        {
            pageCount = (((IQueryable<TEntity>)queryableSource).Count() + this.pageSize - 1) / this.pageSize;

            return
                ((IQueryable<TEntity>)queryableSource).OrderBy(
                    GridLambdaExpressionHelper.GenerateOrderByExpression<TEntity>())
                                                      .Skip(this.pageSize * pageIndex)
                                                      .Take(this.pageSize)
                                                      .Select(
                                                          GridLambdaExpressionHelper.GetSelectClause<TEntity, TModel>())
                                                      .ToList();
        }
    }
}
