// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridActionAttribute.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   //   The Initial Developer of the Original Code is AnnabSoft.
//   //   All Rights Reserved.
// </copyright>
// <summary>
//   Defines Grid ActionAttribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.RazorGrid.GridAttributes
{
    using System;

    /// <summary>
    /// The grid action attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class GridActionAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the action val.
        /// </summary>
        public object ActionVal { get; set; }
    }
}