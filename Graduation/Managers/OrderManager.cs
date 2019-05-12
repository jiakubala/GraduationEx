using Graduation.Dto.Request;
using Graduation.Models;
using Graduation.Stores;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Managers
{
    /// <summary>
    /// 订单逻辑层
    /// </summary>
    public class OrderManager
    {
        protected IOrderStore _orderStore { get; }
        public OrderManager(IOrderStore orderStore)
        {
            _orderStore = orderStore;
        }

        /// <summary>
        /// 获取所有订单
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetAllorder()
        {
            try
            {
                return await _orderStore.GetOrderAsync(a => a.Where(b => b.OrderId != null));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取用户订单列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetOrdersAsync(int userid)
        {
            try
            {
                return await _orderStore.GetOrderAsync(a => a.Where(b => b.UserId == userid));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public async Task<Order> GetAsync(string orderid)
        {
            try
            {
                return await _orderStore.GetAsync(a => a.Where(b => b.OrderId == orderid));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取订单评价
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetevaluateAsync(int? userid)
        {
            try
            {
                if (userid != null && userid != 0)
                {
                    return await _orderStore.GetOrderAsync(a => a.Where(b => b.UserId == userid && b.OrderState == 4 && b.Evaluate != null));
                }
                return await _orderStore.GetOrderAsync(a => a.Where(b => b.OrderState == 4 && b.Evaluate != null));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取用户已购商品列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetOrderbuysAsync(int? userid)
        {
            try
            {
                return await _orderStore.GetOrderAsync(a => a.Where(b => b.UserId == userid && b.OrderState != 1));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 购物车列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetShopcarAsync(int? userid)
        {
            try
            {
                return await _orderStore.GetOrderAsync(a => a.Where(b => b.UserId == userid && b.OrderState == 1));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除订单（删除购物车里商品）
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public async Task DeleteOrderAsync(string orderid)
        {
            try
            {
                var order = await _orderStore.GetAsync(a => a.Where(b => b.OrderId == orderid));
                await _orderStore.Orderdelete(order);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 新增订单（添加商品到购物车）
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public async Task<Order> AddOrderAsync(GoodRequest good)
        {
            try
            {
                return await _orderStore.AddOrder(new Order
                {
                    Price = good.Price,
                    UserId = good.UserId,
                    OrderId = Guid.NewGuid().ToString(),
                    GoodId = good.GoodId,
                    GoodNumber = good.GoodNumber,
                    Name = good.Name,
                    OrderState = 1
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task UpdatestateAsync(StateRequest state)
        {
            try
            {
                foreach (var orderid in state.OrderId)
                {
                    var order = await _orderStore.GetAsync(a => a.Where(b => b.OrderId == orderid));
                    order.OrderState = state.OrderState;
                    await _orderStore.UpdateOrder(order);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 添加评价
        /// </summary>
        /// <param name="eva"></param>
        /// <returns></returns>
        public async Task<Order> AddevaluateAsync(EvaluateRequest eva)
        {
            try
            {
                var order = await _orderStore.GetAsync(a => a.Where(b => b.OrderId == eva.Orderid));
                order.Evaluate = eva.Evaluate;
                return await _orderStore.UpdateOrder(order);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除订单评价
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public async Task<Order> DeleteevaluateAsync(string orderid)
        {
            try
            {
                var order = await _orderStore.GetAsync(a => a.Where(b => b.OrderId == orderid));
                order.Evaluate = null;
                return await _orderStore.UpdateOrder(order);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 支付页面商品总价
        /// </summary>
        /// <param name="orderidlist"></param>
        /// <returns></returns>
        public async Task<decimal> Zongjia(List<string> orderidlist)
        {
            try
            {
                decimal num = 0;
                foreach (var i in orderidlist)
                {
                   var order = await _orderStore.GetAsync(a => a.Where(b => b.OrderId == i));
                    num = num + order.Price * order.GoodNumber;
                }
                return num;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
