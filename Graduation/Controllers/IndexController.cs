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
        public async Task<IActionResult> Goodlist()
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 首页");
            try
            {
                var list = await _indexManager.GetGoodsAsync();
                log.InfoFormat("获取商品列表成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("首页跳转失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 商品列表（分类分支）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Goodlistbranch(string typename)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 商品列表");
            try
            {
                var list = await _indexManager.GetGoodstypeAsync(typename);
                log.InfoFormat("获取分类商品列表成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("商品列表获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

    }
}
