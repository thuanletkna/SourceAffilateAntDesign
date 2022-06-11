using AffilateSource.Data.Configuration;
using AffilateSource.Data.DataEntity;
using AffilateSource.Data.DataEntity.Entities;
using AffilateSource.Data.Helpers;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Data.Services.ServicesConfig;
using AffilateSource.Shared.Kendohelpers;
using AffilateSource.Shared.ViewModel;
using AffilateSource.Shared.ViewModel.Post;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Telerik.DataSource;
using DataSourceResult = Telerik.DataSource.DataSourceResult;

namespace AffilateSource.Data.Services.Repository
{
    public class PostServices : IPostServices
    {
        private readonly SqlConnectionConfiguration _configuration;
        private readonly ISequenceService _sequenceService;
        private readonly ApplicationDbContext _context;
        //private readonly IStorageService _storageService;
        public PostServices(SqlConnectionConfiguration configuration, ISequenceService sequenceService, ApplicationDbContext context)
        {
            _configuration = configuration;
            _sequenceService = sequenceService;
            _context = context;
            //_storageService = storageService;
        }

        public async Task<PostCreateViewModel> CreatePost(PostCreateViewModel request)
        {
            try
            {
                var post = new Post()
                {
                    SeoAlias = request.SeoAlias,
                    CategoryId = request.CategoryId,
                    CreateDate = DateTime.Now,
                    StatusId = request.StatusId,
                    Description = request.Description,
                    Detail = request.Detail,
                    Labels = request.Labels,
                    //OwnerUserId = User.GetUserId(),
                    ImagePost  =request.ImagePost,
                    Title = request.Title,
                    CategoryParentId = request.CategoryParentId
                };
                post.Id = await _sequenceService.GetKnowledgeBaseNewId();
                if (string.IsNullOrEmpty(request.SeoAlias))
                {
                    post.SeoAlias = TextHelper.ToUnsignString(request.Title);
                }
                // Lưu lại vào database
                await _context.Posts.AddAsync(post);

                var result = await _context.SaveChangesAsync();
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

        public async Task<PostCreateViewModel> UpdatePost( PostCreateViewModel request)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@Id", request.Id);
                    ObjParm.Add("@Title", request.Title);
                    ObjParm.Add("@Description", request.Description);
                    ObjParm.Add("@StatusId", request.StatusId);
                    if (!string.IsNullOrEmpty(request.SeoAlias))
                    {
                        request.SeoAlias = TextHelper.ToUnsignString(request.Title);
                    }
                    ObjParm.Add("@SeoAlias", request.SeoAlias);
                    ObjParm.Add("@CategoryParentId", request.CategoryParentId);
                    ObjParm.Add("@ImagePost", request.ImagePost);
                    await conn.ExecuteAsync("[POST_update]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return request;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<PostDetailVm> CreatePostDetail(PostDetailVm request)
        {
            var sql = @"SELECT TOP 1 Id FROM Posts ORDER BY Id DESC";
            var conn = new SqlConnection(_configuration.Value);
            var postdetail = new PostDetail()
            {
                PostId = conn.ExecuteScalar<int>(sql),
                ProductId = request.ProductId,
                Content = request.Content,
                StatusId = request.StatusIdDetail,
                SortDetail = request.SortDetail,
                TitleDetail = request.TitleDetail,
                ImageProducts = request.ImageProducts,
                LinkAffilateLazada = request.LinkAffilateLazada,
                LinkAffilateOther = request.LinkAffilateOther,
                LinkAffilateShopee= request.LinkAffilateShopee,
                ProductAffilateName = request.ProductAffilateName,
                ProductAffilatePrice = request.ProductAffilatePrice
            };
            await _context.PostDetails.AddAsync(postdetail);
            var data = await _context.SaveChangesAsync();
            if(data > 0)
            {
                return request;
            }else
            {
                return request;
            }    
        }
        public async Task<PostDetailVm> CreatePostDetailByPosstId(PostDetailVm request)
        {
           
            var postdetail = new PostDetail()
            {
                PostId = request.PostId,
                ProductId = request.ProductId,
                Content = request.Content,
                StatusId = request.StatusIdDetail,
                SortDetail = request.SortDetail,
                TitleDetail = request.TitleDetail,
                ImageProducts = request.ImageProducts,
                LinkAffilateLazada = request.LinkAffilateLazada,
                LinkAffilateOther = request.LinkAffilateOther,
                LinkAffilateShopee = request.LinkAffilateShopee,
                ProductAffilateName = request.ProductAffilateName,
                ProductAffilatePrice = request.ProductAffilatePrice
            };
            await _context.PostDetails.AddAsync(postdetail);
            var data = await _context.SaveChangesAsync();
            if (data > 0)
            {
                return request;
            }
            else
            {
                return request;
            }
        }
        public async Task<PostDetailVm> UpdatePostDetail(PostDetailVm request)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@Id", request.Id);
                    ObjParm.Add("@Content", request.Content);
                    ObjParm.Add("@TitleDetail", request.TitleDetail);
                    ObjParm.Add("@StatusId", request.StatusId);
                    //ObjParm.Add("@ProductId", request.ProductId);
                    ObjParm.Add("@SortDetail", request.SortDetail);
                    ObjParm.Add("@ProductAffilatePrice", request.ProductAffilatePrice);
                    ObjParm.Add("@ProductAffilateName", request.ProductAffilateName);
                    ObjParm.Add("@ImageProducts", request.ImageProducts);
                    ObjParm.Add("@LinkAffilateLazada", request.LinkAffilateLazada);
                    ObjParm.Add("@LinkAffilateOther", request.LinkAffilateOther);
                    ObjParm.Add("@LinkAffilateShopee", request.LinkAffilateShopee);
                    await conn.ExecuteAsync("[POSTDETAILS_UpdateDetail]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return request;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<List<PostHomeViewModel>> GetPostCreatedLast()
        {
            var sql = @"SELECT TOP 1 Id FROM Posts ORDER BY Id DESC";
            var conn = new SqlConnection(_configuration.Value);
            int id = conn.ExecuteScalar<int>(sql);
            var post = _context.Posts.Select(x => new PostHomeViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Detail = x.Detail
            }).Where(x => x.Id == id).ToListAsync(); ;
            return post;
        }
        public async Task<List<PostDetailVm>> GetPostDetailById()
        {
            var sql = @"SELECT TOP 1 Id FROM Posts ORDER BY Id DESC";
            //var conn = new SqlConnection(_configuration.Value);
            //int id = conn.ExecuteScalar<int>(sql);
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    int id = conn.ExecuteScalar<int>(sql);
                    List<PostDetailVm> productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@postId", id);
                    productQuickViewModel = (List<PostDetailVm>)await conn.QueryAsync<PostDetailVm>("[POSTDETAILS_GetPostDetailsByPostId]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        
        //Get danh sách chi tiết bài đăng từ bài đăng
        public async Task<List<PostDetailVm>> GetPostDetailByIdAdmin(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    List<PostDetailVm> productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@postId", id);
                    productQuickViewModel = (List<PostDetailVm>)await conn.QueryAsync<PostDetailVm>("[POSTDETAILS_GetPostDetailsByPostId]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //Get thông tin chi tiết bài đăng  của id chi tiết bài đăng
        public async Task<PostDetailVm> GetDetailsByPostDetails(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {
                    PostDetailVm productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@id", id);
                    productQuickViewModel = await conn.QuerySingleOrDefaultAsync<PostDetailVm>("[POSTDETAILS_GetDetailsByPostDetails]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PostHomeViewModel>> GetPostHome()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    IEnumerable<PostHomeViewModel> post = await conn.QueryAsync<PostHomeViewModel>("POST_GetAllHomeTop", commandType: CommandType.StoredProcedure);
                    return post;
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

        public async Task<IEnumerable<PostHomeViewModel>> GetPostByViewCount()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {

                    IEnumerable<PostHomeViewModel> post = await conn.QueryAsync<PostHomeViewModel>("POST_GetPostByViewCount", commandType: CommandType.StoredProcedure);
                    return post;
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

        //Get tất cả bài đăng trang chủ
        public async Task<DataEnvelope<PostHomeViewModel>> GetAllPostPaging(int pageIndex, int pageSize)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    var data = await conn.QueryAsync<PostHomeViewModel>("POST_GetAllPost",
                        new { @pageSize = pageSize, @page = pageIndex }, commandType: CommandType.StoredProcedure);
                    var total = await conn.QueryFirstOrDefaultAsync<int>("POST_GetTotalPost", commandType: CommandType.StoredProcedure);
                    var result = new DataEnvelope<PostHomeViewModel>
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


        // Get tất cả bài đăng của từng danh mục trang chủ
        public async Task<DataEnvelope<PostHomeViewModel>> GetAllPostByCategoryIdPaging(int categoryId, int pageIndex, int pageSize)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    var data = await conn.QueryAsync<PostHomeViewModel>("POST_GetAllPostByCategoryId",
                        new { @categoryId = categoryId, @pageSize = pageSize, @page = pageIndex }, commandType: CommandType.StoredProcedure);
                    var total = await conn.QueryFirstOrDefaultAsync<int>("POST_GetTotalPost", commandType: CommandType.StoredProcedure);
                    var result = new DataEnvelope<PostHomeViewModel>
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

        // Get chi tiết bài đăng
        public async Task<PostHomeViewModel> GetPostById(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {

                    PostHomeViewModel productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@Id", id);
                    productQuickViewModel = await conn.QuerySingleOrDefaultAsync<PostHomeViewModel>("[POST_GetDetailById]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PostCreateViewModel> GetPostByIdAdminEdit(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {

                    PostCreateViewModel productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@Id", id);
                    productQuickViewModel = await conn.QuerySingleOrDefaultAsync<PostCreateViewModel>("[POST_GetDetailById]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ListPostDetailVm>> GetPostDetailByPostId(int postId)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    IEnumerable<ListPostDetailVm> post = await conn.QueryAsync<ListPostDetailVm>("[POSTDETAILS_GetPostDetailsByPostId]", new { @postId = postId }, commandType: CommandType.StoredProcedure);
                    return post;
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

        // Get 5 post đăng mới nhất
        public async Task<IEnumerable<PostHomeViewModel>> GetPostByCreateDate(int categoryId)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    IEnumerable<PostHomeViewModel> post = await conn.QueryAsync<PostHomeViewModel>("POST_GetPostByCreateDate", new { @categoryId = categoryId }, commandType: CommandType.StoredProcedure);
                    return post;
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


        // Hiển thị danh sách tất cả bài đăng ra trang chủ admin
        public async Task<DataSourceResult> GetPostPagingFilterAdmin(DataSourceRequest request)
        {
            var result = new DataSourceResult();
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                try
                {
                    string where = " 1=1 And ";
                    if (request.Filters.Any())
                        where += KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    string sort = KendoApplyFilter.GetSorts<PostHomeViewModel>(request);
                    result.Data = await conn.QueryAsync<PostHomeViewModel>("[POST_GetAllPostFilterAdmin]",
                        new { @pageSize = request.PageSize, @page = request.Page, @where = where, @orderBy = sort }, commandType: CommandType.StoredProcedure);
                    result.Total = await conn.QueryFirstOrDefaultAsync<int>("[POST_GetAllPostFilterAdminTotal]", new { where }, commandType: CommandType.StoredProcedure);
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
        public async Task<DeleteViewModel> DeletePost(DeleteViewModel objEmp)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                //Nếu user hoạt động thì nghỉ việc tạm thời sẽ bằng false và ngược lại. Set cứng ở frontend
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Id", objEmp.Id);
                await conn.ExecuteAsync("[POST_Delete]", ObjParm, commandType: CommandType.StoredProcedure);
                conn.Close();
                return objEmp;
            }
        }
    }
}
