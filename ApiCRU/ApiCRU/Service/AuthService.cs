using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCRU.Models;

namespace WebCRU.Service
{
    public class AuthService
    {
        public void SaveNewSession(AuthSession _AuthSession)
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
    }
}