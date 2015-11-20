using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace My_Follow.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public int ProductOwnersID { get; set; }

       // public int EndUsersID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Details")]
        public string Details { get; set; }

        public virtual ProductOwners ProductOwners { get; set; }
        public virtual ICollection<Updates> Updates { get; set; }
        public virtual ICollection<EndUsers> EndUsers { get; set; }
    }
}