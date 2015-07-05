using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace WarshipGirl.Utilities
{
    static class DBInterface
    {
        public const string DBFile="database.db";
        static SQLiteConnection conn;
        private static void CreateConnection()
        {
            string connectString = string.Format(@"Data Source={0};Pooling=true;FailIfMissing=false", Path.Combine(System.Windows.Forms.Application.StartupPath, DBFile));
            conn = new SQLiteConnection(connectString);
            conn.Open();
        }
        private static SQLiteCommand createCmd(string sql)
        {
            if (conn == null) CreateConnection();
            var cmd = new SQLiteCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        public static int runSql(string sql)
        {
            var cmd = createCmd(sql);
            return Convert.ToInt32(cmd.ExecuteNonQuery());
        }
        public static object getData(string sql)
        {
            var cmd = createCmd(sql);
            return cmd.ExecuteScalar();
        }
        public static DataSet getDataSet(string sql)
        {
            var da = new SQLiteDataAdapter();
            da.SelectCommand = createCmd(sql);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
