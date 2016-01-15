// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridHtmlHelperRetriever.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Element Helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.Helpers
{
    using System.Web.Mvc;

    /// <summary>
    /// The grid html helper retriever.
    /// </summary>
    public static class GridHtmlHelperRetriever
    {
        /// <summary>
        /// The retrieve grid html helper.
        /// </summary>
        /// <param name="originalHtmlHelper">
        /// The original html helper.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="HtmlHelper"/>.
        /// </returns>
        public static HtmlHelper<TModel> RetrieveGridHtmlHelper<TModel>(HtmlHelper originalHtmlHelper, TModel model)
        {
            ViewDataContainer viewDataContainer = new ViewDataContainer();
            viewDataContainer.ViewData = new ViewDataDictionary(model);
            return new HtmlHelper<TModel>(originalHtmlHelper.ViewContext, viewDataContainer);
        }
    }

    /// <summary>
    /// The view data container.
    /// </summary>
    public class ViewDataContainer : IViewDataContainer
    {
        /// <summary>
        /// Gets or sets the view data.
        /// </summary>
        public ViewDataDictionary ViewData { get; set; }
    }
}