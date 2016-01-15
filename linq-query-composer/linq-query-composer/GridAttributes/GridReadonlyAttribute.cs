// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridReadonlyAttribute.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid Readonly Attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.GridAttributes
{
    using System;

    /// <summary>
    /// The grid readonly attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class GridReadonlyAttribute : Attribute
    {
    }
}