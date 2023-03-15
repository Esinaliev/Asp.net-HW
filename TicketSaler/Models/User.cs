namespace TicketSaler.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public string AcsessLevel  { get; set; }

        public virtual ICollection<UsersEvent> UsersEvents { get; set; }
    }
}
