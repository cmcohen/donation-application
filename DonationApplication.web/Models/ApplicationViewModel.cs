﻿using DonationApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonationApplication.web.Models
{
    public class ApplicationViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}