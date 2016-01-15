// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuickSearchAttribute.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Quick Search Attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.GridAttributes
{
    using System;

    /// <summary>
    /// The quick search attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class QuickSearchAttribute : Attribute
    {
    }
}