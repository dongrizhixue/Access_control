using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using DAL.Tool;

namespace DAL
{
    public class TCustomerService
    {
        public static TCustomer GetTCustomerByWhere(string where, SqlParameter[] para)
        {
            List<TCustomer> list = GetTCustomerByCondition(where, para);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }
        }

        private static List<TCustomer> GetTCustomerByCondition(string where, SqlParameter[] values)
        {
            List<TCustomer> list = new List<TCustomer>();
            string sql = "SELECT  ROW_NUMBER() over (order by CustomerID) abcd ,* FROM  TCustomer WHERE  " + where;
            try
            {
                DataSet ds = DBHelper.ExecuteDataset(CommandType.Text, sql, values);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                DataTable table = ds.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    TCustomer TCustomer = new TCustomer();

                    TCustomer.CustomerID = row["CustomerID"] == DBNull.Value ? null : (int?)row["CustomerID"];
                    TCustomer.LoginName = row["LoginName"] == DBNull.Value ? null : (string)row["LoginName"];
                    TCustomer.LoginPwd = row["LoginPwd"] == DBNull.Value ? null : (string)row["LoginPwd"];
                    TCustomer.CustomerName = row["CustomerName"] == DBNull.Value ? null : (string)row["CustomerName"];
                    TCustomer.Mobile = row["Mobile"] == DBNull.Value ? null : (string)row["Mobile"];
                    TCustomer.Product = row["Product"] == DBNull.Value ? null : (string)row["Product"];
                    TCustomer.Version = row["Version"] == DBNull.Value ? null : (string)row["Version"];
                    TCustomer.Price = row["Price"] == DBNull.Value ? null : (decimal?)row["Price"];
                    TCustomer.Term = row["Term"] == DBNull.Value ? null : (int?)row["Term"];
                    TCustomer.OpenTime = row["OpenTime"] == DBNull.Value ? null : (DateTime?)row["OpenTime"];
                    TCustomer.LastTime = row["LastTime"] == DBNull.Value ? null : (DateTime?)row["LastTime"];
                    TCustomer.RemitTime = row["RemitTime"] == DBNull.Value ? null : (DateTime?)row["RemitTime"];

                    TCustomer.ABCD = row["ABCD"] == DBNull.Value ? null : (long?)row["ABCD"];

                    list.Add(TCustomer);
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
