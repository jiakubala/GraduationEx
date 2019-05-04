using Graduation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Stores
{
    /// <summary>
    /// 个人中心数据层
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class UserStore<TContext> : IUserStore where TContext : UnifiedDbContext
    {
        public UserStore(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        protected virtual TContext _context { get; }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> Userupdate(User user)
        {
            try
            {
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return await _context.User.Where(a => a.UserId == user.UserId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取User实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TResult> GetuserAsync<TResult>(Func<IQueryable<User>, IQueryable<TResult>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            try
            {
                return await query.Invoke(_context.User.AsNoTracking()).SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 查询Address实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<Address>, IQueryable<TResult>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            try
            {
                return await query.Invoke(_context.Address.AsNoTracking()).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 查询Address列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<TResult>> GetAddressAsync<TResult>(Func<IQueryable<Address>, IQueryable<TResult>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            try
            {
                return await query.Invoke(_context.Address.AsNoTracking()).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public async Task<Address> Addressupdate(Address add)
        {
            try
            {
                _context.Address.Update(add);
                await _context.SaveChangesAsync();
                return await _context.Address.Where(a => a.KeyId == add.KeyId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 新增收货地址
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public async Task<Address> Addressadd(Address add)
        {
            try
            {
                await _context.Address.AddAsync(add);
                await _context.SaveChangesAsync();
                return await _context.Address.Where(a => a.KeyId == add.KeyId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public async Task Addressdelete(Address add)
        {
            try
            {
                _context.Address.Remove(add);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
