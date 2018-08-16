using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace zSpaceWinApp.Logs
{
    public class DAL
    {
        private static string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private static string ConnectionString = "Data Source=" + directory + "\\Logs.db3;Version=3;New=False;Compress=True;UTF8Encoding=True";
        public static void ExecQuery(string query, List<SQLiteParameter> param)
        {
            using (var cnn = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        if (param != null)
                        {
                            foreach (var p in param)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cnn.Close();
                }
            }           
        }

        public static DataTable GetList(string query)
        {
            DataTable dt = new DataTable();
            using (var connect = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connect.Open();
                    using (var cmd = connect.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            dt.Load(rdr);
                        }                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
            return dt;
        }
    }
}
