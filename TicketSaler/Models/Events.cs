namespace TicketSaler.Models
{
    public class Events
    {
        public Guid EventsId { get; set; }
        public int TicketPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventTime { get; set;}
        public string EventAdress { get; set; }
        public string AgeRating { get; set; }
        public int MaxCapacity { get; set; }
        public int SoldPlace { get; set; }

        public virtual ICollection<UsersEvent> UsersEvents { get; set; }
    }
}
