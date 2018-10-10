using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;

namespace WorldGeneralLib.DataBaseApplication
{
    public class DataBaseHelper
    {
        #region  属性变量
        private string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        private string dbType;
        public string DbType
        {
            get
            {
                if (string.IsNullOrEmpty(dbType))
                {
                    return "Access";
                }
                return dbType;
            }
            set
            {
                if (value != null && value != string.Empty)
                {
                    dbType = value;
                }
                if (string.IsNullOrEmpty(dbType))
                {
                    this.dbType = ConfigurationManager.ConnectionStrings["datatype"].ConnectionString;
                }
                if (string.IsNullOrEmpty(dbType))
                {
                    dbType = "Access";
                }
            }

        }



        //数据访问基础类--构造函数
        public DataBaseHelper(string strConnect, string dataType)
        {
            this.ConnectionString = strConnect;
            this.DbType = dataType;
        }
        //数据访问基础类--构造函数
        public DataBaseHelper()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            this.dbType = ConfigurationManager.ConnectionStrings["datatype"].ConnectionString;
        }
        #endregion
        #region  数据类型转换
        private System.Data.IDbDataParameter Idbpara(string ParaName, string DataType)
        {
            switch (this.dbType)
            {
                case "SqlServer":
                    return GetSqlPara(ParaName, DataType);
                case "Access":
                    return GetOledbPara(ParaName, DataType);
                default:
                    return GetSqlPara(ParaName, DataType);
            }
        }

        private SqlParameter GetSqlPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Text":
                    return new SqlParameter(ParaName, SqlDbType.Text);
                case "Int":
                    return new SqlParameter(ParaName, SqlDbType.Int);
                case "DataTime":
                    return new SqlParameter(ParaName, SqlDbType.DateTime);

