using AffilateSource.Shared.ViewModel.Category;
using AffilateSource.Shared.ViewModel.Filter;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Data.Services.Interface
{
    public interface ICategoriesServices
    {
        Task<ListColumnSelectModel> GetAllCategoryFilter(); //Get danh mục filter admin
        Task<IEnumerable<CategoryQuickVM>> GetCategoryHome();
        Task<IEnumerable<CategoryQuickVM>> GetCategoryParent();  // Lấy danh sách danh mục cha
        Task<IEnumerable<CategoryQuickVM>> GetCategoryByParentId(int parentId);  // Lấy danh sách danh mục con từ id cha
        Task<IEnumerable<CategoriesSelectViewModel>> GetCategoriesByParentId(int parentId);  // Lấy danh sách danh mục con từ id cha dùng cho dropdownlist
        Task<DataSourceResult> GetCategoryPagingFilterAdmin(DataSourceRequest request);
        Task<IEnumerable<CategoryQuickVM>> GetCategoriesByParentIdAdmin(int parentId);
        Task<CategoryQuickVM> UpdateCategories(CategoryQuickVM objEmp);
        Task<CategoryQuickVM> GetCategoryDetailById(int id);   // Lấy chi tiết thông tin của 1 danh mục dùng cho update 
    }
}
