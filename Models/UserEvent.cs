namespace iSportProjekti.Models
{
    public class UserEvent
    {  
        public string UserEventId {get; set;}
        public int UserId {get; set;}
        public int EventId {get; set;}
        public User Participants {get; set;}        
    }
}

