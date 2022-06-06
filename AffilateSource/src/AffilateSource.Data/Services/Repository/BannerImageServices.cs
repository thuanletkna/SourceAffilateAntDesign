using AffilateSource.Data.Configuration;
using AffilateSource.Data.DataEntity;
using AffilateSource.Data.DataEntity.Entities;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.Kendohelpers;
using AffilateSource.Shared.ViewModel.BannerImages;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Data.Services.Repository
{
    public class BannerImageServices : IBannerImageServices
    {
        private readonly SqlConnectionConfiguration _configuration;
        public readonly ApplicationDbContext _context;
        public BannerImageServices(SqlConnectionConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        //public async Task<BannerImageViewModel>BannerImageViewModel(BannerImageViewModel bannerImageViewModel)
        //{

        //}
        public async Task<SlideImageVm> CreateImageSlides(SlideImageVm slideImageVm)
        {
            var slide = new Slide()
            {
                SlideName = slideImageVm.SlideName,
                ImageSlide = slideImageVm.ImageSlide,
                StatusId = slideImageVm.StatusId,
            };
            _context.Slides.Add(slide);
            await _context.SaveChangesAsync();
            return slideImageVm;
        }

        public async Task<BannerImageViewModel> GetBannerSlide()
        {
            BannerImageViewModel listCate = new BannerImageViewModel();
            var sql = @"SELECT * FROM [Slides]";
            try
            {
                using var conn = new SqlConnection(_configuration.Value);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var res = await conn.QueryAsync<SlideImageVm>(sql);
                listCate.SlideImages = (List<SlideImageVm>)res;
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCate;
        }
        public async Task<DataSourceResult> GetBannerSlidePagingFilterAdmin(DataSourceRequest request)
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
                    string sort = KendoApplyFilter.GetSorts<SlideImageVm>(request);
                    result.Data = await conn.QueryAsync<SlideImageVm>("[SLIDES_GetBannerSlidePagingFilterAdmin]",
                        new { @pageSize = request.PageSize, @page = request.Page, @where = where, @orderBy = sort }, commandType: CommandType.StoredProcedure);
                    result.Total = await conn.QueryFirstOrDefaultAsync<int>("[SLIDES_GetBannerSlidePagingFilterAdminTotal]", new { where }, commandType: CommandType.StoredProcedure);
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
