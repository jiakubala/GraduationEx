using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Stores
{
    /// <summary>
    /// 个人中心接口层
    /// </summary>
    public interface IUserStore
    {
        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> Userupdate(User user);

        /// <summary>
        /// 查询User实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> GetuserAsync<TResult>(Func<IQueryable<User>, IQueryable<TResult>> query);

        /// <summary>
        /// 查询Address
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(Func<IQueryable<Address>, IQueryable<TResult>> query);

        /// <summary>
        /// 查询Address列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<TResult>> GetAddressAsync<TResult>(Func<IQueryable<Address>, IQueryable<TResult>> query);

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        Task<Address> Addressupdate(Address add);

        /// <summary>
        /// 新增收货地址
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        Task<Address> Addressadd(Address add);

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        Task Addressdelete(Address add);
    }
}
