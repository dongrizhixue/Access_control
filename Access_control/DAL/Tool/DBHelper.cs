using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DAL.Tool
{
    public class DBHelper
    {
        /// <summary>
        /// 获取配置文件中设置的数据库连接字符串
        /// </summary>
        private static string connectionString = System.Configuration.ConfigurationManager.AppSettings["Connection"];

        /// <summary>
        /// 执行sql语句，并返回受影响行数
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="commandText">执行的sql语句</param>
        /// <param name="commandParameters">执行的参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public static void ExecuteStoredProcedure(string commandText, ref SqlParameter[] commandParameters)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();   //打开数据库链接
                SqlCommand sqlComm = new SqlCommand(commandText, sqlCon);
                sqlComm.CommandType = CommandType.StoredProcedure;
                foreach (var item in commandParameters)
                {
                    sqlComm.Parameters.Add(item);
                }
                sqlComm.ExecuteNonQuery();

                foreach (var item in commandParameters)
                {
                    if (item.Direction == ParameterDirection.Output)
                    {
                        item.Value = sqlComm.Parameters[item.ParameterName].Value;
                        item.SqlDbType = sqlComm.Parameters[item.ParameterName].SqlDbType;
                    }
                }
            }
        }

        public static int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteNonQuery(connection, commandType, commandText);
            }
        }

        /// <summary>
        /// 获取查询结果中的第一行第一列的值
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="commandText">需要执行的sql语句</param>
        /// <param name="commandParameters">执行的参数</param>
        /// <returns>查询结果中的第一行第一列的值</returns>
        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                return SqlHelper.ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// 根据查询语句获取DataSet,不带参数
        /// </summary>
        /// <param name="commandType">执行的类型</param>
        /// <param name="commandText">执行的SQL语句</param>
        /// <returns>根据查询语句得到的DataSet</returns>
        public static DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteDataset(connection, commandType, commandText);
            }
        }

        /// <summary>
        /// 根据查询语句获取DataSet
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="commandText">执行语句</param>
        /// <param name="commandParameters">执行的函数</param>
        /// <returns>查询出来的DataSet</returns>
        public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteDataset(connectionString, commandType, commandText, commandParameters);
            }
        }



        /// <summary>
        /// 根据查询语句获取DataTable
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="commandText">执行语句</param>
        /// <returns>查询出来的DataTable</returns>
        public static DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            DataSet ds = ExecuteDataset(commandType, commandText);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 执行sql语句之后返回SqlDataReader对象
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="commandText">sql语句类型</param>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return SqlHelper.ExecuteReader(connection, commandType, commandText);
        }

        /// <summary>
        /// 根据查询语句获取DataTable
        /// </summary>
        /// <param name="commandType">执行类型</param>
        /// <param name="commandText">执行语句</param>
        /// <param name="commandParameters">执行的函数</param>
        /// <returns>查询出来的DataTable</returns>
        public static DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataSet ds = ExecuteDataset(commandType, commandText, commandParameters);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 数据库读取行的只进流
        /// </summary>
        /// <param name="commandType">类型</param>
        /// <param name="commandText">名称</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return SqlHelper.ExecuteReader(connectionString, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// 把DataTable转换为实体集
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dt">带有数据的DataTable</param>
        /// <returns>返回实体集</returns>
        public static IList<T> FillModel<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            T model = default(T);

            foreach (DataRow aRow in dt.Rows)
            {
                model = Activator.CreateInstance<T>();

                foreach (DataColumn dc in aRow.Table.Columns)
                {
                    PropertyInfo pi = model.GetType().GetProperty(dc.ColumnName);
                    if (aRow[dc.ColumnName] != DBNull.Value && aRow[dc.ColumnName].ToString() != "")
                    {
                        pi.SetValue(model, aRow[dc.ColumnName], null);
                    }
                    else
                    {
                        pi.SetValue(model, null, null);
                    }
                }

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 批量执行SQL语句
        /// </summary>
        /// <param name="SQLStringList">sql语句的集合</param>
        /// <returns>执行成功的数量</returns>
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
        }

        private bool ColumnEqual(object objectA, object objectB)
        {
            if (objectA == DBNull.Value && objectB == DBNull.Value)
            {
                return true;
            }
            if (objectA == DBNull.Value || objectB == DBNull.Value)
            {
                return false;
            }
            return (objectA.Equals(objectB));
        }

        /// 按照fieldName从sourceTable中选择出不重复的行， 
        /// 相当于select distinct fieldName from sourceTable 
        /// </summary> 
        /// <param name="tableName">表名</param> 
        /// <param name="sourceTable">源DataTable</param> 
        /// <param name="fieldName">列名</param> 
        /// <returns>一个新的不含重复行的DataTable，列只包括fieldName指明的列</returns> 
        public DataTable SelectDistinct(string tableName, DataTable sourceTable, string fieldName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add(fieldName, sourceTable.Columns[fieldName].DataType);
            object lastValue = null;
            foreach (DataRow dr in sourceTable.Select("", fieldName))
            {
                if (lastValue == null || !(ColumnEqual(lastValue, dr[fieldName])))
                {
                    lastValue = dr[fieldName];
                    dt.Rows.Add(new object[] { lastValue });
                }
            }

            return dt;
        }

        /// <summary>
        /// 将DataTable插入数据库中
        /// 请注意参数sourceColums和参数targetColums一定要的列名一定要一一对应
        /// </summary>
        /// <param name="dt">shujyuan</param>
        /// <param name="TabelName">被插入的数据库中的表的名称</param>
        /// <param name="sourceColums">数据源中源列的名称</param>
        /// <param name="destinationColums">目标表中目标列的名称</param>
        public static void InsertTable(DataTable dt, string TabelName, List<string> sourceColums, List<string> destinationColums)
        {
            //声明数据库连接  
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //声明SqlBulkCopy ,using释放非托管资源  
            using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
            {
                //设置要批量写入的表  
                sqlBC.DestinationTableName = TabelName;
                //自定义的datatable和数据库的字段进行对应  
                for (int i = 0; i < sourceColums.Count; i++)
                {
                    sqlBC.ColumnMappings.Add(sourceColums[i], destinationColums[i]);
                }
                //批量写入  
                sqlBC.WriteToServer(dt);
            }
            conn.Dispose();
        }
    }
}
