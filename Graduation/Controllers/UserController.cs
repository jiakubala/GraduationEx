using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /// 个人资料展示与修改
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Userupdate(User user)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 修改个人资料");
            try
            {
                var list = await _userManager.Userupdate(user);
                log.InfoFormat("修改个人资料成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                return View(list);
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
        public async Task<IActionResult> Userfavorite(int userid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 我的收藏");
            try
            {
                var list = await _userManager.Getfavorite(userid);
                log.InfoFormat("获取我的收藏成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("获取我的收藏失败,错误提示: " + Helper.JsonHelper.ToJson(e));
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
        public async Task<IActionResult> Updatepassword(User user)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 修改密码");
            try
            {
                var list = await _userManager.Userupdate(user);
                log.InfoFormat("修改密码成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                return View(list);
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
        public async Task<IActionResult> Address(int userid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 获取收货地址");
            try
            {
                var list = await _userManager.GetAddressesAsync(userid);
                log.InfoFormat("获取收货地址成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("获取收货地址失败,错误提示: " + Helper.JsonHelper.ToJson(e));
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
            log.InfoFormat(userName + " || Get into 编辑收货地址");
            try
            {
                var list = await _userManager.UpdateAddressesAsync(add);
                log.InfoFormat("编辑收货地址成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("编辑收货地址失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
    }
}