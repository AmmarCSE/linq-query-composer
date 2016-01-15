// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchModel.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines the ISearch Model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.SearchResources
{
    using System.Collections.Generic;
    using Travel.Agency.EntityFramework;
    using Travel.Agency.RazorGrid.GridResources;
    using Travel.Agency.RazorGrid.LambdaFilters.LambdaFilterResources.FilterModels;

    /// <summary>
    /// The SearchModel interface.
    /// </summary>
    public interface ISearchModel
    {
        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the search model name.
        /// </summary>
        string SearchModelName { get; set; }

        /// <summary>
        /// Gets or sets the grid model.
        /// </summary>
        IGridModel GridModel { get; set; }

        /// <summary>
        /// Gets or sets the filter model.
        /// </summary>
        IFilterModel FilterModel { get; set; }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        List<IFilter> Filters { get; set; }

        /// <summary>
        /// Gets or sets the grid permissions.
        /// </summary>
        List<GridEnums.GridPermission> GridPermissions { get; set; }

        /// <summary>
        /// The set grid permissions.
        /// </summary>
        void SetGridPermissions(TAS_DevEntities dbContext);
    }
}
