﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AccountParameters : PagingParameters
    {
        public AccountParameters()
        {
            OrderBy = "DateCreated";
        }
    }
}
