using Graduation.Dto.Request;
using Graduation.Models;
using Graduation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Managers
{
    /// <summary>
    /// 首页逻辑层
    /// </summary>
    public class IndexManager
    {
        protected IIndexStore _indexStore { get; }
        protected IOrderStore _orderStore { get; }
        public IndexManager(IIndexStore indexStore, IOrderStore orderStore)
        {
            _indexStore = indexStore;
            _orderStore = orderStore;
        }

        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        public async Task<Good> GetAsync(int goodid)
        {
            try
            {
                return await _indexStore.GetAsync(a => a.Where(b => b.GoodId == goodid));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Good>> GetGoodsAsync()
        {
            try
            {
                var list = await _indexStore.GetGoodAsync(a => a.Where(b => b.Stock != 0));
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetTypesAsync()
        {
            try
            {
                var list = await _indexStore.GetTypelistAsync();
                List<string> typelist = new List<string>();
                foreach (var model in list)
                {
                    typelist.Add(model.TypeName);
                }
                return typelist;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 获取商品列表（分类分支）
        /// </summary>
        /// <returns></returns>
        public async Task<List<Good>> GetGoodstypeAsync(GoodlistRequest condition)
        {
            try
            {
                var list = await _indexStore.GetGoodAsync(a => a.Where(b => b.Stock != 0));
                //搜索条件
                if (condition.Keyword != null)
                {
                    return list.Where(a => a.Name.Contains(condition.Keyword)).ToList();
                }
                //分支类型
                if (condition.Typename != null)
                {
                    return list.Where(a => a.Type == condition.Typename).ToList();
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 分类商品列表排序
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<List<Good>> OrderGoodstypeAsync(GoodlistinfoRequest condition)
        {
            try
            {
                var list = await _indexStore.GetGoodAsync(a => a.Where(b => condition.Goodidlist.Contains(b.GoodId)));
                //是否根据销量排序
                if (condition.IsSales != null)
                {
                    return list.OrderByDescending(a => a.Sold).ToList();
                }
                //根据价格排序
                if (condition.IsPrice != null)
                {
                    return list.OrderByDescending(a => a.Price).ToList();
                }
                //根据价格区间排序
                if (condition.Minprice != null && condition.Maxprice != null)
                {
                    return list.Where(a => a.Price >= condition.Minprice && a.Price <= condition.Maxprice).ToList();
                }
                //默认
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="goodid"></param>
        /// <returns></returns>
        public async Task DeleteGoodAsync(int goodid)
        {
            try
            {
                var good = await _indexStore.GetAsync(a => a.Where(b => b.GoodId == goodid));
                var orders = await _orderStore.GetOrderAsync(a => a.Where(b => b.GoodId == goodid && b.OrderState == 1));
                await _indexStore.Gooddelete(good);
                foreach (var order in orders)
                { 
                    await _orderStore.Orderdelete(order);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public async Task AddGoodAsync(Good good)
        {
            try
            {
                var oldtypes = await _indexStore.GetTypelistAsync();
                List<string> typenames = new List<string>();
                foreach (var oldtyoe in oldtypes)
                {
                    typenames.Add(oldtyoe.TypeName);
                }
                if (typenames.Contains(good.Type))
                {
                    await _indexStore.Goodadd(new Good
                    {
                        Name = good.Name,
                        Price = good.Price,
                        Type = good.Type,
                        Stock = good.Stock,
                        Details = good.Details,
                        Createtime = DateTime.Now
                    });
                }
                await _indexStore.Goodadd(new Good
                {
                    Name = good.Name,
                    Price = good.Price,
                    Type = "其他商品",
                    Stock = good.Stock,
                    Details = good.Details,
                    Createtime = DateTime.Now
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
