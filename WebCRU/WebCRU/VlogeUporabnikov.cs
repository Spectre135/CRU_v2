//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace si.hit.WebCRU
{
    using System;
    using System.Collections.Generic;
    
    public partial class VlogeUporabnikov
    {
        public long VlogaKLJ { get; set; }
        public long LokacijaKLJ { get; set; }
        public long UporabnikKLJ { get; set; }
    
        public virtual Lokacija Lokacija { get; set; }
        public virtual Uporabniki Uporabniki { get; set; }
        public virtual Vloge Vloge { get; set; }
    }
}
