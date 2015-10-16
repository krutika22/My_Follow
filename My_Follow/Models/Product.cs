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

        //public int EndUsersID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public virtual ICollection<Updates> Updates { get; set; }
     
    }
}