namespace Linq.Query.Composer.LambdaFilters.FilterData
{
    using DBContext;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Linq.Query.Composer.LambdaFilters.FilterData.LambdaHelper;
    using Linq.Query.Composer.LambdaFilters.LamdaFilterResources.FilterModels;

    public static class FilterDataHelper
    {
        public static IQueryable<TEntity> GetRawQueryable<TEntity>(
            List<FilterSearchItem> searchItems, bool isCommon, List<PropertyInfo> quickSearchProperties)
            where TEntity : class
        {
            LambdaExpressionHelper expressionHelper = new LambdaExpressionHelper();
            TAS_DevEntities1 dbContext = new TAS_DevEntities1();
            IQueryable<TEntity> queryable = dbContext.Set<TEntity>();

            if (!isCommon)
            {
                queryable =
                    queryable.Where(expressionHelper.GenerateWhereClause<TEntity>(searchItems, quickSearchProperties));
            }

            return queryable;
        }
    }
}