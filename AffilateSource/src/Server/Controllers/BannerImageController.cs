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
        [HttpPost("CreateSlide")]
        public async Task<IActionResult>CreateSlide(SlideImageVm slideImageVm)
        {
            var slide =await _bannerImageServices.CreateImageSlides(slideImageVm);
            return Ok(slide);
        }

        [HttpPost("GetBannerSlidePagingFilterAdmin")]
        public async Task<DataSourceResult> GetBannerSlidePagingFilterAdmin([FromBody] DataSourceRequest request)
        {
            var post = await _bannerImageServices.GetBannerSlidePagingFilterAdmin(request);
            return post;
        }


        [HttpGet("GetBannerSlide")]
        public async Task<IActionResult> GetSlide()
        {
            var slide =await _bannerImageServices.GetBannerSlide();
            return Ok(slide);
        }
    }
}
