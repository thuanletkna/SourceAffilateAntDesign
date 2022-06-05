using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel
{
    public class DataSourceResult
    {
        public IEnumerable Data
        {
            get;
            set;
        }

        //
        // Summary:
        //     The total number of records in the original data source. Used for calculating
        //     a pager, for example.
        public int Total
        {
            get;
            set;
        }
    }
}
