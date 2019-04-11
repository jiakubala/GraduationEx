using Graduation.Models;
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
    }
}
