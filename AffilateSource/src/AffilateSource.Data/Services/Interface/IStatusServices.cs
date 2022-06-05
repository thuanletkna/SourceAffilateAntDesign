using AffilateSource.Shared.ViewModel.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Data.Services.Interface
{
    public interface IStatusServices
    {
        Task<List<StatusVm>> GetListSatatus();
    }
}
