using System.Collections.Generic;
using Telerik.DataSource;

namespace AffilateSource.Shared.ViewModel
{
    public class DataEnvelope<T> : PaginationBase where T : class
    {
        /// <summary>
        /// Use this when there is data grouping
        /// </summary>
        public List<AggregateFunctionsGroup> GroupedData { get; set; }

        /// <summary>
        /// Use this when there is no data grouping and the response is flat data, like in the common case.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Always set this to the total number of records.
        /// </summary>
        public int Total { get; set; }
    }
}
