//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReservationStatus
    {
        public ReservationStatus()
        {
            this.Reservations = new HashSet<Reservation>();
        }
    
        public int ReservationStatusID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}