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

        /// <summary>
        /// 个人资料展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Usermessage()
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 获取个人资料");
            try
            {
                var list = await _userManager.Userqurey(userId);
                log.InfoFormat("获取个人资料成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View("Usermessage", list);
            }
            catch (Exception e)
            {
                log.Error("获取个人资料失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        
        /// <summary>
        /// 个人资料修改
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Userupdate(User user)
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 修改个人资料");
            try
            {
                user.UserId = userId;
                await _userManager.Userupdate(user);
                log.InfoFormat("修改个人资料成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Usermessage");
            }
            catch (Exception e)
            {
                log.Error("修改个人资料失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Userfavorite()
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 我的收藏");
            try
            {
                var list = await _userManager.Getfavorite(userId);
                log.InfoFormat("获取我的收藏成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("获取我的收藏失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 添加/删除收藏
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Userfavorite(FavoriteRequest fr)
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 编辑收藏");
            try
            {
                fr.UserId = userId;
                var good = await _userManager.Updatefavorite(fr);
                log.InfoFormat("编辑收藏成功" + (good != null ? Helper.JsonHelper.ToJson(good) : ""));
                ViewData["UserName"] = userName;
                return View("../Index/Goodlistbranchdetails", good);
            }
            catch (Exception e)
            {
                log.Error("编辑收藏失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 修改密码界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Passwordupdate()
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 进入修改密码");
            try
            {
                log.InfoFormat("进入修改密码成功");
                ViewData["UserName"] = userName;
                return View("Passwordupdate");
            }
            catch (Exception e)
            {
                log.Error("进入修改密码失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Updatepassword(PasswordRequest pas)
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 修改密码");
            try
            {
                pas.UserId = userId;
                var t = await _userManager.Psupdate(pas);
                if (t)
                {
                    ViewData["Bool"] = 1;
                }
                log.InfoFormat("修改密码成功");
                ViewData["UserName"] = userName;
                return View();
            }
            catch (Exception e)
            {
                log.Error("修改密码失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 收货地址
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Address()
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 获取收货地址");
            try
            {
                var list = await _userManager.GetAddressesAsync(userId);
                log.InfoFormat("获取收货地址成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("获取收货地址失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 跳转编辑收货地址页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> UpdateAddress(int keyid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 跳转编辑收货地址页面");
            try
            {
                var model = await _userManager.GetAddAsync(keyid);
                ViewData["UserName"] = userName;
                return View(model);
            }
            catch (Exception e)
            {
                log.Error("跳转编辑收货地址页面,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 编辑收货地址
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> UpdateAddress(Address add)
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 编辑收货地址");
            try
            {
                add.UserId = userId;
                var list = await _userManager.UpdateAddressesAsync(add);
                log.InfoFormat("编辑收货地址成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View("UpdateAddress", list);
            }
            catch (Exception e)
            {
                log.Error("编辑收货地址失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 新增收货地址
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> AddAddress(Address add)
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 新增收货地址");
            try
            {
                add.UserId = userId;
                var list = await _userManager.AddAddressesAsync(add);
                log.InfoFormat("新增收货地址成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return RedirectToAction("Address",add.UserId);
            }
            catch (Exception e)
            {
                log.Error("新增收货地址失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="keyid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> DeleteAddress(int keyid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 删除收货地址");
            try
            {
                await _userManager.DeleteAddressesAsync(keyid);
                log.InfoFormat("删除收货地址成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Address");
            }
            catch (Exception e)
            {
                log.Error("删除收货地址失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
    }
}