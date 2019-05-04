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
        public async Task<List<TResult>> GetOrderAsync<TResult>(Func<IQueryable<Order>, IQueryable<TResult>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            try
            {
                return await query.Invoke(_context.Order.AsNoTracking()).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<Order>, IQueryable<TResult>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            try
            {
                return await query.Invoke(_context.Order.AsNoTracking()).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task Orderdelete(Order order)
        {
            try
            {
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> AddOrder(Order order)
        {
            try
            {
                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();
                return await _context.Order.Where(a => a.OrderId == order.OrderId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Order> UpdateOrder(Order order)
        {
            try
            {
                _context.Order.Update(order);
                await _context.SaveChangesAsync();
                return await _context.Order.Where(a => a.OrderId == order.OrderId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
