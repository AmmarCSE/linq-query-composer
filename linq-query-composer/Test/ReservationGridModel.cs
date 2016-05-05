namespace Travel.Agency.BusnissLogic.Models.SearchModels.GridModels
{
    using DBContext;
    using System.ComponentModel.DataAnnotations;
    using Linq.Query.Composer.Model;
    using Linq.Query.Composer.Model.Attribute;

    public class ReservationDataModel : DataModel<Reservation, ReservationDataModel>
    {
        [Key]
        [DataEntityProperty(TargetedPropertyPath = new[] { "ReservationID" })]
        public int ReservationId { get; set; }

        [DataEntityProperty(TargetedPropertyPath = new[] { "SequanceID" })]
        public long SequanceID { get; set; }

        [DataEntityProperty(TargetedPropertyPath = new[] { "FromDate" })]
        public string strFromDate { get; set; }

        [DataEntityProperty(TargetedPropertyPath = new[] { "ToDate" })]
        public string strToDate { get; set; }

        [DataEntityProperty(TargetedPropertyPath = new[] { "Hotel", "HotelName" })]
        public string HotelName { get; set; }

        [DataEntityProperty(TargetedPropertyPath = new[] { "GuestName" })]
        public string GuestName { get; set; }

        [Key]
        [DataEntityProperty(TargetedPropertyPath = new[] { "PostToAccount" })]
        public bool? PostToAccount { get; set; }

        [DataComputedProperty]
        public string PostToAccountString
        {
            get
            {
                return this.PostToAccount.HasValue && this.PostToAccount.Value ? "Yes" : "No";
            }
        }

        [DataEntityProperty(TargetedPropertyPath = new[] { "ReservationStatus", "Name" })]
        public string ReservationStatusName { get; set; }
    }
}