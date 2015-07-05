using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace jxGameFramework.Data
{
    public class SQLiteInterop : IDisposable
    {
        public SQLiteInterop(string dbfile)
        {
            this.DBFile = dbfile;
            CreateConnection();
        }
        public string DBFile { get; set; }
        static SQLiteConnection conn;
        private void CreateConnection()
        {
            string connectString = string.Format(@"Data Source={0};Pooling=true;FailIfMissing=false", Path.Combine(System.Windows.Forms.Application.StartupPath, DBFile));
            conn = new SQLiteConnection(connectString);
            conn.Open();
        }
        private SQLiteCommand createCmd(string sql)
        {
            if (conn == null) CreateConnection();
            var cmd = new SQLiteCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        public int runSql(string sql)
        {
            var cmd = createCmd(sql);
            return Convert.ToInt32(cmd.ExecuteNonQuery());
        }
        public object getData(string sql)
        {
            var cmd = createCmd(sql);
            return cmd.ExecuteScalar();
        }
        public DataSet getDataSet(string sql)
        {
            var da = new SQLiteDataAdapter();
            da.SelectCommand = createCmd(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
        }
    }
}
