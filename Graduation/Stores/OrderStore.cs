using Graduation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Stores
{
    /// <summary>
    /// 订单数据层
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class OrderStore<TContext> : IOrderStore where TContext : UnifiedDbContext
    {
        public OrderStore(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        protected virtual TContext _context { get; }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<List<TResult>> GetOrderAsync<TResult>(Func<IQueryable<Order>, IQueryable<TResult>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            try
            {
                return query.Invoke(_context.Order.AsNoTracking()).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
