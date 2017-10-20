using System.Collections.Generic;
using System.Linq;
using WebCRU.Models;
using System.Web.Script.Serialization;
using WebCRU.DAO;
using WebCRU.DTO;

namespace si.hit.WebCRU.Service
{
    public class DAOService
    {

        public List<Aplikacija> GetAplikacije(string uporabnikID)
        {

            List<Aplikacija> response = new List<Aplikacija>();

            using (CruDBEntities db = new CruDBEntities())
            {
                IQueryable<Aplikacija> apl = from a in db.Aplikacija
                                             join v in db.Vloge on a.AplikacijaKLJ equals v.AplikacijaKLJ
                                             join vu in db.VlogeUporabnikov on v.VlogaKLJ equals vu.VlogaKLJ
                                             join u in db.Uporabniki on vu.UporabnikKLJ equals u.UporabnikKLJ
                                             where u.UporabnikID == uporabnikID
                                             select a;

                response = apl.ToList();

            }

            return response;

        }

        public void AddAplikacija(Aplikacija aplikacija)
        {
            using (CruDBEntities db = new CruDBEntities())
            {
                db.Aplikacija.Add(aplikacija);
            }

        }

        public List<Uporabniki> GetUporabniki()
        {
            List<Uporabniki> response = new List<Uporabniki>();

            using (CruDBEntities db = new CruDBEntities())
            {
                IQueryable<Uporabniki> uprb = db.Uporabniki;

                response = uprb.ToList();
            }

            return response;
        }

        /*Berem SQLite bazo preko navadnega selecta*/
        public DResponse GetDResponse(string SearchString, int pageIndex, int pageSelected, string sortKey, string asc)
        {
            DAO dao = new DAO();

            DResponse dto = new DResponse
            {
                DataList = dao.GetData(SearchString.Replace("undefined", ""), pageIndex, pageSelected, sortKey, asc),
                RowsCount = dao.GetRowsCount(SearchString)
            };

            //return JsonConvert.SerializeObject(dto, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy" });

            return dto;

        }

    }
}