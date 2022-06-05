using AffilateSource.Data.DataEntity;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Status;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Data.Services.Repository
{
    public class StatusServices : IStatusServices
    {
        public readonly ApplicationDbContext context;
        public StatusServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<StatusVm>>GetListSatatus()
        {
            var status =await context.DocStatus.Select(x => new StatusVm()
            {
                StatusId = x.Status,
                StatusName = x.StatusName
            }).ToListAsync();
            return status;
        }
    }
}
