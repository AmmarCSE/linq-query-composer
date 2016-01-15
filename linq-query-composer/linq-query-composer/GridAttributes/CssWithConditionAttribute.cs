// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CssWithConditionAttribute.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Css With Condition Attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.GridAttributes
{
    using System;

    /// <summary>
    /// The i class css with condition.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class CssWithConditionAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the class valse.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the should equal.
        /// </summary>
        public string ShouldEqual { get; set; }
    }
}
