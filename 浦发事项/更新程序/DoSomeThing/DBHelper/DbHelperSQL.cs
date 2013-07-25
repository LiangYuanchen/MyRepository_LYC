using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;

namespace DoSomeThing.DBHelper
{
    /// <summary>
    /// 数据访问抽象基础类
    /// Copyright (C) 2004-2008 By LiTianPing 
    /// </summary>
    public static class DbHelperSQL
    { 
        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch
                    {
                        return -1;
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }
      

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlTransaction tx = null;

                try
                {
                    connection.Open();
                    cmd.Connection = connection;
                    tx = connection.BeginTransaction();
                    cmd.Transaction = tx;
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        cmd.CommandText = SQLStringList[n];
                        count += cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                    return count;
                }
                catch
                {
                    if (tx != null)
                    {
                        tx.Rollback();
                    }
                    return -1;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

      
        /// <summary>
        /// 返回数据行数
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        public static int GetCount(string SQLString, string connectionString)
        {
            object o = GetSingle(SQLString, connectionString);
            if (null == o || string.IsNullOrEmpty(o.ToString()))
            {
                return -1;
            }
            return int.Parse(o.ToString());
        }
       

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

       
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch
            {
                return null;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }


        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string SQLString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataTable GetDataTable(string SQLString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                    return ds.Tables[0];
                }
                catch
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion
    }
}
