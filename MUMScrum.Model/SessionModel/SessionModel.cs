using MUMScrum.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MUMScrum.Model.Model;
namespace MUMScrum.Model.SessionModel
{
    public class SessionModel
    {
        public static LoginUser LoginUser
        {
            get
            {
                var user = HttpContext.Current.Session["LoginUser"] as LoginUser;
                return user ?? new LoginUser();
            }
            set
            {
                HttpContext.Current.Session["LoginUser"] = value;
            }
        }
    }
}
