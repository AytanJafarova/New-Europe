//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewEurope
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Users
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int CustomerID { get; set; }
    
        public virtual Tbl_Customer Tbl_Customer { get; set; }
    }
}
