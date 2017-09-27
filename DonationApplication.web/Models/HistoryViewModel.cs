using DonationApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonationApplication.web.Models
{
    public class HistoryViewModel
    {
        public IEnumerable<Application> Applications { get; set; }
        public User User { get; set; }
    }
}