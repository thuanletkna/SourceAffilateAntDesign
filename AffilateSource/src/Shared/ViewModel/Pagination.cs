﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AffilateSource.Shared.ViewModel
{
    public class Pagination<T> : PaginationBase where T : class
    {



        public List<T> Items { get; set; }



    }
}
