using Graduation.Models;
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
    }
}
