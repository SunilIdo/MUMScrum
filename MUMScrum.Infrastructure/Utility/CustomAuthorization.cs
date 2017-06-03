using MUMScrum.Model.SessionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MUMScrum.Model.ENUMS;
using MUMScrum.Model.Model;

namespace MUMScrum.Infrastructure.Utility
{
    public class CustomAuthorization
    {
        public class AuthorizeUserAttribute : AuthorizeAttribute
        {
            // Custom property
            public RoleEnum Role { get; set; }
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                if (SessionModel.LoginUser.RoleId !=null)
                {
                    var currentUser = SessionModel.LoginUser;
                    return (currentUser.RoleId == Role) ;
                }
                return false;
            }
        }
    }
}
