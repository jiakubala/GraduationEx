using Graduation.Filter;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Graduation.Managers;
using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Controllers
{
    /// <summary>
    /// 登陆服务层
    /// </summary>
    public class LoginController : Controller
    {
        private readonly LoginManager _loginManager;
        private ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(LoginController));

        public LoginController(LoginManager loginManager)
        {
            _loginManager = loginManager;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Loginin()
        {
            log.InfoFormat(" || Get into 登陆页面");
            try
            {
                return View();
            }
            catch (Exception e)
            {
                log.Error("登陆页面跳转失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Loginjudge(User user)
        {
            log.InfoFormat(" || Get into 登陆验证");
            if (!ModelState.IsValid)
            {
                log.Warn("登陆验证模型验证失败");
            }
            try
            {
                var a = await _loginManager.GetUserAsync(user.Password, user.UserId);
                if (a == null)
                {
                    ModelState.AddModelError(nameof(user.Password), "用户名或密码错误");
                    return RedirectToAction("Loginin");
                }
                var b = Convert.ToInt32(a.UserId);
                //创建session
                HttpContext.Session.SetString("UserName", a.Name);
                HttpContext.Session.SetInt32("UserId", b);
                ViewData["UserName"] = a.Name;
                log.InfoFormat("登陆验证成功" + (a != null ? Helper.JsonHelper.ToJson(a) : ""));
                return RedirectToAction("Goodlist", "Index");
            }
            catch (Exception e)
            {
                log.Error("登陆验证失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }


        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            log.InfoFormat(" || Get into 注册页面");
            try
            {
                return View();
            }
            catch (Exception e)
            {
                log.Error("注册页面跳转失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            log.InfoFormat(" || Get into 注册");
            if (!ModelState.IsValid)
            {
                log.Warn("注册验证模型验证失败");
            }
            try
            {
                var u = await _loginManager.AddAsync(user);
                log.InfoFormat("注册成功" + (u != null ? Helper.JsonHelper.ToJson(u) : ""));
                ViewData["Message"] = "注册成功";
                return RedirectToAction("Loginin");
            }
            catch (Exception e)
            {
                log.Error("注册失败,错误提示: " + Helper.JsonHelper.ToJson(e));
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
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 登录注销");
            try
            {
                HttpContext.Session.Remove("UserName");
                log.InfoFormat("登录注销成功");
                return RedirectToAction("Loginin");
            }
            catch (Exception e)
            {
                log.Error("注销登陆失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }
    }
}
