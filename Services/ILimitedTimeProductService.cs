using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Services
{
    public partial interface ILimitedTimeProductService
    {
        Task<LimitedTimeProduct> GetByIdAsync(int id);

        Task<IPagedList<LimitedTimeProduct>> GetAllPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue);

        Task<LimitedTimeProduct> GetActiveByProductIdAsync(int productId);

        Task InsertAsync(LimitedTimeProduct limitedTimeProduct);

        Task UpdateAsync(LimitedTimeProduct limitedTimeProduct);

        Task DeleteAsync(LimitedTimeProduct limitedTimeProduct);
    }
}