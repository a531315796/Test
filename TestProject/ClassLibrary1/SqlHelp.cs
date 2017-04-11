using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

namespace DAL
{
    public class SqlHelp
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ToString();
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataReader dr = null;

        public SqlHelp() {
            con = new SqlConnection(conStr);
        }

        private SqlConnection CreateCon() {
            if (con.State == System.Data.ConnectionState.Closed) {
                con.Open();
            }
            return con;
        }

        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="para"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(SqlParameter[] para,string sql) {
            using (cmd = new SqlCommand(sql, CreateCon())) {
                if (para != null && para.Count() > 0)
                {
                    cmd.Parameters.AddRange(para);
                }
                con.Close();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="para"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteReader(SqlParameter[] para, string sql) {
            DataTable dt = new DataTable();
            using (cmd = new SqlCommand(sql, CreateCon()))
            {
                if (para != null && para.Count() > 0)
                {
                    cmd.Parameters.AddRange(para);
                }
                dt.Load(cmd.ExecuteReader());
                con.Close();
                return dt;
            }
        }

        #region 事务
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLList">SQL语句的哈希表（key为sql语句，value是该语句的OleDbParameter[]）</param>
        public bool ExecuteTransaction(Hashtable SqlList)
        {
            CreateCon();
            using (SqlTransaction trans = con.BeginTransaction())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    //循环
                    foreach (DictionaryEntry myDE in SqlList)
                    {
                        string cmdText = myDE.Key.ToString();
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                        PrepareCommand(cmd, con, trans, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            CreateCon();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
                cmd.Parameters.AddRange(cmdParms);
        }
        #endregion
    }
}
