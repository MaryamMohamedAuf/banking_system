//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace banking_system.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transfer
    {
        public int id { get; set; }
        public Nullable<int> from_customer_id { get; set; }
        public Nullable<int> to_customer_id { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<System.DateTime> transfer_date { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Customer Customer1 { get; set; }
    }
}
