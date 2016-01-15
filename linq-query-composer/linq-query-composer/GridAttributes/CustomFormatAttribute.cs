// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomFormatAttribute.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Custom Format Attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.GridAttributes
{
    using System;

    using Travel.Agency.RazorGrid.GridResources;

    /// <summary>
    /// The custom format attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CustomFormatAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the column custom format.
        /// </summary>
        public GridEnums.ColumnCustomFormat ColumnCustomFormat { get; set; }

        /// <summary>
        /// Gets or sets the custom format.
        /// </summary>
        public string CustomFormat { get; set; }
    }
}
