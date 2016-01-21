// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridDataBuilder.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Data Buildere.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.LambdaFilters
{
    using System.Collections.Generic;
    using System.Linq;

    using Travel.Agency.RazorGrid.Helpers;
    using Travel.Agency.RazorGrid.LambdaFilters.FilterData;

    /// <summary>
    /// The grid data builder.
    /// </summary>
    public class GridDataBuilder
    {
        /// <summary>
        /// The page size.
        /// </summary>
        private readonly int pageSize = 10;

        /// <summary>
        /// The get raw grid data for grid model.
        /// </summary>
        /// <param name="searchItems">
        /// The search items.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<TEntity> GetRawGridDataForGridModel<TEntity, TModel>(List<FilterSearchItem> searchItems)
            where TEntity : class
        {
            return FilterDataHelper.GetRawQueryable<TEntity>(
                searchItems, false, GridPropertyHelper.ExtractQuickSearchProperties<TModel>());
        }

        /// <summary>
        /// The select grid data for grid model.
        /// </summary>
        /// <param name="queryableSource">
        /// The queryable source.
        /// </param>
        /// <param name="searchItems">
        /// The search items.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageCount">
        /// The page count.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The List.
        /// </returns>
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
