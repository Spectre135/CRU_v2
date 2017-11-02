using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCRU.DAO;

namespace WebCRU.DTO
{
    public class DVloge
    {
        [DataFieldAttribute("AplikacijaKLJ")]
        public int AplikacijaKLJ { get; set; }

        [DataFieldAttribute("AplikacijaNaziv")]
        [DataSearchAttribute("AplikacijaNaziv")]
        public string AplikacijaNaziv { get; set; }

        [DataFieldAttribute("VlogaKLJ")]
        public string VlogaKLJ { get; set; }

        [DataFieldAttribute("VlogaNaziv")]
        [DataSearchAttribute("VlogaNaziv")]
        public string VlogaNaziv { get; set; }

        [DataFieldAttribute("PravicaKLJ")]
        public string PravicaKLJ { get; set; }

        [DataFieldAttribute("PravicaNaziv")]
        [DataSearchAttribute("PravicaNaziv")]
        public string PravicaNaziv { get; set; }

        [DataFieldAttribute("PravicaOpis")]
        [DataSearchAttribute("PravicaOpis")]
        public string PravicaOpis { get; set; }

    }
}