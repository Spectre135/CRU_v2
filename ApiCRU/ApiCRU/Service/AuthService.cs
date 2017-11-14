using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiCRU.Models;

namespace WebCRU.Service
{
    public class AuthService
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
    }
}