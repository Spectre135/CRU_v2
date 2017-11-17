using System;
using System.Collections.Generic;
using System.Linq;
using WcfAuth.Models;
using WCFAuthConsole.DTO;

namespace WcfAuth.DAO
{
    class DAOService
    {
        public static void SaveNewSession(AuthSession authSession)
        {

            using (Entities db = new Entities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.AuthSession.Add(authSession);

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

        public static AuthSession GetSession(string sessionToken)
        {
            AuthSession response = new AuthSession();

            using (Entities db = new Entities())
            {
                response = db.AuthSession.Where(b => b.SessionToken == sessionToken).FirstOrDefault();
            }

            return response;
        }

        public static Uporabniki GetUporabnik(string userName)
        {
            Uporabniki response = new Uporabniki();

            using (Entities db = new Entities())
            {
                response = db.Uporabnikis.Where(b => b.UporabnikID == userName).FirstOrDefault();
            }

            return response;
        }

        public static List<DPravice> GetPravice(string userName, string apl)
        {
            List<DPravice> response = new List<DPravice>();

            using (Entities db = new Entities())
            {
                IQueryable<DPravice> pravice =(from p  in db.Pravices 
                                              join a  in db.Aplikacijas on p.AplikacijaKLJ equals a.AplikacijaKLJ
                                              join vp in db.VlogePravices on p.PravicaKLJ equals vp.PravicaKLJ
                                              join v  in db.Vloges on vp.VlogaKLJ  equals v.VlogaKLJ
                                              join vu in db.VlogeUporabnikovs on v.VlogaKLJ equals vu.VlogaKLJ
                                              join u  in db.Uporabnikis on vu.UporabnikKLJ equals u.UporabnikKLJ
                                              where u.UporabnikID == userName && a.Naziv == apl
                                              select new DPravice()
                                               {
                                                   Naziv = p.Naziv,
                                                   Opis  = p.Opis
                                               });

                response = pravice.ToList();

            }

            return response;
        }
    }
}
