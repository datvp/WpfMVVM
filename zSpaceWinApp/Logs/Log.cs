using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSpaceWinApp.Logs
{
    public class Log
    {
        public static void Info(Model.Downloads m)
        {
            var query = "INSERT INTO Downloads(DeviceId,PackageId,DownloadedOn)VALUES(@DeviceId,@PackageId,@DownloadedOn)";
            List<SQLiteParameter> param = new List<SQLiteParameter>();
            param.Add(new SQLiteParameter("@DeviceId", m.DeviceId));
            param.Add(new SQLiteParameter("@PackageId", m.PackageId));
            param.Add(new SQLiteParameter("@DownloadedOn", m.DownloadedOn));
            DAL.ExecQuery(query, param);
        }

        public static void Error(string err, string funcId)
        {
            if (string.IsNullOrEmpty(err) || string.IsNullOrEmpty(funcId)) return;

            Model.Errors m = new Model.Errors();
            m.FuncId = funcId;
            m.Description = err;
            m.OccuredOn = DateTime.Now.ToString("yyyy-MM-dd HH:MM:tt");
            m.ModifiedBy = "System";

            var query = "INSERT INTO Errors(FuncId,Description,OccuredOn,ModifiedBy)VALUES(@FuncId,@Description,@OccuredOn,@ModifiedBy)";
            List<SQLiteParameter> param = new List<SQLiteParameter>();
            param.Add(new SQLiteParameter("@FuncId", m.FuncId));
            param.Add(new SQLiteParameter("@Description", m.Description));
            param.Add(new SQLiteParameter("@OccuredOn", m.OccuredOn));
            param.Add(new SQLiteParameter("@ModifiedBy", m.ModifiedBy));
            DAL.ExecQuery(query, param);
        }

        public static ObservableCollection<Model.Errors> GetListErrors()
        {
            ObservableCollection<Model.Errors> collection = new ObservableCollection<Model.Errors>();
            string query = "Select * from Errors";
            var dt = DAL.GetList(query);
            if (dt == null) return collection;
            foreach (DataRow r in dt.Rows)
            {
                Model.Errors m = new Model.Errors();
                m.FuncId = r["FuncId"].ToString();
                m.Description = r["Description"].ToString();
                m.OccuredOn = r["OccuredOn"].ToString();
                m.ModifiedBy = r["ModifiedBy"].ToString();
                collection.Add(m);
            }
            return collection;
        }        
    }
}
