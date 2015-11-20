using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My_Follow.ViewModel
{
    public class FollowedProducts
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public bool Followed { get; set; }
    }
}