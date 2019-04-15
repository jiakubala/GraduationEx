using Graduation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Managers
{
    /// <summary>
    /// 个人中心逻辑层
    /// </summary>
    public class UserManager
    {
        protected IUserStore _userStore { get; }
        public UserManager(IUserStore userStore)
        {
            _userStore = userStore;
        }
    }
}
