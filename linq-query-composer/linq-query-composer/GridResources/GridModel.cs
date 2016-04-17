// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridModel.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linq.Query.Composer.GridResources
{
    using System.Collections.Generic;
    using System.Linq;

    using Linq.Query.Composer.LambdaFilters;
    using Linq.Query.Composer.LambdaFilters.LamdaFilterResources.FilterModels;

    /// <summary>
    /// The grid model.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TModel">
    /// </typeparam>
    public class GridModel<TEntity, TModel> : IGridModel
        where TEntity : class
    {
        /// <summary>
        /// The get raw grid data.
        /// </summary>
        /// <param name="searchItems">
        /// The search items.
        /// </param>
        /// <returns>
        /// The dynamic.
        /// </returns>
        public dynamic GetRawGridData(List<FilterSearchItem> searchItems)
        {
            return new GridDataBuilder()
                .GetRawGridDataForGridModel<TEntity, TModel>(searchItems);
        }

        /// <summary>
        /// The select grid data for grid model.
        /// </summary>
        /// <param name="queryable">
        /// The queryable.
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
        /// <returns>
        /// The dynamic.
        /// </returns>
        public dynamic SelectGridDataForGridModel(
            IQueryable queryable, List<FilterSearchItem> searchItems, int pageIndex, out int pageCount)
        {
            return new GridDataBuilder().SelectGridDataForGridModel<TEntity, TModel>(
                queryable, searchItems, pageIndex, out pageCount);
        }
    }
}
