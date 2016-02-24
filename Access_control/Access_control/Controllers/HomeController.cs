using System.Web.Mvc;

namespace Access_control.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录请求
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>登录结果，0表示登录失败，1表示登录成功</returns>
        public JsonResult UserLogin(Entity.TCustomer user)
        {
            string result = BLL.TCustomerManage.UserLogin(user);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}