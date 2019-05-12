using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduation.Dto.Request;
using Graduation.Filter;
using Graduation.Managers;
using Graduation.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    public class AdminController : Controller
    {
        private ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(IndexController));
        private readonly LoginManager _loginManager;
        private readonly OrderManager _orderManager;
        private readonly IndexManager _indexManager;
        private readonly UserManager _userManager;

        public AdminController(LoginManager loginManager, OrderManager orderManager, IndexManager indexManager,
            UserManager userManager)
        {
            _loginManager = loginManager;
            _orderManager = orderManager;
            _indexManager = indexManager;
            _userManager = userManager;
        }

        #region 管理员登录
        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Adminlogin()
        {
            log.InfoFormat(" || Get into 管理员登陆页面");
            try
            {
                return View();
            }
            catch (Exception e)
            {
                log.Error("管理员登陆页面跳转失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Adminloginjudge(Admin admin)
        {
            log.InfoFormat(" || Get into 管理员登陆验证");
            if (!ModelState.IsValid)
            {
                log.Warn("管理员登陆验证模型验证失败");
            }
            try
            {
                var a = await _loginManager.GetAdminAsync(admin.Password, admin.Id);
                if (a == null)
                {
                    ModelState.AddModelError(nameof(admin.Password), "用户名或密码错误");
                    return RedirectToAction("Adminlogin");
                }
                //创建session
                HttpContext.Session.SetString("UserName", a.Name);
                ViewData["UserName"] = a.Name;
                log.InfoFormat("管理员登陆验证成功" + (a != null ? Helper.JsonHelper.ToJson(a) : ""));
                return RedirectToAction("AdminIndex");
            }
            catch (Exception e)
            {
                log.Error("登陆验证失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 登录注销
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public IActionResult Loginout()
        {
            string adminName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(adminName + " || Get into 管理员登录注销");
            try
            {
                HttpContext.Session.Remove("UserName");
                log.InfoFormat("管理员登录注销成功");
                return RedirectToAction("Adminlogin");
            }
            catch (Exception e)
            {
                log.Error("管理员注销登陆失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
        #endregion

        #region 首页
        /// <summary>
        /// 管理员首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public IActionResult AdminIndex()
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(" || Get into 管理员首页");
            try
            {
                ViewData["UserName"] = userName;
                return View();
            }
            catch (Exception e)
            {
                log.Error("管理员首页跳转失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
        #endregion
        
        #region 订单管理
        /// <summary>
        /// 获取所有订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Allorderlist()
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 管理端订单管理");
            try
            {
                var list = await _orderManager.GetAllorder();
                log.InfoFormat("获取管理端订单管理列表成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("管理端订单管理列表获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> UpadateOrderState(StateRequest state)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 修改订单状态");
            try
            {
                await _orderManager.UpdatestateAsync(state);
                log.InfoFormat("修改订单状态成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Allorderlist");
            }
            catch (Exception e)
            {
                log.Error("修改订单状态失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Deleteorder(string orderid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 删除订单");
            try
            {
                await _orderManager.DeleteOrderAsync(orderid);
                log.InfoFormat("删除订单成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Allorderlist");
            }
            catch (Exception e)
            {
                log.Error("删除订单失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
        #endregion

        #region 商品管理
        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Allgoodlist()
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 首页");
            try
            {
                //获取商品列表
                var goodlist = await _indexManager.GetGoodsAsync();
                log.InfoFormat("获取商品列表成功" + (goodlist != null ? Helper.JsonHelper.ToJson(goodlist) : ""));
                ViewData["UserName"] = userName;
                return View(goodlist);
            }
            catch (Exception e)
            {
                log.Error("首页跳转失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 上传商品页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public IActionResult Addgoodview()
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 上传商品页面");
            try
            {
                log.InfoFormat("跳转上传商品页面成功");
                ViewData["UserName"] = userName;
                return View();
            }
            catch (Exception e)
            {
                log.Error("跳转上传商品页面失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 上传商品
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Addgood(Good good)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 上传商品");
            try
            {
                await _indexManager.AddGoodAsync(good);
                log.InfoFormat("上传商品成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Allgoodlist");
            }
            catch (Exception e)
            {
                log.Error("上传商品失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Deletegood(int goodid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 删除商品");
            try
            {
                await _indexManager.DeleteGoodAsync(goodid);
                log.InfoFormat("删除商品成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Allgoodlist");
            }
            catch (Exception e)
            {
                log.Error("删除商品失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        #endregion

        #region 用户管理
        /// <summary>
        /// 用户管理首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Alluserlist()
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 用户管理");
            try
            {
                //获取商品列表
                var userlist = await _userManager.Userlistqurey();
                log.InfoFormat("获取用户列表成功" + (userlist != null ? Helper.JsonHelper.ToJson(userlist) : ""));
                ViewData["UserName"] = userName;
                return View(userlist);
            }
            catch (Exception e)
            {
                log.Error("获取用户列表失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 封禁用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Deleteuser(int userid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(" || Get into 封禁用户");
            try
            {
                await _userManager.DeleteuserAsync(userid);
                log.InfoFormat("封禁用户成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Alluserlist");
            }
            catch (Exception e)
            {
                log.Error("封禁用户失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
        #endregion

        #region 评价管理
        /// <summary>
        /// 评价管理列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Allevaluate(int userId)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 评价列表");
            try
            {
                var list = await _orderManager.GetevaluateAsync(userId);
                log.InfoFormat("获取评价列表成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("评价列表获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 删除评价
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Deleteeva(string orderid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 删除评价");
            try
            {
                var order = await _orderManager.DeleteevaluateAsync(orderid);
                log.InfoFormat("删除评价成功" + (order != null ? Helper.JsonHelper.ToJson(order) : ""));
                ViewData["UserName"] = userName;
                return RedirectToAction("Allevaluate");
            }
            catch (Exception e)
            {
                log.Error("删除评价失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
        #endregion
    }
}