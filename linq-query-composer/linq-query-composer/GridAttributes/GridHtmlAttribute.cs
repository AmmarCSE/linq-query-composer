// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridHtmlAttribute.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Html Attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.GridAttributes
{
    using System;

    /// <summary>
    /// The grid html attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public class GridHtmlAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the attr.
        /// </summary>
        public string Attr { get; set; }

        /// <summary>
        /// Gets or sets the attr val.
        /// </summary>
        public object AttrVal { get; set; }
    }
}