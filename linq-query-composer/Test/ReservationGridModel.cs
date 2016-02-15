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
    using System.ComponentModel.DataAnnotations;

    using Travel.Agency.EntityFramework;
    using Travel.Agency.RazorGrid.GridAttributes;
    using Travel.Agency.RazorGrid.GridResources;

    /// <summary>
    /// The reservation grid model.
    /// </summary>
    [GridAction(Action = "data-add-link", ActionVal = "GeneralInfo?ReservationID=0,Reservations,Reservations")]
    [GridAction(Action = "data-edit-link", ActionVal = "GeneralInfo?ReservationID=,Reservations,Reservations")]
    [GridAction(Action = "data-delete-link", ActionVal = "DeleteBatchReservation,Reservations,Reservations")]
    [GridAction(Action = "data-search-link", ActionVal = "GetReservationForSearch,Reservations,Reservations")]
    [GridAction(Action = "data-activate-link", ActionVal = "ActiveBatchReservation,Reservations,Reservations")]
    [GridAction(Action = "data-unactivate-link", ActionVal = "DeactiveBatchReservation,Reservations,Reservations")]
    public class ReservationGridModel : GridModel<Reservation, ReservationGridModel>
    {
        /// <summary>
        /// Gets or sets the reservation id.
        /// </summary>
        [Key]
        [GridHtml(Attr = "data-row-key", AttrVal = "primary")]
        [GridEntityProperty(TargetedPropertyPath = new[] { "ReservationID" })]
        public int ReservationId { get; set; }

        /// <summary>
        /// Gets or sets the sequance id.
        /// </summary>
        [QuickSearch]
        [Display(Name = @"Reservation ID")]
        [GridHtml(Attr = "style", AttrVal = "width:10.3%")]
        [GridEntityProperty(TargetedPropertyPath = new[] { "SequanceID" })]
        public long SequanceID { get; set; }

        /// <summary>
        /// Gets or sets the str from date.
        /// </summary>
        [GridHtml(Attr = "style", AttrVal = "width:12%")]
        [Display(Name = @"Arrival")]
        [GridEntityProperty(TargetedPropertyPath = new[] { "FromDate" })]
        public string strFromDate { get; set; }

        /// <summary>
        /// Gets or sets the str to date.
        /// </summary>
        [GridHtml(Attr = "style", AttrVal = "width:12%")]
        [Display(Name = @"Departure")]
        [GridEntityProperty(TargetedPropertyPath = new[] { "ToDate" })]
        public string strToDate { get; set; }

        /// <summary>
        /// Gets or sets the hotel name.
        /// </summary>
        [QuickSearch]
        [GridHtml(Attr = "style", AttrVal = "width:19%")]
        [Display(Name = @"Hotel Name")]
        [GridEntityProperty(TargetedPropertyPath = new[] { "Hotel", "HotelName" })]
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the guest name.
        /// </summary>
        [QuickSearch]
        [GridHtml(Attr = "style", AttrVal = "width:12%")]
        [Display(Name = @"Guest Name")]
        [GridEntityProperty(TargetedPropertyPath = new[] { "GuestName" })]
        public string GuestName { get; set; }

        /// <summary>
        /// Gets or sets the post to account.
        /// </summary>
        [Key]
        [GridEntityProperty(TargetedPropertyPath = new[] { "PostToAccount" })]
        public bool? PostToAccount { get; set; }

        /// <summary>
        /// Gets the post to account string.
        /// </summary>
        [GridHtml(Attr = "style", AttrVal = "width:12%")]
        [Display(Name = @"Posted")]
        [CssWithConditionAttribute(ClassName = "NoClass", ShouldEqual = "No")]
        [CssWithConditionAttribute(ClassName = "YesClass", ShouldEqual = "Yes")]
        [GridComputedProperty]
        public string PostToAccountString
        {
            get
            {
                return this.PostToAccount.HasValue && this.PostToAccount.Value ? "Yes" : "No";
            }
        }

        /// <summary>
        /// Gets or sets the reservation status name.
        /// </summary>
        [QuickSearch]
        [GridHtml(Attr = "style", AttrVal = "width:12%")]
        [Display(Name = @"Status")]
        [CssWithConditionAttribute(ClassName = "ReservationConfirmed", ShouldEqual = "Confirmed")]
        [CssWithConditionAttribute(ClassName = "ReservationTentative", ShouldEqual = "Tentative")]
        [GridEntityProperty(TargetedPropertyPath = new[] { "ReservationStatus", "Name" })]
        public string ReservationStatusName { get; set; }
    }
}