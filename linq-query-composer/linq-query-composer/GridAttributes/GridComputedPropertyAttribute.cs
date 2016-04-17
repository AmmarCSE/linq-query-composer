// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridComputedPropertyAttribute.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Computed Property Attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linq.Query.Composer.GridAttributes
{
    using System;

    /// <summary>
    /// The grid computed property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GridComputedPropertyAttribute : Attribute
    {
    }
}