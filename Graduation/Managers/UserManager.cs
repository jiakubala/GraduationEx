using Graduation.Dto.Request;
using Graduation.Models;
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
        protected IIndexStore _indexStore { get; }
        protected ILoginStore _loginStore { get; }
        public UserManager(IUserStore userStore, IIndexStore indexStore, ILoginStore loginStore)
        {
            _userStore = userStore;
            _indexStore = indexStore;
            _loginStore = loginStore;
        }

        /// <summary>
        /// 查询User实体
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<User> Userqurey(int? userid)
        {
            try
            {
                return await _userStore.GetuserAsync(a => a.Where(b => b.UserId == userid));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> Userlistqurey()
        {
            try
            {
                return await _userStore.GetuserlistAsync(a => a.Where(b => b.UserId != null));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pas"></param>
        /// <returns></returns>
        public async Task<bool> Psupdate(PasswordRequest pas)
        {
            try
            {
                var old = await _userStore.GetuserAsync(a => a.Where(b => b.UserId == pas.UserId));
                var model = await _loginStore.GetAsync(a => a.Where(b => b.Password == pas.Oldpassword && b.UserId == pas.UserId));
                if (model == null)
                {
                    return false;
                }
                if (pas.Newpassword != pas.Upassword)
                {
                    return false;
                }
                old.Password = pas.Newpassword;
                await _userStore.Userupdate(old);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 修改User实体
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Userupdate(User user)
        {
            try
            {
                var old = await _userStore.GetuserAsync(a => a.Where(b => b.UserId == user.UserId));
                if (user.Name == null)
                {
                    user.Name = old.Name;
                }
                if (user.Email == null)
                {
                    user.Email = old.Email;
                }
                if (user.TrueName == null)
                {
                    user.TrueName = old.TrueName;
                }
                if (user.Sex == null)
                {
                    user.Sex = old.Sex;
                }
                if (user.Phone == null)
                {
                    user.Phone = old.Phone;
                }
                if (user.Password == null)
                {
                    user.Password = old.Password;
                }
                //修改个人资料
                await _userStore.Userupdate(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取我的收藏商品
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<Good>> Getfavorite(int? userid)
        {
            try
            {
                //获取收藏商品Id
                var fagoods = await _indexStore.GetFavoritelistAsync(a => a.Where(b => b.UserId == userid));
                List<int> goodids = new List<int>();
                foreach(var fagood in fagoods)
                {
                    goodids.Add(fagood.GoodId);
                }
                return await _indexStore.GetGoodAsync(a => a.Where(b => goodids.Contains(b.GoodId)));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 添加/删除收藏
        /// </summary>
        /// <param name="fr"></param>
        /// <returns></returns>
        public async Task<Good> Updatefavorite(FavoriteRequest fr)
        {
            try
            {
                //获取商品
                var Go = await _indexStore.GetAsync(a => a.Where(b => b.GoodId == fr.GoodId));
                //获取该商品的用户收藏
                var fa = await _indexStore.GetFavoriteAsync(a => a.Where(b => b.GoodId == fr.GoodId && b.UserId == fr.UserId));
                if (fa == null)
                {
                    await _indexStore.Favoriteadd(new Favorite
                    {
                        GoodId = fr.GoodId,
                        UserId = fr.UserId
                    });   
                    //增加收藏数量
                    Go.Facount += 1;
                    return await _indexStore.UpdateGood(Go);
                }
                await _indexStore.Favoritedelete(fa);
                //减少收藏数量
                Go.Facount -= 1;
                return await _indexStore.UpdateGood(Go);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取我的收货地址
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<Address>> GetAddressesAsync(int? userid)
        {
            try
            {
                return await _userStore.GetAddressAsync(a => a.Where(b => b.UserId == userid));
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
        public async Task<Address> UpdateAddressesAsync(Address add)
        {
            try
            {
                return await _userStore.Addressupdate(add);
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
        public async Task<Address> AddAddressesAsync(Address add)
        {
            try
            {
                return await _userStore.Addressadd(add);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="keyid"></param>
        /// <returns></returns>
        public async Task DeleteAddressesAsync(int keyid)
        {
            try
            {
                var address = await _userStore.GetAsync(a => a.Where(b => b.KeyId == keyid));
                await _userStore.Addressdelete(address);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取收货地址实体
        /// </summary>
        /// <param name="keyid"></param>
        /// <returns></returns>
        public async Task<Address> GetAddAsync(int keyid)
        {
            try
            {
                return await _userStore.GetAsync(a => a.Where(b => b.KeyId == keyid));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 封禁用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task DeleteuserAsync(int userid)
        {
            try
            {
                var user = await _userStore.GetuserAsync(a => a.Where(b => b.UserId == userid));
                await _userStore.Userdelete(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
