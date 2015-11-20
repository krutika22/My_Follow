using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace My_Follow.Models
{
    public class Media
    {

        public int MediaID { get; set; }


     //   [DisplayName("Select File to Upload")]
        public IEnumerable<HttpPostedFileBase> Images { get; set; }
      //  public string Image { get; set; }

        public string Video { get; set; }
        

    //    public IEnumerable<HttpPostedFileBase> Video { get; set; }

        public int UpdatesID { get; set; }


        public virtual Updates Updates { get; set; }
    }
}