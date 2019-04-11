using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Stores
{
    /// <summary>
    /// 登录接口层
    /// </summary>
    public interface ILoginStore
    {
        /// <summary>
        /// 查询user实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(Func<IQueryable<User>, IQueryable<TResult>> query);

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> AddAsync(User user);
    }
}
