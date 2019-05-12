using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduation.Dto.Request;
using Graduation.Dto.Response;
using Graduation.Filter;
using Graduation.Managers;
using Graduation.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    /// <summary>
    /// 订单服务层
    /// </summary>
    public class OrderController : Controller
    {
        private readonly OrderManager _orderManager;
        private readonly IndexManager _indexManager;
        private readonly UserManager _userManager;
        private ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(LoginController));

        public OrderController(OrderManager orderManager, IndexManager indexManager, UserManager userManager)
        {
            _orderManager = orderManager;
            _indexManager = indexManager;
            _userManager = userManager;
        }

        /// <summary>
        /// 我的订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Orderlist(int userid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 我的订单");
            try
            {
                var list = await _orderManager.GetOrdersAsync(userid);
                log.InfoFormat("获取订单列表成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("订单列表获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 订单评价
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Evaluatelist()
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 订单评价");
            try
            {
                var list = await _orderManager.GetevaluateAsync(userId);
                log.InfoFormat("获取订单评价成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View("../User/Evaluatelist", list);
            }
            catch (Exception e)
            {
                log.Error("订单评价获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 添加评价
        /// </summary>
        /// <param name="eva"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Addevaluate(EvaluateRequest eva)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 添加评价");
            try
            {
                var list = await _orderManager.AddevaluateAsync(eva);
                log.InfoFormat("添加评价成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return RedirectToAction("Orderbuylist");
            }
            catch (Exception e)
            {
                log.Error("添加评价失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 删除订单评价
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Deleteevaluate(string orderid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 删除评价");
            try
            {
                var order = await _orderManager.DeleteevaluateAsync(orderid);
                log.InfoFormat("删除评价成功" + (order != null ? Helper.JsonHelper.ToJson(order) : ""));
                ViewData["UserName"] = userName;
                return RedirectToAction("Evaluatelist");
            }
            catch (Exception e)
            {
                log.Error("删除评价失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 我的已购商品（个人主页）
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Orderbuylist()
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 我的已购商品");
            try
            {
                var list = await _orderManager.GetOrderbuysAsync(userId);
                log.InfoFormat("获取已购商品成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View("../User/Orderbuylist", list);
            }
            catch (Exception e)
            {
                log.Error("已购商品获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 购物车页面
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Shopcarlist()
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 我的购物车");
            try
            {
                var list = await _orderManager.GetShopcarAsync(userId);
                log.InfoFormat("获取我的购物车列表成功" + (list != null ? Helper.JsonHelper.ToJson(list) : ""));
                ViewData["UserName"] = userName;
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("我的购物车列表获取失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 购物车页面flow2
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Shopflow(ShopflowRequest order)
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 获取收货地址");
            try
            {
                var adds = await _userManager.GetAddressesAsync(userId);
                var list = new ShopflowResponse
                {
                    Adds = adds,
                    Orderidlist = order.Orderidlist
                };
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
        /// 删除购物车商品
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> DeleteShopcar(string orderid)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 删除购物车商品");
            try
            {
                await _orderManager.DeleteOrderAsync(orderid);
                log.InfoFormat("删除购物车商品成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Shopcarlist");
            }
            catch (Exception e)
            {
                log.Error("删除购物车商品失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> AddShopcar(GoodRequest order)
        {
            string userName = HttpContext.Session.GetString("UserName");
            int? userId = HttpContext.Session.GetInt32("UserId");
            log.InfoFormat(userName + " || Get into 添加购物车商品");
            try
            {
                order.UserId = userId;
                var or = await _orderManager.AddOrderAsync(order);
                var good = await _indexManager.GetAsync(order.GoodId);
                log.InfoFormat("获取商品详情成功" + (good != null ? Helper.JsonHelper.ToJson(good) : ""));
                log.InfoFormat("添加购物车商品成功" + (or != null ? Helper.JsonHelper.ToJson(or) : ""));
                ViewData["UserName"] = userName;
                return View("../Index/Goodlistbranchdetails", good);
            }
            catch (Exception e)
            {
                log.Error("添加购物车商品失败,错误提示: " + Helper.JsonHelper.ToJson(e));
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
                return RedirectToAction("Orderbuylist");
            }
            catch (Exception e)
            {
                log.Error("修改订单状态失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 支付页面
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Payment(ShopflowRequest olist)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 转到支付页面");
            try
            {
                var num = await _orderManager.Zongjia(olist.Orderidlist);
                log.InfoFormat("转到支付页面成功");
                ViewData["UserName"] = userName;
                ViewData["Num"] = num;
                var list = new ShopflowResponse
                {
                    Orderidlist = olist.Orderidlist
                };
                return View(list);
            }
            catch (Exception e)
            {
                log.Error("转到支付页面失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

        /// <summary>
        /// 完成支付
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(SessionFilter))]
        public async Task<IActionResult> Paymentinfo(ShopflowRequest olist)
        {
            string userName = HttpContext.Session.GetString("UserName");
            log.InfoFormat(userName + " || Get into 支付完成");
            try
            {
                StateRequest state = new StateRequest
                {
                    OrderId = olist.Orderidlist,
                    OrderState = 2
                };
                await _orderManager.UpdatestateAsync(state);
                log.InfoFormat("支付成功");
                ViewData["UserName"] = userName;
                return RedirectToAction("Shopcarlist");
            }
            catch (Exception e)
            {
                log.Error("支付失败,错误提示: " + Helper.JsonHelper.ToJson(e));
                return View("Error", e);
            }
        }

    }
}