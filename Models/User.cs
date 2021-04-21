using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSportProjekti.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set;}

        [Required]
        [Display (Name="Name:")]
        [MinLength(2,ErrorMessage="Name must be at least 2 characters!")]
        public string Name {get; set;}


        [Required]
        [EmailAddress]
        [Display (Name="Email:")]
        public string Email {get; set;}

        [Required]
        [MinLength(8,ErrorMessage="Password must be at least 8 characters!")]
        [DataType(DataType.Password)]
        [Display (Name="Password:")]
        public string Password {get; set;}


        [DataType(DataType.Date)]
        [Required]
        public string Birthdate {get; set;}  

        public List<Event> EventsPlanned {get; set;} 
    }

    public class Login{
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string EmailAttempt {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string PasswordAttempt {get; set;}
    }
}