                case "VarChar":
                    return new SqlParameter(ParaName, SqlDbType.VarChar);
                case "Decimal":
                    return new SqlParameter(ParaName, SqlDbType.Decimal);
                default:
                    return new SqlParameter(ParaName, SqlDbType.VarChar);

            }
        }
        private OleDbParameter GetOledbPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Text":
                    return new OleDbParameter(ParaName, System.Data.DbType.String);
                case "Int":
                    return new OleDbParameter(ParaName, System.Data.DbType.Int32);
                case "DataTime":
                    return new OleDbParameter(ParaName, System.Data.DbType.DateTime);
                case "VarChar":
                    return new OleDbParameter(ParaName, System.Data.DbType.String);
                case "Decimal":
                    return new OleDbParameter(ParaName, System.Data.DbType.Decimal);
                default:
                    return new OleDbParameter(ParaName, System.Data.DbType.String);
            }


        }

        #endregion
        //创建连接（connection）和执行命令（command）
        private IDbConnection GetConnection()
        {
            //根据数据库类型创建连接
            switch (this.DbType)
            {
                case "SqlServer":
                    return new SqlConnection(this.ConnectionString);
                case "Access":
                    return new OleDbConnection(this.ConnectionString);
                default:
                    return new SqlConnection(this.ConnectionString);
            }
        }
        //command
        private IDbCommand GetCommand(string sql, IDbConnection iCon)
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new SqlCommand(sql, (SqlConnection)iCon);
                case "Access":
                    return new OleDbCommand(sql, (OleDbConnection)iCon);
                default:
                    return new SqlCommand(sql, (SqlConnection)iCon);
            }
        }
        private IDbCommand GetCommand()
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new SqlCommand();
                case "Access":
                    return new OleDbCommand();
                default:
                    return new SqlCommand();
            }
        }
        private IDataAdapter GetDataAdapter(string sql, IDbConnection iCon)
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new SqlDataAdapter(sql, (SqlConnection)iCon);
                case "Access":
                    return new OleDbDataAdapter(sql, (OleDbConnection)iCon);
                default:
                    return new SqlDataAdapter(sql, (SqlConnection)iCon);

            }
        }
        private IDbDataAdapter GetDataAdapter(IDbCommand iCmd)
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new SqlDataAdapter((SqlCommand)iCmd);
                case "Access":
                    return new OleDbDataAdapter((OleDbCommand)iCmd);
                default:
                    return new SqlDataAdapter((SqlCommand)iCmd);
            }
        }
        #region 执行简单的sql语句 
        /// <summary>
        /// 执行查询  增、删、改
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="iParam">传入的可变参数</param>
        /// <returns>返回受影响的记录</returns>
        public int ExecuteSql(string strSql, params IDataParameter[] iParam)
        {
            using (IDbConnection iCon = this.GetConnection())
            {
                using (IDbCommand iCmd = this.GetCommand(strSql, iCon))
                {
                    if (iParam != null)
                    {
                        foreach (IDataParameter item in iParam)
                        {
                            iCmd.Parameters.Add(item);
                        }
                    }
                    iCon.Open();
                    try
                    {
                        int rows = iCmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (Exception ex)
                    {
                        iCon.Close();
                        iCon.Dispose();
                        MessageBox.Show(ex.Message);
                        throw;
                    }
                }
            }
        }
        ///// <summary>
        ///// 执行一个带存储参数过程的sql语句
        ///// </summary>
        ///// <param name="SqlString">sql语句</param>
        ///// <param name="content">参数内容</param>
        ///// <returns>返回受影响的记录</returns>
        //public   int ExecuteSql(string strSql, string content, params IDataParameter[] iParam)
        //{
        //    using (IDbConnection iCon = this.GetConnection())
        //    {
        //        using (IDbCommand iCmd = this.GetCommand(strSql, iCon))
        //        {
        //            IDataParameter myParameter = this.Idbpara("@content", "Text");
        //            myParameter.Value = content;
        //            iCmd.Parameters.Add(myParameter);
        //            iCon.Open();
        //            try
        //            {
        //                int rows = iCmd.ExecuteNonQuery();
        //                return rows;
        //            }
        //            catch
        //            {
        //                iCon.Close();
        //                iCon.Dispose();
        //                throw;
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 执行查询，返回一行一列
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="iParam">传入的课变参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public object ExecuteScalar(string strSql, params IDataParameter[] iParam)
        {
            using (IDbConnection iCon = this.GetConnection())
            {
                using (IDbCommand iCmd = this.GetCommand(strSql, iCon))
                {
                    if (iParam != null)
                    {
                        foreach (IDataParameter item in iParam)
                        {
                            iCmd.Parameters.Add(item);
                        }
                    }
                    iCon.Open();
                    try
                    {
                        object obj = iCmd.ExecuteScalar();
                        if ((Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        return obj;
                    }
                    catch
                    {
                        iCon.Close();
                        iCon.Dispose();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询，返回多行多列
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="iParam">传入的可变参数</param>
        /// <returns>返回多行多列受影响的记录</returns>
        public IDataAdapter ExecuteReader(string strSql, params IDataParameter[] iParam)
        {
            using (IDbConnection iCon = this.GetConnection())
            {
                using (IDbCommand iCmd = this.GetCommand(strSql, iCon))
                {
                    if (iParam != null)
                    {
                        foreach (IDataParameter item in iParam)
                        {
                            iCmd.Parameters.Add(item);
                        }
                    }
                    iCon.Open();
                    try
                    {
                        IDataAdapter iAdapter = this.GetDataAdapter(strSql, iCon);
                        return iAdapter;
                    }
                    catch
                    {
                        iCon.Close();
                        iCon.Dispose();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>返回DataSet</returns>
        public DataSet Query(string strSql, params IDataParameter[] iParam)
        {
            using (IDbConnection iCon = this.GetConnection())
            {
                using (IDbCommand iCmd = this.GetCommand(strSql, iCon))
                {
                    if (iParam != null)
                    {
                        foreach (IDataParameter item in iParam)
                        {
                            iCmd.Parameters.Add(item);
                        }
                    }
                    DataSet ds = new DataSet();
                    iCon.Open();

                    try
                    {
                        IDataAdapter iAdapter = this.GetDataAdapter(strSql, iCon);
                        iAdapter.Fill(ds);
                        return ds;
                    }
                    catch
                    {
                        iCon.Close();
                        iCon.Dispose();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询，返回dataset
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="DataSet">要填充的dataset</param>
        /// <param name="tableName">要填充的表名</param>
        /// <returns></returns>
        public DataSet Query(string strSql, DataSet DataSet, string tableName, params IDataParameter[] iParam)
        {
            using (IDbConnection iCon = this.GetConnection())
            {
                using (IDbCommand iCmd = this.GetCommand(strSql, iCon))
                {

                    if (iParam != null)
                    {
                        foreach (IDataParameter item in iParam)
                        {
                            iCmd.Parameters.Add(item);
                        }
                    }
                    iCon.Open();
                    try
                    {
                        IDataAdapter iAdapter = this.GetDataAdapter(strSql, iCon);
                        ((OleDbDataAdapter)iAdapter).Fill(DataSet, tableName);
                        return DataSet;
                    }
                    catch
                    {
                        iCon.Close();
                        iCon.Dispose();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行sql语句，返回存储过程
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="DataSet">要填充的dataset</param>
        /// <param name="startIndex">开始记录</param>
        /// <param name="pageSise">页面记录大小</param>
        /// <param name="tableName">表名</param>
        /// <returns>返回dataset</returns>
        public DataSet Query(string strSql, DataSet DataSet, int startIndex, int pageSise, string tableName)
        {
            using (IDbConnection iCon = this.GetConnection())
            {
                iCon.Open();
                try
                {
                    IDataAdapter iAdapter = this.GetDataAdapter(strSql, iCon);
                    ((OleDbDataAdapter)iAdapter).Fill(DataSet, startIndex, pageSise, tableName);
                    return DataSet;
                }
                catch
                {
                    iCon.Open();
                    iCon.Dispose();
                    throw;
                }
            }
        }
        /// <summary>
        /// 执行查询语句,返回datatable
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>返回的datatable</returns>
        public DataTable ExecuteQuery(string strSql)
        {
            using (IDbConnection iCon = this.GetConnection())
            {
                using (IDbCommand iCmd = this.GetCommand(strSql, iCon))
                {
                    DataSet ds = new DataSet();
                    iCon.Open();
                    try
                    {
                        IDataAdapter iAdapter = this.GetDataAdapter(strSql, iCon);
                        iAdapter.Fill(ds);
                        return ds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        iCon.Close();
                        iCon.Dispose();
                        MessageBox.Show(ex.Message.ToString());
                        throw;
                    }
                }
            }
        }

        //封装返回DATATABLE
        public DataTable ExecuteDataTable(string strSql, params SqlParameter[] iParam)
        {
            using (IDbConnection iCon = this.GetConnection())
            {

                using (IDbCommand iCmd = this.GetCommand(strSql, iCon))
                {
                    if (iParam != null)
                    {
                        foreach (IDataParameter item in iParam)
                        {
                            iCmd.Parameters.Add(item);
                        }
                    }

                    try
                    {
                        DataSet ds = new DataSet();
                        IDataAdapter iAdapter = this.GetDataAdapter(strSql, iCon);
                        ((OleDbDataAdapter)iAdapter).Fill(ds);
                        return ds.Tables[0];
                    }
                    catch
                    {
                        iCon.Close();
                        iCon.Dispose();
                        throw;
                    }

                }
            }
        }
        #endregion
    }
}
