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
        public IndexManager(IIndexStore indexStore)
        {
            _indexStore = indexStore;
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
        public async Task<List<Good>> GetGoodsAsync(int code)
        {
            try
            {
                var list = await _indexStore.GetGoodAsync(a => a.Where(b => b.Stock != 0));
                //特别推荐
                if (code == 1)
                {
                    return list.OrderByDescending(a => a.Facount).ToList();
                }
                //新品上架
                if (code == 2)
                {
                    return list.OrderByDescending(a => a.Createtime).ToList();
                }
                //热门商品
                if (code == 3)
                {
                    return list.OrderBy(a => a.Facount).ToList();
                }
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
                //是否根据销量排序
                if (condition.IsSales != null)
                {
                    return condition.Goodlist.OrderByDescending(a => a.Sold).ToList();
                }
                //根据价格排序
                if (condition.IsPrice != null)
                {
                    return condition.Goodlist.OrderByDescending(a => a.Price).ToList();
                }
                //根据价格区间排序
                if (condition.Minprice != null && condition.Maxprice != null)
                {
                    return condition.Goodlist.Where(a => a.Price >= condition.Minprice && a.Price <= condition.Maxprice).ToList();
                }
                //默认
                return condition.Goodlist;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
