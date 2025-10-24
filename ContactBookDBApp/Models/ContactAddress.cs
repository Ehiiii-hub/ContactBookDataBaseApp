

namespace ContactBookDBApp.Models
{
    public class ContactAddress
    {
        public int Id { get; set; } 
        public int ContactId { get; set; }
        public string ContactAddress1 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
