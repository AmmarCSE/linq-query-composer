// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridTagHelper.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Tag Helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.Helpers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// The grid tag helper.
    /// </summary>
    public static class GridTagHelper
    {
        /// <summary>
        /// The wrap in element.
        /// </summary>
        /// <param name="elementType">
        /// The element type.
        /// </param>
        /// <param name="innerHtml">
        /// The inner html.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string WrapInElement(string elementType, string innerHtml)
        {
            return WrapInElement(elementType, innerHtml, appendBreak: false);
        }

        /// <summary>
        /// The wrap in element.
        /// </summary>
        /// <param name="elementType">
        /// The element type.
        /// </param>
        /// <param name="innerHtml">
        /// The inner html.
        /// </param>
        /// <param name="appendBreak">
        /// The append break.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string WrapInElement(string elementType, string innerHtml, bool appendBreak)
        {
            return WrapInElement(elementType, innerHtml, appendBreak, string.Empty);
        }

        /// <summary>
        /// The wrap in element.
        /// </summary>
        /// <param name="elementType">
        /// The element type.
        /// </param>
        /// <param name="innerHtml">
        /// The inner html.
        /// </param>
        /// <param name="appendBreak">
        /// The append break.
        /// </param>
        /// <param name="cssClass">
        /// The css class.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string WrapInElement(string elementType, string innerHtml, bool appendBreak, string cssClass)
        {
            return WrapInElement(elementType, innerHtml, appendBreak, cssClass, htmlAttributes: null);
        }

        /// <summary>
        /// The wrap in element.
        /// </summary>
        /// <param name="elementType">
        /// The element type.
        /// </param>
        /// <param name="innerHtml">
        /// The inner html.
        /// </param>
        /// <param name="appendBreak">
        /// The append break.
        /// </param>
        /// <param name="cssClass">
        /// The css class.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string WrapInElement(string elementType, string innerHtml, bool appendBreak, string cssClass, Dictionary<string, object> htmlAttributes)
        {
            return WrapInElementHelper(elementType, innerHtml, appendBreak, cssClass, htmlAttributes);
        }

        /// <summary>
        /// The wrap in element helper.
        /// </summary>
        /// <param name="elementType">
        /// The element type.
        /// </param>
        /// <param name="innerHtml">
        /// The inner html.
        /// </param>
        /// <param name="appendBreak">
        /// The append break.
        /// </param>
        /// <param name="cssClass">
        /// The css class.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string WrapInElementHelper(string elementType, string innerHtml, bool appendBreak, string cssClass, Dictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder(elementType);
            tagBuilder.InnerHtml = innerHtml;

            if (cssClass != string.Empty)
            {
                tagBuilder.AddCssClass(cssClass);
            }

            if (htmlAttributes != null)
            {
                tagBuilder.MergeAttributes(htmlAttributes);
            }

            string element = tagBuilder.ToString();
            if (appendBreak)
            {
                element += "<br/>";
            }

            return element;
        }
    }
}