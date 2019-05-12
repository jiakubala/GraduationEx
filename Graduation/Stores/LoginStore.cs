using Graduation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Stores
{
    /// <summary>
    /// 登录数据层
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class LoginStore<TContext> : ILoginStore where TContext : UnifiedDbContext
    {
        public LoginStore(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        protected virtual TContext _context { get; }

        /// <summary>
        /// 查询User实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<User>, IQueryable<TResult>> query)
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
        /// 查询Admin实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TResult> GetAdminAsync<TResult>(Func<IQueryable<Admin>, IQueryable<TResult>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            try
            {
                return await query.Invoke(_context.Admin.AsNoTracking()).SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> AddAsync(User user)
        {
            try
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
                return await _context.User.Where(a => a.UserId == user.UserId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
