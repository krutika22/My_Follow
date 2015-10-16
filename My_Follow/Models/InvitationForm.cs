using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace My_Follow.Models
{
    public class InvitationForm
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string CompanyName { get; set; }

        //public string Status { get; set; }
        //[Display(Name = "Is Approved")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        //public bool IsApproved { get; set; }

        //public StatusEnum? Status { get; set; } 

        //public enum StatusEnum
        //{
        //    New = 0,
        //    Pending = 1,
        //    Approved = 2
        //}


        public string Status { get; set; }
        public string Token { get; set; }

       
       
    }
}