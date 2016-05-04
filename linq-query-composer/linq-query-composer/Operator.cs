namespace Linq.Query.Composer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Linq.Query.Composer.Model;
    using DBContext;

    public class Operator
    {
        private readonly int pageSize = 10;

        public IQueryable<TEntity> GetRawGridDataForGridModel<TEntity, TModel>(List<FilterItem> searchItems)
            where TEntity : class
        {
            return GetRawQueryable<TEntity>( searchItems, false);
        }

        public List<TModel> SelectGridDataForGridModel<TEntity, TModel>(
            IQueryable queryableSource, List<FilterItem> searchItems, int pageIndex, out int pageCount)
            where TEntity : class
        {
            pageCount = (((IQueryable<TEntity>)queryableSource).Count() + this.pageSize - 1) / this.pageSize;

            return
                ((IQueryable<TEntity>)queryableSource).OrderBy(
                    LambdaExpressionGenerator.GenerateOrderByExpression<TEntity>())
                                                      .Skip(this.pageSize * pageIndex)
                                                      .Take(this.pageSize)
                                                      .Select(
                                                          LambdaExpressionGenerator.GetSelectClause<TEntity, TModel>())
                                                      .ToList();
        }

        private static IQueryable<TEntity> GetRawQueryable<TEntity>(
            List<FilterItem> searchItems, bool isCommon)
            where TEntity : class
        {
            TAS_DevEntities1 dbContext = new TAS_DevEntities1();
            IQueryable<TEntity> queryable = dbContext.Set<TEntity>();

            if (!isCommon)
            {
                queryable =
                    queryable.Where(LambdaExpressionGenerator.GenerateWhereClause<TEntity>(searchItems));
            }

            return queryable;
        }
    }
}
