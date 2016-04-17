// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReservationGridModel.cs" company="AnnabSoft">
//   The Original Code is TAS.
//   The Initial Developer of the Original Code is AnnabSoft.
//   All Rights Reserved.
// </copyright>
// <summary>
//   Defines the Reservation Grid Model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Travel.Agency.BusnissLogic.Models.SearchModels.GridModels
{
    using DBContext;
    using System.ComponentModel.DataAnnotations;
    using Linq.Query.Composer.GridAttributes;
    using Linq.Query.Composer.GridResources;

    public class ReservationGridModel : GridModel<Reservation, ReservationGridModel>
    {
        /// <summary>
        /// Gets or sets the reservation id.
        /// </summary>
        [Key]
        [GridEntityProperty(TargetedPropertyPath = new[] { "ReservationID" })]
        public int ReservationId { get; set; }

        /// <summary>
        /// Gets or sets the sequance id.
        /// </summary>
        [GridEntityProperty(TargetedPropertyPath = new[] { "SequanceID" })]
        public long SequanceID { get; set; }

        /// <summary>
        /// Gets or sets the str from date.
        /// </summary>
        [GridEntityProperty(TargetedPropertyPath = new[] { "FromDate" })]
        public string strFromDate { get; set; }

        /// <summary>
        /// Gets or sets the str to date.
        /// </summary>
        [GridEntityProperty(TargetedPropertyPath = new[] { "ToDate" })]
        public string strToDate { get; set; }

        /// <summary>
        /// Gets or sets the hotel name.
        /// </summary>
        [GridEntityProperty(TargetedPropertyPath = new[] { "Hotel", "HotelName" })]
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the guest name.
        /// </summary>
        [GridEntityProperty(TargetedPropertyPath = new[] { "GuestName" })]
        public string GuestName { get; set; }

        /// <summary>
        /// Gets or sets the post to account.
        /// </summary>
        [Key]
        [GridEntityProperty(TargetedPropertyPath = new[] { "PostToAccount" })]
        public bool? PostToAccount { get; set; }

        [GridComputedProperty]
        public string PostToAccountString
        {
            get
            {
                return this.PostToAccount.HasValue && this.PostToAccount.Value ? "Yes" : "No";
            }
        }
        /*public string PostToAccountString
        {
            get
            {
                return this.PostToAccount.HasValue && this.PostToAccount.Value ? "Yes" : "No";
            }
        }*/

        [GridEntityProperty(TargetedPropertyPath = new[] { "ReservationStatus", "Name" })]
        public string ReservationStatusName { get; set; }
    }
}