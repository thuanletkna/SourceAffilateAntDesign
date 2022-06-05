using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Post;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postServices;
        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }
        [HttpPost("GetPostPagingFilterAdmin")]
        public async Task<DataSourceResult> GetPostPagingFilterAdmin([FromBody] DataSourceRequest request)
        {
            var post = await _postServices.GetPostPagingFilterAdmin(request);
            return post;
        }
        [HttpGet("GetPostHome")]
        public async Task<IActionResult>GetPostHome()
        {
            var postHome = await _postServices.GetPostHome();
            return Ok(postHome);
        }
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody]PostCreateViewModel request)
        {
            var post = await _postServices.CreatePost(request);
            return Ok(post);
        }
        [HttpPost("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] PostCreateViewModel request)
        {
            var post = await _postServices.UpdatePost(request);
            return Ok(post);
        }
        [HttpPost("UpdatePostDetail")]
        public async Task<IActionResult> UpdatePostDetail([FromBody] PostDetailVm request)
        {
            var post = await _postServices.UpdatePostDetail(request);
            return Ok(post);
        }
        [HttpPost("CreatePostDetail")]
        public async Task<IActionResult> CreatePostDetail([FromBody] PostDetailVm request)
        {
            var post = await _postServices.CreatePostDetail(request);
            return Ok(post);
        }
        [HttpGet("GetPostCreatedLast")]
        public async Task<IActionResult> GetPostCreatedLast()
        {
            var post = await _postServices.GetPostCreatedLast();
            return Ok(post);
        }
        
        //Api này get id tự động dành cho hàm tạo chi tiết bài đăng
        [HttpGet("GetPostDetailById")]
        public async Task<IActionResult> GetPostDetailById()
        {
            var post = await _postServices.GetPostDetailById();
            return Ok(post);
        }

        [HttpGet("GetPostByIdAdmin")]
        public async Task<IActionResult> GetPostByIdAdmin([FromBody] int id)
        {
            var post = await _postServices.GetPostById(id);
            return Ok(post);
        }
        [HttpPost("GetPostByIdAdminEdit")]
        public async Task<IActionResult> GetPostByIdAdminEdit([FromBody] int id)
        {
            var post = await _postServices.GetPostByIdAdminEdit(id);
            return Ok(post);
        }
        [HttpPost("GetPostDetailByIdAdmin")]
        public async Task<IActionResult> GetPostDetailByIdAdmin([FromBody] int id)
        {
            var post = await _postServices.GetPostDetailByIdAdmin(id);
            return Ok(post);
        }

        [HttpPost("GetDetailsByPostDetails")]
        public async Task<IActionResult> GetDetailsByPostDetails([FromBody] int id)
        {
            var post = await _postServices.GetDetailsByPostDetails(id);
            return Ok(post);
        }
    }
}
