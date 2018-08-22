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
    public class Log: ILog
    {
        private bool CheckExist(string tableName, string colName, string colVal)
        {
            var query = $"SELECT {colName} FROM {tableName} WHERE {colName}=@colVal LIMIT 1";
            List<SQLiteParameter> param = new List<SQLiteParameter>();
            param.Add(new SQLiteParameter("@colVal", colVal));

            var dt = DAL.GetList(query, param);
            return dt != null && dt.Rows.Count > 0;
        }
        public void Info(Model.DownHis m)
        {            
            var query = "INSERT INTO DownHis(DriverName,TotalSize,Progress,Status,CreatedOn)VALUES(@DriverName,@TotalSize,@Progress,@Status,@CreatedOn)";
            var found = CheckExist("DownHis", "DriverName", m.DriverName);
            if (found)
            {
                query = "UPDATE DownHis SET TotalSize=@TotalSize,Progress=@Progress,Status=@Status,CreatedOn=@CreatedOn WHERE DriverName=@DriverName";
            }
            List<SQLiteParameter> param = new List<SQLiteParameter>();
            param.Add(new SQLiteParameter("@DriverName", m.DriverName));
            param.Add(new SQLiteParameter("@TotalSize", m.TotalSize));
            param.Add(new SQLiteParameter("@Progress", m.Progress));
            param.Add(new SQLiteParameter("@Status", m.Status));
            param.Add(new SQLiteParameter("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:MM tt")));
            DAL.ExecQuery(query, param);
        }

        public void Error(string err, string funcId)
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

        public ObservableCollection<Model.Errors> GetListErrors()
        {
            ObservableCollection<Model.Errors> collection = new ObservableCollection<Model.Errors>();
            string query = "Select * from Errors";
            var dt = DAL.GetList(query, null);
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

        public ObservableCollection<Model.DownHis> GetListDownHis()
        {
            ObservableCollection<Model.DownHis> collection = new ObservableCollection<Model.DownHis>();
            string query = "Select * from DownHis";
            var dt = DAL.GetList(query, null);
            if (dt == null) return collection;
            foreach (DataRow r in dt.Rows)
            {
                Model.DownHis m = new Model.DownHis();
                m.DriverName = r["DriverName"].ToString();
                m.TotalSize = int.Parse(r["TotalSize"].ToString());
                m.Progress = int.Parse(r["Progress"].ToString());
                m.Status = r["Status"].ToString();
                collection.Add(m);
            }
            return collection;
        }
    }
}
