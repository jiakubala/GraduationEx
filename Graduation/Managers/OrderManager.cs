using Graduation.Models;
using Graduation.Stores;
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
        /// 获取订单评价
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetevaluateAsync(int userid)
        {
            try
            {
                return await _orderStore.GetOrderAsync(a => a.Where(b => b.UserId == userid && b.OrderState == 4 && b.Evaluate != null));
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
        public async Task<List<Order>> GetOrderbuysAsync(int userid)
        {
            try
            {
                return await _orderStore.GetOrderAsync(a => a.Where(b => b.UserId == userid && b.OrderState == 2));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
