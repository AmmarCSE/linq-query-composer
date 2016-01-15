// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridElementHelper.cs" company="AnnabSoft">
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
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    /// The grid element helper.
    /// </summary>
    public static class GridElementHelper
    {
        /// <summary>
        /// The validation element.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString ValidationElement<TModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property)
        {
            bool isNullable;
            int typeCode = GridPropertyHelper.DetermineTypeCodeAndNullability(property, out isNullable);

            MvcHtmlString mvcElement = null;
            if (isNullable)
            {
                switch (typeCode)
                {
                    case (int)TypeCode.Int32:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, int?>(property, 0));
                        break;
                    case (int)TypeCode.Int64:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, long?>(property, 0));
                        break;
                    case (int)TypeCode.Decimal:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, decimal?>(property, 0));
                        break;
                    case (int)TypeCode.DateTime:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, DateTime?>(property, 0));
                        break;
                    case (int)TypeCode.Boolean:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, bool?>(property, 0));
                        break;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case (int)TypeCode.String:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, string>(property, 0));
                        break;
                    case (int)TypeCode.Int32:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, int>(property, 0));
                        break;
                    case (int)TypeCode.Int64:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, long>(property, 0));
                        break;
                    case (int)TypeCode.Decimal:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, decimal>(property, 0));
                        break;
                    case (int)TypeCode.DateTime:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, DateTime>(property, 0));
                        break;
                    case (int)TypeCode.Boolean:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, bool>(property, 0));
                        break;
                    case (int)TypeCode.Object:
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, object>(property, 0));
                        break;
                    case 17: // 17 is for timespan which was removed from .net - http://stackoverflow.com/questions/7329834/what-happened-to-system-typecode-of-value-17
                        mvcElement = htmlHelper.ValidationMessageFor(GridLambdaExpressionHelper.GenerateExpression<TModel, TimeSpan>(property, 0));
                        break;
                }
            }

            return mvcElement;
        }

        /// <summary>
        /// The label element.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString LabelElement<TModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property)
        {
            bool isNullable;
            int typeCode = GridPropertyHelper.DetermineTypeCodeAndNullability(property, out isNullable);

            MvcHtmlString mvcElement = null;
            if (isNullable)
            {
                switch (typeCode)
                {
                    case (int)TypeCode.Int32:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, int?>(property, null));
                        break;
                    case (int)TypeCode.Int64:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, long?>(property, null));
                        break;
                    case (int)TypeCode.Decimal:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, decimal?>(property, null));
                        break;
                    case (int)TypeCode.DateTime:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, DateTime?>(property, null));
                        break;
                    case (int)TypeCode.Boolean:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, bool?>(property, null));
                        break;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case (int)TypeCode.String:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, string>(property, null));
                        break;
                    case (int)TypeCode.Int32:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, int>(property, null));
                        break;
                    case (int)TypeCode.Int64:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, long>(property, null));
                        break;
                    case (int)TypeCode.Decimal:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, decimal>(property, null));
                        break;
                    case (int)TypeCode.DateTime:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, DateTime>(property, null));
                        break;
                    case (int)TypeCode.Boolean:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, bool>(property, null));
                        break;
                    case (int)TypeCode.Object:
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, object>(property, null));
                        break;
                    case 17: // 17 is for timespan which was removed from .net - http://stackoverflow.com/questions/7329834/what-happened-to-system-typecode-of-value-17
                        mvcElement = htmlHelper.LabelFor(GridLambdaExpressionHelper.GenerateExpression<TModel, TimeSpan>(property, null));
                        break;
                }
            }

            return mvcElement;
        }

        /// <summary>
        /// The text box element.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <param name="Data">
        /// The data.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TGridModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString TextBoxElement<TModel, TGridModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property, int rowIndex, IList<TGridModel> Data)
        {
            bool isNullable;
            int typeCode = GridPropertyHelper.DetermineTypeCodeAndNullability(property, out isNullable);
            string strColumnCustomFormat = GridPropertyHelper.RetrieveColumnCustomFormat(property, rowIndex);

            MvcHtmlString mvcElement = null;
            if (isNullable)
            {
                switch (typeCode)
                {
                    case (int)TypeCode.Int32:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, int?>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Int64:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, long?>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Decimal:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, decimal?>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.DateTime:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, DateTime?>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Boolean:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, bool?>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case (int)TypeCode.String:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, string>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Int32:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, int>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Int64:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, long>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Decimal:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, decimal>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.DateTime:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, DateTime>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Boolean:
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, bool>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Object:
                        string textBoxValue = string.Empty;

                        // since first row is template row
                        if (rowIndex > 0)
                        {
                            textBoxValue += string.Join(
                                ",",
                                (IEnumerable<string>)Data[rowIndex].GetType().GetProperty(property.Name).GetValue(Data[rowIndex]));
                        }

                        mvcElement = htmlHelper.TextBox(property.Name, textBoxValue, strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case 17: // 17 is for timespan which was removed from .net - http://stackoverflow.com/questions/7329834/what-happened-to-system-typecode-of-value-17
                        mvcElement = htmlHelper.TextBoxFor(GridLambdaExpressionHelper.GenerateExpression<TModel, TimeSpan>(property, rowIndex), strColumnCustomFormat, GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }

            return mvcElement;
        }

        /// <summary>
        /// The key element.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString KeyElement<TModel>(this HtmlHelper<TModel> htmlHelper, PropertyInfo property, int rowIndex)
        {
            bool isNullable;
            int typeCode = GridPropertyHelper.DetermineTypeCodeAndNullability(property, out isNullable);

            MvcHtmlString mvcElement = null;
            if (isNullable)
            {
                switch (typeCode)
                {
                    case (int)TypeCode.Int32:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, int?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Int64:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, long?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Decimal:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, decimal?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.DateTime:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, DateTime?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Boolean:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, bool?>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }
            else
            {
                switch (typeCode)
                {
                    case (int)TypeCode.String:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, string>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Int32:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, int>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Int64:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, long>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Decimal:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, decimal>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.DateTime:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, DateTime>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case (int)TypeCode.Boolean:
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, bool>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                    case 17: // 17 is for timespan which was removed from .net - http://stackoverflow.com/questions/7329834/what-happened-to-system-typecode-of-value-17
                        mvcElement = htmlHelper.HiddenFor(GridLambdaExpressionHelper.GenerateExpression<TModel, TimeSpan>(property, rowIndex), GridPropertyHelper.RetrieveHtmlAttributes(property));
                        break;
                }
            }

            return mvcElement;
        }
    }
}