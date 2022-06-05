using AffilateSource.Shared.ViewModel;
using AffilateSource.Shared.ViewModel.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Data.Services.Interface
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductHomeViewModel>> GetProductHome();
        Task<IEnumerable<ProductHomeViewModel>> GetProductByViewCount();
        Task<IEnumerable<ProductHomeViewModel>> GetProductByCategoryIdHomeTop(int categoryId);
        Task<ProductHomeViewModel> GetProductById(int id);
        Task<DataEnvelope<ProductHomeViewModel>> GetAllProductPaging(int pageIndex, int pageSize);
        Task<DataEnvelope<ProductHomeViewModel>> GetAllProductByCategoryIdPaging(int categoryId, int pageIndex, int pageSize);
        Task<IEnumerable<ProductSelectViewModel>> GetProductSelectByCategoryId(int categoryId);
        Task<ProductCreateViewModel> CreateProduct(ProductCreateViewModel request);
        Task<ProductCreateViewModel> UpdateProduct(ProductCreateViewModel request);
        Task<Telerik.DataSource.DataSourceResult> GetProductPagingFilterAdmin(DataSourceRequest request);
        Task<ProductCreateViewModel> GetDetailsProductUpdateById(int id);
    }
}
