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
    }
}
