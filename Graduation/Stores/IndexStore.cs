﻿using Graduation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Stores
{
    /// <summary>
    /// 首页数据层
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class IndexStore<TContext> : IIndexStore where TContext : UnifiedDbContext
    {
        public IndexStore(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        protected virtual TContext _context { get; }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<List<TResult>> GetGoodAsync<TResult>(Func<IQueryable<Good>, IQueryable<TResult>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            try
            {
                return query.Invoke(_context.Good.AsNoTracking()).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
