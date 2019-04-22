using Graduation.Models;
using Graduation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<Good>> GetGoodsAsync()
        {
            try
            {
                return await _indexStore.GetGoodAsync(a => a.Where(b => b.Stock != 0));
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
        public async Task<List<Good>> GetGoodstypeAsync(string typename)
        {
            try
            {
                return await _indexStore.GetGoodAsync(a => a.Where(b => b.Type == typename));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
