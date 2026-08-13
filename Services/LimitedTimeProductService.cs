using System;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Services
{
    public partial class LimitedTimeProductService : ILimitedTimeProductService
    {
        private readonly IRepository<LimitedTimeProduct> _limitedTimeProductRepository;
        private readonly IStaticCacheManager _staticCacheManager;

        public LimitedTimeProductService(
            IRepository<LimitedTimeProduct> limitedTimeProductRepository,
            IStaticCacheManager staticCacheManager)
        {
            _limitedTimeProductRepository = limitedTimeProductRepository;
            _staticCacheManager = staticCacheManager;
        }

        public async Task<LimitedTimeProduct> GetByIdAsync(int id)
        {
            return await _limitedTimeProductRepository.GetByIdAsync(id);
        }

        public async Task<IPagedList<LimitedTimeProduct>> GetAllPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _limitedTimeProductRepository.Table.OrderByDescending(x => x.Id);
            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<LimitedTimeProduct> GetActiveByProductIdAsync(int productId)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(
                LimitedTimeDefaults.ProductByProductIdCacheKey, productId);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                var utcNow = DateTime.UtcNow;

                var query = from ltp in _limitedTimeProductRepository.Table
                            where ltp.ProductId == productId
                                  && ltp.IsActive
                                  && ltp.StartDateUtc <= utcNow
                                  && ltp.EndDateUtc >= utcNow
                            select ltp;

                return await query.FirstOrDefaultAsync();
            });
        }

        public async Task InsertAsync(LimitedTimeProduct limitedTimeProduct)
        {
            await _limitedTimeProductRepository.InsertAsync(limitedTimeProduct);
            await ClearCacheAsync(limitedTimeProduct.ProductId);
        }

        public async Task UpdateAsync(LimitedTimeProduct limitedTimeProduct)
        {
            await _limitedTimeProductRepository.UpdateAsync(limitedTimeProduct);
            await ClearCacheAsync(limitedTimeProduct.ProductId);
        }

        public async Task DeleteAsync(LimitedTimeProduct limitedTimeProduct)
        {
            await _limitedTimeProductRepository.DeleteAsync(limitedTimeProduct);
            await ClearCacheAsync(limitedTimeProduct.ProductId);
        }

        private async Task ClearCacheAsync(int productId)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(
                LimitedTimeDefaults.ProductByProductIdCacheKey, productId);
            await _staticCacheManager.RemoveAsync(cacheKey);
        }
    }
}
