using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Filter
{
    /// <summary>
    /// 验证登录过滤器
    /// </summary>
    public class SessionFilter : ActionFilterAttribute
    {
        private ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(SessionFilter));

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userName = context.HttpContext.Session.GetString("UserName");
            if (userName == null)
            {
                context.Result = new RedirectResult("/Login/Loginin");
            }
            base.OnActionExecuting(context);

        }
    }
}
