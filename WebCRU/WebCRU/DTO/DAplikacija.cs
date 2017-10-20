﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCRU.DAO;

namespace WebCRU.DTO
{
    public class DAplikacija
    {
        [DataFieldAttribute("Ime")]
        [DataSearchAttribute("Ime")]
        public string Ime { get; set; }

        [DataFieldAttribute("Naziv")]
        [DataSearchAttribute("Naziv")]
        public string Naziv { get; set; }

        [DataFieldAttribute("Http")]
        public string Http { get; set; }

        [DataFieldAttribute("Opis")]
        [DataSearchAttribute("Opis")]
        public string Opis { get; set; }

        [DataFieldAttribute("DataBase")]
        public string DataBase { get; set; }


    }
}