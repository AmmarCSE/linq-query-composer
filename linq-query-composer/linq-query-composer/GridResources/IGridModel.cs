// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGridModel.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines I Grid Model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linq.Query.Composer.GridResources
{
    using System.Collections.Generic;
    using System.Linq;
    using Linq.Query.Composer.LambdaFilters.LamdaFilterResources.FilterModels;

    /// <summary>
    /// The GridModel interface.
    /// </summary>
    public interface IGridModel 
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
        dynamic GetRawGridData(List<FilterSearchItem> searchItems);

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
        dynamic SelectGridDataForGridModel(
            IQueryable queryable, List<FilterSearchItem> searchItems, int pageIndex, out int pageCount);
    }
}
