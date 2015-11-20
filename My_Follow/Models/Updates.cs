using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace My_Follow.Models
{
    public class Updates
    {
        public int UpdatesID { get; set; }

     //   public int ProductOwnersID { get; set; }
        public int ProductID { get; set; }

    //    public int EndUsersID { get; set; }

        [Display(Name = "Introduction")]
        public string Intro { get; set; }

        public string Details { get; set; }

        //public string Photo { get; set; }

        public virtual Product Products { get; set; }

     //   public virtual ProductOwners ProductOwners { get; set; }

     //   public virtual EndUsers EndUsers { get; set; }
        public virtual ICollection<Media> Media { get; set; }

    }
}