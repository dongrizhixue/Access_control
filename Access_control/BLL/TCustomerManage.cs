using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TCustomerManage
    {
        public static string UserLogin(Entity.TCustomer user)
        {
            string where = "LoginName = @LoginName and LoginPwd = @LoginPwd";
            System.Data.SqlClient.SqlParameter[] para = {
                new System.Data.SqlClient.SqlParameter("@LoginName",user.LoginName),
                new System.Data.SqlClient.SqlParameter("@LoginPwd",user.LoginPwd) //暂时不加密了
                };
            Entity.TCustomer entity = DAL.TCustomerService.GetTCustomerByWhere(where, para);
            if (entity == null)
            {
                return "0";
            }
            else
            {
                System.Web.HttpContext.Current.Session["CurrentUser"] = entity;
                return "1";
            }
        }
    }
}
