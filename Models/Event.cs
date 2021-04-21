using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iSportProjekti.Models
{
    public class Event
    {
        [Key]
        public int EventId {get; set;}

        [Required]
        [Display (Name="Event Name:")]
        public string Events {get; set;}

        [Required]
        [Display (Name="Location Name:")]
        public string Location {get; set;}


        // [DataType(DataType.Date)]
        [FutureDate]
        public DateTime Date {get; set;} 
        

        public int UserId {get; set;}

        public User Planner {get; set;}
        
        public List<UserEvent> UserEvents {get; set;}
        
    }

}