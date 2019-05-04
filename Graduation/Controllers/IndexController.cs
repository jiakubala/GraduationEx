using Graduation.Dto.Request;
using Graduation.Dto.Response;
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
        /// <param name="code">1:特别推荐 2:新品上架 3:热门商品</param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Goodlist(int code)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 首页");
            try
            {
                //获取商品列表
                var goodlist = await _indexManager.GetGoodsAsync(code);
                //获取类型列表
                var typelist = await _indexManager.GetTypesAsync();
                var list = new IndexResponse()
                {
                    GoodList = goodlist,
                    TypeList = typelist
                };
                log.InfoFormat("获取商品列表成功" + (goodlist != null ? Helper.JsonHelper.ToJson(goodlist) : ""));
                log.InfoFormat("获取类型列表成功" + (typelist != null ? Helper.JsonHelper.ToJson(typelist) : ""));
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
        /// 商品列表（分类分支，搜索）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Goodlistbranch(GoodlistRequest condition)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 商品列表");
            try
            {
                var list = await _indexManager.GetGoodstypeAsync(condition);
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

        /// <summary>
        /// 分类商品列表排序
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Goodlistbranchinfo(GoodlistinfoRequest condition)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 分类商品列表排序");
            try
            {
                var list = await _indexManager.OrderGoodstypeAsync(condition);
                log.InfoFormat("获取分类商品列表排序成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("分类商品列表排序获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="typename"></param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Goodlistbranch(int goodid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 商品详情");
            try
            {
                var good = await _indexManager.GetAsync(goodid);
                log.InfoFormat("获取商品详情成功" + (good != null ? Helper.JsonHelper.ToJson(good) : ""));
                ViewData["UserName"] = userName;
                return View(good);
            }
            catch (Exception e)
            {
                log.Error("商品详情获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
    }
}
