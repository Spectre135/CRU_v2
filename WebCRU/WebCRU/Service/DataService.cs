using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using si.hit.WebCRU;

namespace si.hit.WebCRU.Service
{
    public class DataService
    {
        public List<Aplikacija> GetAplikacije(string uporabnikID)
        {

            List<Aplikacija> response = new List<Aplikacija>();

            using (CruDBEntities db = new CruDBEntities())
            {
                var apl = (from a in db.Aplikacija
                           join v in db.Vloge on a.AplikacijaKLJ equals v.AplikacijaKLJ
                           join vu in db.VlogeUporabnikov on v.VlogaKLJ equals vu.VlogaKLJ
                           join u in db.Uporabniki on vu.UporabnikKLJ equals u.UporabnikKLJ
                           where u.UporabnikID == uporabnikID
                           select new
                           {
                               Ime = a.Ime,
                               Naziv = a.Naziv,
                               Opis = a.Opis,
                               AplikacijaKLJ = a.AplikacijaKLJ


                           }).ToList();

                foreach (var a in apl)
                {
                    Aplikacija e = new Aplikacija
                    {
                        Naziv = a.Naziv,
                        Ime = a.Ime,
                        Opis = a.Opis
                    };
                    response.Add(e);
                }

                return response;

            }

        }
    }
}