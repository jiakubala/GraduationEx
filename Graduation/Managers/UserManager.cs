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
        public UserManager(IUserStore userStore, IIndexStore indexStore)
        {
            _userStore = userStore;
            _indexStore = indexStore;
        }

        /// <summary>
        /// 修改User实体
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> Userupdate(User user)
        {
            try
            {
                if (user.Password != null)
                {
                    //修改密码
                    return await _userStore.Userupdate(new User
                    {
                        Password = user.Password
                    });
                }
                if (user.Name == null)
                {
                    //显示个人资料
                    return await _userStore.GetuserAsync(a => a.Where(b => b.UserId == user.UserId));
                }
                //修改个人资料
                return await _userStore.Userupdate(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    TrueName = user.TrueName,
                    Phone = user.Phone,
                    Sex = user.Sex
                });
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
        public async Task<List<Good>> Getfavorite(int userid)
        {
            try
            {
                return await _indexStore.GetGoodAsync(a => a.Where(b => b.Faid == userid));
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
                var fa = await _indexStore.GetAsync(a => a.Where(b => b.GoodId == fr.GoodId));
                if (fa.Faid == null)
                {
                    fa.Faid = fr.UserId;
                }
                fa.Faid = null;
                return await _indexStore.UpdateGood(fa);
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
        public async Task<List<Address>> GetAddressesAsync(int userid)
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
                return await _userStore.Addressupdate(new Address
                {
                    Name = add.Name,
                    Local = add.Local,
                    Addres = add.Addres,
                    ZipCode = add.ZipCode,
                    Phone = add.Phone,
                });
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
                return await _userStore.Addressadd(new Address
                {
                    Name = add.Name,
                    Local = add.Local,
                    Addres = add.Addres,
                    ZipCode = add.ZipCode,
                    Phone = add.Phone,
                });
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

    }
}
