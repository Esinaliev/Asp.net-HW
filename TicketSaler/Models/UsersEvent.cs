namespace TicketSaler.Models
{
    public class UsersEvent
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Events Events { get; set; }
        public Guid EventsId { get; set; }

    }
}