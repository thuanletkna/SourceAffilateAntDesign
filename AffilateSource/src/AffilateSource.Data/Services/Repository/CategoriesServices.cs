using AffilateSource.Data.Configuration;
using AffilateSource.Data.DataEntity;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.Kendohelpers;
using AffilateSource.Shared.ViewModel.Category;
using AffilateSource.Shared.ViewModel.Filter;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Data.Services.Repository
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly SqlConnectionConfiguration _configuration;
        public readonly ApplicationDbContext _context;
        public CategoriesServices(SqlConnectionConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<IEnumerable<CategoryQuickVM>> GetCategoryByParentId(int parentId)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {

                    IEnumerable<CategoryQuickVM> productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@ParentId", parentId);
                    productQuickViewModel = await conn.QueryAsync<CategoryQuickVM>("[GetCategoryByParentId]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CategoryQuickVM>> GetCategoryHome()
        {
            try
            {
                IEnumerable<CategoryQuickVM> productQuickViewModel;
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    productQuickViewModel = await conn.QueryAsync<CategoryQuickVM>("[GetCategoryQuickVM]", commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Lấy danh sách danh mục cha
        public async Task<IEnumerable<CategoryQuickVM>> GetCategoryParent()
        {
            try
            {
                IEnumerable<CategoryQuickVM> productQuickViewModel;
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    productQuickViewModel = await conn.QueryAsync<CategoryQuickVM>("[CATEGORY_GetCategoryParent]", commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<CategoriesSelectViewModel>> GetCategoriesByParentId(int parentId)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {

                    IEnumerable<CategoriesSelectViewModel> productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@ParentId", parentId);
                    productQuickViewModel = await conn.QueryAsync<CategoriesSelectViewModel>("[CATEGORIES_GetCategoryByParentId]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Lấy danh sách danh mục con để lọc trong admin
        public async Task<ListColumnSelectModel> GetAllCategoryFilter()
        {
            ListColumnSelectModel listCate = new ListColumnSelectModel();
            List<CategoriesSelectViewModel> Category;
            var sql = @"SELECT CategoryName, Id FROM [Categories] where ParentId = 3 ";
            try
            {
                using var conn = new SqlConnection(_configuration.Value);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var res = await conn.QueryAsync<CategoriesSelectViewModel>(sql);
                Category = res.ToList();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            listCate.ListCategoryFilter = Category;
            return listCate;
        }

        // Hiển thị danh sách tất cả bài đăng ra trang chủ admin
        public async Task<DataSourceResult> GetCategoryPagingFilterAdmin(DataSourceRequest request)
        {
            var result = new DataSourceResult();
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    string where = " 1=1  ";
                    //if (request.Filters.Any())
                    //    where += KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    string sort = KendoApplyFilter.GetSorts<CategoryQuickVM>(request);
                    result.Data = await conn.QueryAsync<CategoryQuickVM>("[CATEGORIES_GetAllCategoryFilterAdmin]",
                        new { @pageSize = request.PageSize, @page = request.Page, @where = where, @orderBy = sort }, commandType: CommandType.StoredProcedure);
                    result.Total = await conn.QueryFirstOrDefaultAsync<int>("[CATEGORIES_GetAllCategoryFilterAdminTotal]", new { where }, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                return result;
            }
        }

    }
}
