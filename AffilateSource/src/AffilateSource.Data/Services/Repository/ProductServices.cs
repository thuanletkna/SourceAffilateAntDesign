using AffilateSource.Data.Configuration;
using AffilateSource.Data.DataEntity;
using AffilateSource.Data.DataEntity.Entities;
using AffilateSource.Data.Helpers;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.Kendohelpers;
using AffilateSource.Shared.ViewModel;
using AffilateSource.Shared.ViewModel.Product;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Data.Services.Repository
{
    public class ProductServices : IProductServices
    {
        private readonly SqlConnectionConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductServices(SqlConnectionConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<ProductHomeViewModel>> GetProductHome()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {

                    IEnumerable<ProductHomeViewModel> product = await conn.QueryAsync<ProductHomeViewModel>("PRODUCT_GetAllHomeTop", commandType: CommandType.StoredProcedure);
                    return product;
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

            }
        }
        public async Task<DataEnvelope<ProductHomeViewModel>> GetAllProductPaging(int pageIndex, int pageSize)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    var data = await conn.QueryAsync<ProductHomeViewModel>("PRODUCT_GetAllProduct",
                        new { pageSize, @page = pageIndex }, commandType: CommandType.StoredProcedure);
                    var total = await conn.QueryFirstOrDefaultAsync<int>("PRODUCT_GetTotalProduct", commandType: CommandType.StoredProcedure);
                    var result = new DataEnvelope<ProductHomeViewModel>
                    {
                        Data = data,
                        Total = total,
                        PageSize = pageSize,
                        PageIndex = pageIndex,
                        TotalRecords = total,
                    };
                    return result;
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

            }
        }
        public async Task<DataEnvelope<ProductHomeViewModel>> GetAllProductByCategoryIdPaging(int categoryId, int pageIndex, int pageSize)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    var data = await conn.QueryAsync<ProductHomeViewModel>("PRODUCT_GetAllProductByCategoryid",
                        new { categoryId, pageSize, @page = pageIndex }, commandType: CommandType.StoredProcedure);
                    var total = await conn.QueryFirstOrDefaultAsync<int>("PRODUCT_GetTotalProduct", commandType: CommandType.StoredProcedure);
                    var result = new DataEnvelope<ProductHomeViewModel>
                    {
                        Data = data,
                        Total = total,
                        PageSize = pageSize,
                        PageIndex = pageIndex,
                        TotalRecords = total,
                    };
                    return result;
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

            }
        }

        public async Task<IEnumerable<ProductHomeViewModel>> GetProductByCategoryIdHomeTop(int categoryId)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {

                    IEnumerable<ProductHomeViewModel> productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@CategoryId", categoryId);
                    productQuickViewModel = await conn.QueryAsync<ProductHomeViewModel>("[PRODUCT_GetProductByCategoryIdHomeTop]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductHomeViewModel>> GetProductByViewCount()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {

                    IEnumerable<ProductHomeViewModel> product = await conn.QueryAsync<ProductHomeViewModel>("PRODUCT_GetProductByViewCount", commandType: CommandType.StoredProcedure);
                    return product;
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

            }
        }

        public async Task<ProductHomeViewModel> GetProductById(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {

                    ProductHomeViewModel productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@Id", id);
                    productQuickViewModel = await conn.QuerySingleOrDefaultAsync<ProductHomeViewModel>("[PRODUCT_GetDetailById]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ProductSelectViewModel>> GetProductSelectByCategoryId(int categoryId)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {

                    IEnumerable<ProductSelectViewModel> productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@categoryId", categoryId);
                    productQuickViewModel = await conn.QueryAsync<ProductSelectViewModel>("[PRODUCT_GetProductByCategoryId]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Create-Update-ViewDetailUpdate Product Admin
        public async Task<ProductCreateViewModel> CreateProduct(ProductCreateViewModel request)
        {
            try
            {
                var sql = @"SELECT TOP 1 Id FROM Products ORDER BY Id DESC";
                var conn = new SqlConnection(_configuration.Value);
                var product = new Product()
                {
                    Id = conn.ExecuteScalar<int>(sql) +1,
                    SeoAlias = request.SeoAlias,
                    CategoryId = 2, // Danh mục sản phẩm set mặc định bằng 2
                    CreateDate = DateTime.Now,
                    StatusId = request.StatusId,
                    Description = request.Description,
                    Detail = request.Detail,
                    Labels = request.Labels,
                    //OwnerUserId = User.GetUserId(),
                    ImageProducts = request.ImageProducts,
                    Title = request.Title,
                    CategoryParentId = request.CategoryParentId,
                    Price  = request.Price,
                    LinkAffilateLazada = request.LinkAffilateLazada,
                    LinkAffilateShopee = request.LinkAffilateShopee,
                    LinkAffilateOther = request.LinkAffilateOther,
                    LinkAffilateTiki = request.LinkAffilateTiki
                };
               
                if (string.IsNullOrEmpty(request.SeoAlias))
                {
                    product.SeoAlias = TextHelper.ToUnsignString(request.Title);
                }

                // Lưu lại vào database
                await _applicationDbContext.Products.AddAsync(product);

                var result = await _applicationDbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return request;
                }
                else
                {
                    return request;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductCreateViewModel> UpdateProduct(ProductCreateViewModel request)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@Id", request.Id);
                    ObjParm.Add("@Title", request.Title);
                    ObjParm.Add("@Description", request.Description);
                    ObjParm.Add("@ImageProducts", request.ImageProducts);
                    ObjParm.Add("@StatusId", request.StatusId);
                    ObjParm.Add("@Details",request.Detail);
                    ObjParm.Add("@Price", request.Price);
                    ObjParm.Add("@LinkAffilateLazada", request.LinkAffilateLazada);
                    ObjParm.Add("@LinkAffilateOther", request.LinkAffilateOther);
                    ObjParm.Add("@LinkAffilateShopee", request.LinkAffilateShopee);
                    ObjParm.Add("@LinkAffilateTiki", request.LinkAffilateTiki);
                    if (!string.IsNullOrEmpty(request.SeoAlias))
                    {
                        request.SeoAlias = TextHelper.ToUnsignString(request.Title);
                    }
                    ObjParm.Add("@SeoAlias", request.SeoAlias);
                    ObjParm.Add("@CategoryParentId", request.CategoryParentId);
                    await conn.ExecuteAsync("[PRODUCT_update]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return request;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get thông tin chi tiết sản phẩm hiển thị dành cho update sản phẩm ở phần admin
        public async Task<ProductCreateViewModel> GetDetailsProductUpdateById(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    ProductCreateViewModel productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@id", id);
                    productQuickViewModel = await conn.QuerySingleOrDefaultAsync<ProductCreateViewModel>("[PRODUCT_GetDetailById]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Create-Update-ViewDetailUpdate Product Admin

        public async Task<Telerik.DataSource.DataSourceResult> GetProductPagingFilterAdmin(DataSourceRequest request)
        {
            var result = new Telerik.DataSource.DataSourceResult();
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    string where = " 1=1 And ";
                    if (request.Filters.Any())
                        where += KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    string sort = KendoApplyFilter.GetSorts<ProductHomeViewModel>(request);
                    result.Data = await conn.QueryAsync<ProductHomeViewModel>("[PRODUCT_GetAllProductFilterAdmin]",
                        new { @pageSize = request.PageSize, @page = request.Page, where, @orderBy = sort }, commandType: CommandType.StoredProcedure);
                    result.Total = await conn.QueryFirstOrDefaultAsync<int>("[PRODUCT_GetAllProductFilterAdminTotal]", new { where }, commandType: CommandType.StoredProcedure);
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
