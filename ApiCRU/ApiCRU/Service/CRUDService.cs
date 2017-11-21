using System.Collections.Generic;
using System.Linq;
using ApiCRU.Models;
using System.Data.Entity;
using System;
using ApiCRU.DTO;
using ApiCRU.DAO;

namespace ApiCRU.Service
{
    public class CRUDService
    {

        //Aplikacije CRUD operation

        //Aplikacije handle
        public List<Aplikacija> GetAplikacije(string uporabnikID)
        {

            List<Aplikacija> response = new List<Aplikacija>();

            using (Entities db = new Entities())
            { 
                IQueryable<Aplikacija> apl = from a in db.Aplikacijas
                                             join v in db.Vloges on a.AplikacijaKLJ equals v.AplikacijaKLJ
                                             join vu in db.VlogeUporabnikovs on v.VlogaKLJ equals vu.VlogaKLJ
                                             join u in db.Uporabnikis on vu.UporabnikKLJ equals u.UporabnikKLJ
                                             where u.UporabnikID == uporabnikID
                                             select a;

                response = apl.ToList();

            }

            return response;

        }

        public void SaveAplikacija(Aplikacija aplikacija)
        {

            using (Entities db = new Entities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (aplikacija.AplikacijaKLJ == 0)
                        {
                            db.Aplikacijas.Add(aplikacija);
                        }
                        else
                        {
                            db.Aplikacijas.Attach(aplikacija);
                            db.Entry(aplikacija).State = EntityState.Modified;
                        }

                        db.SaveChanges();
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex.InnerException;
                    }
                }

            }
        }

        public void DeleteAplikacija(Aplikacija aplikacija)
        {

            using (Entities db = new Entities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (aplikacija != null)
                        {
                            db.Aplikacijas.Attach(aplikacija);
                            db.Aplikacijas.Remove(aplikacija);
                            db.SaveChanges();
                            tran.Commit();
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }

            }
        }

        //Pravice handle
        public List<DVlogePravice> GetPravice(int aplikacijaKLJ, int vlogaKLJ)
        {


            using (Entities db = new Entities())
            {
                IQueryable<DVlogePravice> vloge = (from p in db.Pravices
                                            join vp in db.VlogePravices on p.PravicaKLJ equals vp.PravicaKLJ
                                            join v in db.Vloges on vp.VlogaKLJ equals v.VlogaKLJ
                                            join a in db.Aplikacijas on p.AplikacijaKLJ equals a.AplikacijaKLJ
                                            where p.AplikacijaKLJ == (aplikacijaKLJ == 0 ? p.AplikacijaKLJ : aplikacijaKLJ) &&
                                                  v.VlogaKLJ == (vlogaKLJ == 0 ? v.VlogaKLJ : vlogaKLJ)
                                            select new DVlogePravice()
                                            {
                                                AplikacijaKLJ = a.AplikacijaKLJ,
                                                AplikacijaNaziv = a.Opis,
                                                PravicaKLJ = p.PravicaKLJ,
                                                PravicaNaziv = p.Naziv,
                                                PravicaOpis = p.Opis,
                                                VlogaKLJ = v.VlogaKLJ,
                                                VlogaNaziv = v.Naziv
                                            });




                return vloge.ToList();

            }
        }

        public void SavePravice(DVlogePravice dVlogePravice)
        {
            Pravice pravice = this.ParsePravice(dVlogePravice);

            using (Entities db = new Entities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (pravice.PravicaKLJ == 0)
                        {
                            db.Pravices.Add(pravice);
                            db.VlogePravices.Add(this.ParseVlogePravice(dVlogePravice));

                        }
                        else
                        {
                            db.Pravices.Attach(pravice);
                            db.Entry(pravice).State = EntityState.Modified;
                        }

                        db.SaveChanges();
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex.InnerException;
                    }
                }

            }
        }

        public void DeletePravice(DVlogePravice dVlogePravice)
        {
            Pravice pravice = this.ParsePravice(dVlogePravice);

            using (Entities db = new Entities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (pravice!= null)
                        {
                            db.Pravices.Attach(pravice);
                            db.Pravices.Remove(pravice);
                            db.SaveChanges();
                            tran.Commit();
                        }

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }

            }
        }

        //Uporabniki handle
        public List<Uporabniki> GetUporabniki()
        {
            List<Uporabniki> response = new List<Uporabniki>();

            using (Entities db = new Entities())
            {
                IQueryable<Uporabniki> uprb = db.Uporabnikis;

                response = uprb.ToList();
            }

            return response;
        }

        public void SaveUporabniki(Uporabniki uporabniki)
        {

            using (Entities db = new Entities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (uporabniki.UporabnikKLJ == 0)
                        {
                            db.Uporabnikis.Add(uporabniki);
                        }
                        else
                        {
                            db.Uporabnikis.Attach(uporabniki);
                            db.Entry(uporabniki).State = EntityState.Modified;
                        }

                        db.SaveChanges();
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex.InnerException;
                    }
                }

            }
        }

        public void DeleteUporabniki(Uporabniki uporabniki)
        {

            using (Entities db = new Entities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (uporabniki != null)
                        {
                            db.Uporabnikis.Attach(uporabniki);
                            db.Uporabnikis.Remove(uporabniki);
                            db.SaveChanges();
                            tran.Commit();
                        }

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }

            }
        }

        public static Uporabniki GetUporabnik(string UserName)
        {
           Uporabniki response = new Uporabniki();

            using (Entities db = new Entities())
            {
                response = db.Uporabnikis.Where(b => b.UporabnikID == UserName).FirstOrDefault(); 
            }

            return response;
        }


        //Ostalo
        /*Berem SQLite bazo preko navadnega selecta*/
        public DResponse GetDResponse(string SearchString, int pageIndex, int pageSelected, string sortKey, string asc)
        {
            DaoService dao = new DaoService();

            DResponse  dto = new DResponse
            {
                DataList = dao.GetData(SearchString.Replace("undefined", ""), pageIndex, pageSelected, sortKey, asc),
                //RowsCount = dao.GetRowsCount(SearchString)
                RowsCount = 10
            };

            return dto;

        }

        //Sifranti
        public List<DSifranti> GetDSifranti(string id)
        {

            List<DSifranti> dto = DaoService.GetSifranti(id).ToList();

            return dto;

        }

        //Utils
        private Pravice ParsePravice(DVlogePravice dVlogePravice)
        {
            Pravice pravice = new Pravice
            {
                PravicaKLJ = dVlogePravice.PravicaKLJ,
                AplikacijaKLJ = dVlogePravice.AplikacijaKLJ,
                Naziv = dVlogePravice.PravicaNaziv,
                Opis = dVlogePravice.PravicaOpis
            };

            return pravice;
        }

        private VlogePravice ParseVlogePravice(DVlogePravice dVlogePravice)
        {
            VlogePravice vlogePravice = new VlogePravice
            {
                VlogaKLJ = dVlogePravice.VlogaKLJ,
                PravicaKLJ = dVlogePravice.PravicaKLJ
            };

            return vlogePravice;
        }

    }
}