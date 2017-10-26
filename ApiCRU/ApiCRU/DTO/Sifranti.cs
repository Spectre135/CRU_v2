using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCRU.DAO;

namespace WebCRU.DTO
{
    public class DSifranti
    {
        [DataFieldAttribute("id")]
        public int SifrantKLJ { get; set; }

        [DataFieldAttribute("Name")]
        public string Ime { get; set; }

    }
}