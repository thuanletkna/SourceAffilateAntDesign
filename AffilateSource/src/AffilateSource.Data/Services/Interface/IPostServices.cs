using AffilateSource.Shared.ViewModel;
using AffilateSource.Shared.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Data.Services.Interface
{
    public interface IPostServices
    {
        Task<IEnumerable<PostHomeViewModel>> GetPostHome();
        Task<IEnumerable<ListPostDetailVm>> GetPostDetailByPostId(int postId);
        Task<PostDetailVm> GetDetailsByPostDetails(int id);
        Task<PostHomeViewModel> GetPostById(int id);
        Task<PostCreateViewModel> GetPostByIdAdminEdit(int id);
        Task<List<PostDetailVm>> GetPostDetailByIdAdmin(int id);
        Task<DataEnvelope<PostHomeViewModel>> GetAllPostPaging(int pageIndex, int pageSize);
        Task<DataEnvelope<PostHomeViewModel>> GetAllPostByCategoryIdPaging(int categoryId, int pageIndex, int pageSize);
        Task<IEnumerable<PostHomeViewModel>> GetPostByViewCount();
        Task<IEnumerable<PostHomeViewModel>> GetPostByCreateDate(int categoryId);
        Task<PostCreateViewModel> CreatePost(PostCreateViewModel request);
        Task<PostCreateViewModel> UpdatePost(PostCreateViewModel request);
        Task<PostDetailVm> CreatePostDetail(PostDetailVm request);
        Task<List<PostHomeViewModel>> GetPostCreatedLast();
        Task<List<PostDetailVm>> GetPostDetailById();
        Task<PostDetailVm> UpdatePostDetail(PostDetailVm request);
        Task<Telerik.DataSource.DataSourceResult> GetPostPagingFilterAdmin(DataSourceRequest request);
        Task<DeleteViewModel> DeletePost(DeleteViewModel objEmp);
    }
}
