
namespace ContactBookDBApp.DataTransferObject.RequestObject
{
public class CreateContactRequest
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactAddress1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        
    }
}
