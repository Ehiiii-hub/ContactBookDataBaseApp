

using ContactBookDBApp.Models;

namespace ContactBookDBApp.DataTransferObject.ResponseObject
{
    public class ContactDetailResponse
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public List<ConEmail> ContactEmail {  get; set; }
        public List<ContactPhoneNum> ContactPhones { get; set; }
        public List<ContactAddress> ContactAddresses { get; set; }
    }
}
