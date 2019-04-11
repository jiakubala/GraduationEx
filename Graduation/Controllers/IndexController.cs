using Graduation.Filter;
using Graduation.Managers;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Controllers
{
    /// <summary>
    /// 首页服务层
    /// </summary>
    public class IndexController : Controller
    {
        private readonly IndexManager _indexManager;
        private ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(IndexController));

        public IndexController(IndexManager indexManager)
        {
            _indexManager = indexManager;
        }

        /// <summary>
        /// 商品列表（首页）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public IActionResult Goodlist ()
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 商品列表");
            return View();
        }

    }
}
