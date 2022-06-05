using AffilateSource.Shared.ViewModel.Category;
using AffilateSource.Shared.ViewModel.Filter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AffilateSource.Data.Services.Interface
{
    public interface ICategoriesServices
    {
        Task<ListColumnSelectModel> GetAllCategoryFilter(); //Get danh mục filter admin
        Task<IEnumerable<CategoryQuickVM>> GetCategoryHome();
        Task<IEnumerable<CategoryQuickVM>> GetCategoryParent();  // Lấy danh sách danh mục cha
        Task<IEnumerable<CategoryQuickVM>> GetCategoryByParentId(int parentId);  // Lấy danh sách danh mục con từ id cha
        Task<IEnumerable<CategoriesSelectViewModel>> GetCategoriesByParentId(int parentId);  // Lấy danh sách danh mục con từ id cha dùng cho dropdownlist
    }
}
