using My_Follow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My_Follow.ViewModel
{
    public class EndUsersIndexData
    {
        public IEnumerable<EndUsers> EndUsers { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Updates> Updates { get; set; }

        public IEnumerable<ProductOwners> ProductOwners { get; set; }
    }
}