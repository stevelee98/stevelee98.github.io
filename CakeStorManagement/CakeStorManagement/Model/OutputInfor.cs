//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CakeStorManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class OutputInfor
    {
        public int Id { get; set; }
        public string IdCake { get; set; }
        public int IdCustomer { get; set; }
        public int IdOutput { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public double OutputPrice { get; set; }
    
        public virtual Cake Cake { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Output Output { get; set; }
    }
}
