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
        Task<DataSourceResult> GetBannerImagePagingFilterAdmin(DataSourceRequest request);
        Task<SlideImageVm> UpdateSlide(SlideImageVm objEmp);
        Task<SlideImageVm> GetSlideById(int id);
        Task<BannerImageCreateUpdate> CreateBannerImage(BannerImageCreateUpdate slideImageVm);
        Task<BannerImageCreateUpdate> UpdateBannerImage(BannerImageCreateUpdate objEmp);
        Task<BannerImageCreateUpdate> GetBannerImageById(int id);
        Task<BannerImageViewModel> GetBannerImages();
    }
}
