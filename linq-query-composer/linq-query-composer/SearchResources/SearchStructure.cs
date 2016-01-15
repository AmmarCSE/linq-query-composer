// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchStructure.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Search Structure.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Mvc;
namespace Travel.Agency.RazorGrid.SearchResources
{
    /// <summary>
    /// The search structure.
    /// </summary>
    public class SearchStructure
    {
        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public string GridHtml { get; set; }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        public dynamic Filters { get; set; }
    }
}
