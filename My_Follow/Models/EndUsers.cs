using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace My_Follow.Models
{
    public class EndUsers
    {
        public int EndUsersID { get; set; }

        public string Name { get; set; }

        //public int UpdatesID { get; set; }
        //public int ProductID { get; set; }
        public string Email { get; set; }

        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime DateOfJoining { get; set; }

        public string Gender { get; set; }


        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }
        public string City { get; set; }

        public string State { get; set; }
        public string Country { get; set; }

        public string Pin { get; set; }

        public string ContactNumber { get; set; }

       public string Address
        {
            get
            {
                string dspStreet1 = string.IsNullOrWhiteSpace(this.Street1) ? "" : this.Street1;
                string dspStreet2 = string.IsNullOrWhiteSpace(this.Street2) ? "" : this.Street2;
                string dspCity = string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspState = string.IsNullOrWhiteSpace(this.State) ? "" : this.State;
                string dspCountry = string.IsNullOrWhiteSpace(this.Country) ? "" : this.Country;
                string dspPin = string.IsNullOrWhiteSpace(this.Pin) ? "" : this.Pin;
                return string.Format("{0} {1} {2} {3} {4} {5}", dspStreet1, dspStreet2,dspCity,dspState,dspCountry,dspPin);
            }
        }



       public virtual ICollection<Product> Products { get; set; }

     //  public virtual ICollection<Updates> ReceiveUpdates { get; set; }
    }
}