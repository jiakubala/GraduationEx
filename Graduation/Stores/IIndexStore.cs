using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Stores
{
    /// <summary>
    /// 首页接口层
    /// </summary>
    public interface IIndexStore
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<TResult>> GetGoodAsync<TResult>(Func<IQueryable<Good>, IQueryable<TResult>> query);

        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(Func<IQueryable<Good>, IQueryable<TResult>> query);

        /// <summary>
        /// 修改商品实体
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        Task<Good> UpdateGood(Good good);
    }
}
