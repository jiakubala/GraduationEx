using Graduation.Models;
using Graduation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Managers
{
    /// <summary>
    /// 登录逻辑层
    /// </summary>
    public class LoginManager
    {
        protected ILoginStore _loginStore { get; }
        public LoginManager(ILoginStore loginStore)
        {
            _loginStore = loginStore;
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(string password, int id)
        {
            try
            {
                return await _loginStore.GetAsync(a => a.Where(b => b.Password == password && b.UserId == id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name = "user" ></ param >
        /// < returns ></ returns >
        public async Task<User> AddAsync(User user)
        {
            return await _loginStore.AddAsync(new User
            {
                UserId = user.UserId,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                TrueName = user.TrueName,
                Sex = user.Sex
            });
        }
    }
}
