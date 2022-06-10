using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.BannerImages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BannerImageController : ControllerBase
    {
        private readonly IBannerImageServices _bannerImageServices;
        public BannerImageController(IBannerImageServices bannerImageServices)
        {
            _bannerImageServices = bannerImageServices;
        }

        #region Admin

        [HttpPost("CreateSlide")]
        public async Task<IActionResult>CreateSlide(SlideImageVm slideImageVm)
        {
            var slide =await _bannerImageServices.CreateImageSlides(slideImageVm);
            return Ok(slide);
        }
        [HttpPost("CreateBannerImage")]
        public async Task<IActionResult> CreateBannerImage(BannerImageCreateUpdate slideImageVm)
        {
            var slide =await _bannerImageServices.CreateBannerImage(slideImageVm);
            return Ok(slide);
        }
        [HttpPost("GetDetailSlideUpdateById")]
        public async Task<IActionResult> GetDetailSlideUpdateById([FromBody] int id)
        {
            var productDetail = await _bannerImageServices.GetSlideById(id);
            if (productDetail == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(productDetail);
            }
        } 
        [HttpPost("GetBannerImageById")]
        public async Task<IActionResult> GetBannerImageById([FromBody] int id)
        {
            var productDetail = await _bannerImageServices.GetBannerImageById(id);
            if (productDetail == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(productDetail);
            }
        }
        [HttpPost("UpdateSlide")]
        public async Task<IActionResult> UpdateSlide(SlideImageVm slideImageVm)
        {
            var slide = await _bannerImageServices.UpdateSlide(slideImageVm);
            return Ok(slide);
        } 
        [HttpPost("UpdateBannerImage")]
        public async Task<IActionResult> UpdateBannerImage(BannerImageCreateUpdate slideImageVm)
        {
            var slide = await _bannerImageServices.UpdateBannerImage(slideImageVm);
            return Ok(slide);
        }

        [HttpPost("GetBannerSlidePagingFilterAdmin")]
        public async Task<DataSourceResult> GetBannerSlidePagingFilterAdmin([FromBody] DataSourceRequest request)
        {
            var post = await _bannerImageServices.GetBannerSlidePagingFilterAdmin(request);
            return post;
        }

        [HttpPost("GetBannerImagePagingFilterAdmin")]
        public async Task<DataSourceResult> GetBannerImagePagingFilterAdmin([FromBody] DataSourceRequest request)
        {
            var post = await _bannerImageServices.GetBannerImagePagingFilterAdmin(request);
            return post;
        }

        #endregion Admin

        [HttpGet("GetBannerSlide")]
        public async Task<IActionResult> GetSlide()
        {
            var slide = await _bannerImageServices.GetBannerSlide();
            return Ok(slide);
        }
    }
}
