// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridEntityPropertyAttribute.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Entity Property Attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linq.Query.Composer.GridAttributes
{
    using System;

    /// <summary>
    /// The grid entity property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GridEntityPropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the targeted property path.
        /// </summary>
        public string[] TargetedPropertyPath { get; set; }
    }
}