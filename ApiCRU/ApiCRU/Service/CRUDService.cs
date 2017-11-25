using System.Collections.Generic;
using System.Linq;
using ApiCRU.Models;
using System.Data.Entity;
using System;
using ApiCRU.DTO;
using ApiCRU.DAO;
using NLog.Fluent;

namespace ApiCRU.Service
{
    public class CRUDService
    {

        //Aplikacije CRUD operation
        public List<Aplikacija> GetAplikacije(string uporabnikID)
        {

            List<Aplikacija> response = new List<Aplikacija>();

            try
            {
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
            }catch (Exception ex)
            {
                Log.Error("Napaka pri branju Aplikacije /"+ ex);
                throw new ApplicationException("Napaka pri branju Aplikacije /" + ex);
            }

            return response;

        }

        public List<DVlogePravice> GetPravice(int aplikacijaKLJ, int vlogaKLJ)
        {
            IQueryable<DVlogePravice> vloge;

            try
            {

                using (Entities db = new Entities())
                {
                    vloge = (from p in db.Pravices
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


                }
            }
            catch (Exception ex)
            {
                Log.Error("Napaka pri branju Pravic /" + ex);
                throw new ApplicationException("Napaka pri branju Pravic /" + ex);
            }

            return vloge.ToList();

        }

        public List<Uporabniki> GetUporabniki()
        {
            List<Uporabniki> response = new List<Uporabniki>();

            try
            {
                using (Entities db = new Entities())
                {
                    IQueryable<Uporabniki> uprb = db.Uporabnikis;

                    response = uprb.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Napaka pri branju Uporabnikov /" + ex);
                throw new ApplicationException("Napaka pri branju Uporabnikov /" + ex);
            }

            return response;
        }

        public static Uporabniki GetUporabnik(string UserName)
        {
            Uporabniki response = new Uporabniki();

            try
            {

                using (Entities db = new Entities())
                {
                    response = db.Uporabnikis.Where(b => b.UporabnikID == UserName).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Napaka pri branju Uporabnikov /" + ex);
                throw new ApplicationException("Napaka pri branju Uporabnikov /" + ex);
            }

            return response;
        }

        public void Save(Object obj, DbSet set, long ID)
        {

            try
            {
                using (Entities db = new Entities())
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if (ID != 0)
                            {
                                set.Attach(obj);
                                db.Entry(obj).State = EntityState.Modified;
                            }
                            else
                            {
                                set.Add(obj);
                                db.Entry(obj).State = EntityState.Added;
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
            catch (Exception ex)
            {
                Log.Error("Napaka pri branju Uporabnikov /" + ex);
                throw new ApplicationException("Napaka pri branju Uporabnikov /" + ex);
            }
        }

        public void Delete(Object obj, DbSet set)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            set.Attach(obj);
                            set.Remove(obj);
                            db.Entry(obj).State = EntityState.Deleted;

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
            catch (Exception ex)
            {
                Log.Error("Napaka pri branju Uporabnikov /" + ex);
                throw new ApplicationException("Napaka pri branju Uporabnikov /" + ex);
            }
        }

        /*Berem SQLite bazo preko navadnega selecta*/
        public DResponse GetDResponse(string SearchString, int pageIndex, int pageSelected, string sortKey, string asc)
        {
            DaoService dao = new DaoService();
            DResponse dto;

            try
            {
                dto = new DResponse
                {
                    DataList = dao.GetData(SearchString.Replace("undefined", ""), pageIndex, pageSelected, sortKey, asc),
                    //RowsCount = dao.GetRowsCount(SearchString)
                    RowsCount = 10
                };
            }
            catch (Exception ex)
            {
                Log.Error("Napaka pri branju Aplikacij /" + ex);
                throw new ApplicationException("Napaka pri branju Aplikacij /" + ex);
            }

            return dto;

        }

        //Sifranti
        public List<DSifranti> GetDSifranti(string id)
        {
            List<DSifranti> dto;

            try
            {
                dto = DaoService.GetSifranti(id).ToList();
            }
            catch (Exception ex)
            {
                Log.Error("Napaka pri branju Šifrantov /" + ex);
                throw new ApplicationException("Napaka pri branju Šifrantov /" + ex);
            }

            return dto;

        }

        public static Pravice ParsePravice(DVlogePravice dVlogePravice)
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

        public static VlogePravice ParseVlogePravice(DVlogePravice dVlogePravice)
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