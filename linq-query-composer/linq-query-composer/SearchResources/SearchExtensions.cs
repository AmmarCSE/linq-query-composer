// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchExtensions.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Search Extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.Web.Mvc.Html
{
    using System.Linq.Expressions;

    using Travel.Agency.RazorGrid.LambdaFilters.FilterAssembler;
    using Travel.Agency.RazorGrid.SearchResources;

    /// <summary>
    /// The search extensions.
    /// </summary>
    public static class SearchExtensions
    {
        /// <summary>
        /// The search for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TSearchModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString SearchFor<TSearchModel>(
            this HtmlHelper<TSearchModel> htmlHelper, Expression<Func<TSearchModel, TSearchModel>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            ISearchModel searchModel = (ISearchModel)metadata.Model;

            return new SearchAssembler().AssembleSearchForRazor(htmlHelper, searchModel, null);
        }
    }
}