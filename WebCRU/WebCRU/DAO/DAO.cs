using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using WebCRU.DTO;
using System.Data.SQLite;

namespace WebCRU.DAO
{
    public class DAO
    {

        public IEnumerable<DAplikacija> GetData(string query, int pageIndex, int pageSize, string sortKey, string asceding)
        {

            try
            {

                List<DAplikacija> result = new List<DAplikacija>();

                SQLiteConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

                using (connection)
                {
                    connection.Open();
                    SQLiteCommand cmd;
                    using (cmd = new SQLiteCommand(this.GetSQL(sortKey, asceding), connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@searchParam", "%" + query + "%");
                        //cmd.Parameters.AddWithValue("@pageSize", pageSize);
                        //cmd.Parameters.AddWithValue("@pageIndex", pageIndex);

                        SQLiteDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(ReflectPropertyInfo.ReflectType<DAplikacija>(reader));
                            }
                        }
                    }

                }
                connection.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                //connection.Close(); //tega ne rabimo če uporabimo using
            }
        }

        public int GetRowsCount(string query)
        {
            int pageCount = 0;

            try
            {

                SQLiteConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

                using (connection)
                {
                    connection.Open();
                    SQLiteCommand cmd;
                    using (cmd = new SQLiteCommand(this.GetSQLRowsCount(), connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@searchParam", "%" + query + "%");

                        SQLiteDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pageCount = (int)reader[0];
                            }
                        }
                    }

                }

                return pageCount;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                //throw;
                return 10; //kar neki vrnemo ker ne vem zakaj SQLite jamra
            }
        }

        private string GetSQL(string sortKey, string asceding)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("Select  * from Aplikacija_view ");
            sbSql.Append(" where ").Append(ReflectPropertyInfo.GetSearchFields(new DAplikacija())).Append(" like @searchParam ");
            //sbSql.Append(" ORDER BY ");
            //sbSql.Append(sortKey.Equals("null") ? " Datum " : sortKey + (asceding.Equals("true") ? " ASC " : " DESC "));
            //sbSql.Append(" OFFSET @pageSize*(@pageIndex-1) ROWS FETCH NEXT @pageSize ROWS ONLY;");

            return sbSql.ToString();

        }

        private string GetSQLRowsCount()
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("Select cast(count(*) as int) count from Aplikacija_view ");
            sbSql.Append(" where ").Append(ReflectPropertyInfo.GetSearchFields(new DAplikacija())).Append(" like @searchParam ");

            return sbSql.ToString();

        }
        
    }
}