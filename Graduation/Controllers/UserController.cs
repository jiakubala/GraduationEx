using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduation.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    /// <summary>
    /// 个人中心控制层
    /// </summary>
    public class UserController : Controller
    {
        private readonly UserManager _userManager;
        private ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(LoginController));

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}