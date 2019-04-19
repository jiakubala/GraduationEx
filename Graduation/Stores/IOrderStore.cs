using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Stores
{
    /// <summary>
    /// 订单接口层
    /// </summary>
    public interface IOrderStore
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<TResult>> GetOrderAsync<TResult>(Func<IQueryable<Order>, IQueryable<TResult>> query);

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(Func<IQueryable<Order>, IQueryable<TResult>> query);

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task Orderdelete(Order order);

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<Order> AddOrder(Order order);

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<Order> UpdateOrder(Order order);
    }
}
