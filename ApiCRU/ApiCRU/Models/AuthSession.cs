//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiCRU.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuthSession
    {
        public string SessionToken { get; set; }
        public Nullable<long> UporabnikKLJ { get; set; }
        public Nullable<long> SessionTimeOut { get; set; }
        public Nullable<System.DateTime> Issued { get; set; }
        public Nullable<System.DateTime> LastAccessed { get; set; }
        public Nullable<System.DateTime> Expired { get; set; }
    }
}
