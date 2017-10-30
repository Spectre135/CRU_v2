using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCRU.DAO;

namespace WebCRU.DTO
{
    public class DSifranti
    {
        [DataFieldAttribute("SifrantiKLJ")]
        public int Id { get; set; }

        [DataFieldAttribute("Naziv")]
        public string Naziv { get; set; }

    }
}