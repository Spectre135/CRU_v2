using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfAuth.Models;

namespace WcfAuth.DAO
{
    class DAOService
    {
        public static void SaveNewSession(AuthSession _AuthSession)
        {

            using (Entities db = new Entities())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.AuthSession.Add(_AuthSession);

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

        public static AuthSession GetSession(string SessionToken)
        {
            AuthSession response = new AuthSession();

            using (Entities db = new Entities())
            {
                response = db.AuthSession.Where(b => b.SessionToken == SessionToken).FirstOrDefault();
            }

            return response;
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
    }
}
