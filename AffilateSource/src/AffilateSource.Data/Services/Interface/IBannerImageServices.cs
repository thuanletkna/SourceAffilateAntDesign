using AffilateSource.Shared.ViewModel.BannerImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Data.Services.Interface
{
    public interface IBannerImageServices
    {
        Task<SlideImageVm> CreateImageSlides(SlideImageVm slideImageVm);
        Task<BannerImageViewModel> GetBannerSlide();
        Task<DataSourceResult> GetBannerSlidePagingFilterAdmin(DataSourceRequest request);
        Task<SlideImageVm> UpdateSlide(SlideImageVm objEmp);
        Task<SlideImageVm> GetSlideById(int id);
    }
}
