

using ContactBookDBApp.DataTransferObject.RequestObject;
using ContactBookDBApp.DataTransferObject.ResponseObject;
using ContactBookDBApp.Models;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;

namespace ContactBookDBApp.Repository
{
    public class ContactRepo
    {
        private readonly string ConnectionString = "Data Source=DESKTOP-AHS4PRH;Initial Catalog=ContactBookDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlConnection Con = new SqlConnection() ;
        ContactEmailRepo _emailRepo;
        ContactPhoneNumRepo _phoneNumber;
        ContactAddressRepo _addressRepo;
        public ContactRepo()
        {
            Con = new SqlConnection(ConnectionString);
            _emailRepo = new ContactEmailRepo();
            _phoneNumber = new ContactPhoneNumRepo();
            _addressRepo = new ContactAddressRepo();

        }

        public  ContactDetailResponse CreateContact(CreateContactRequest contactRequest)
        {
            Contacts newContact = AddContact(contactRequest.ContactName);
            ConEmail contactEmail = _emailRepo.AddContactEmail(newContact.Id, contactRequest.ContactEmail);
            ContactPhoneNum contactPhoneNum = _phoneNumber.AddPhonenumber(newContact.Id, contactRequest.ContactPhone);
            ContactAddress contactAddress = _addressRepo.AddContactAddress(newContact.Id, contactRequest.ContactAddress1, contactRequest.Country, contactRequest.State, contactRequest.City);

            ContactDetailResponse contactDetailResponse = new ContactDetailResponse
            {
                Id = newContact.Id,
                ContactName = newContact.ContactName,
                ContactEmail = new List<ConEmail> { contactEmail },
                ContactPhones = new List<ContactPhoneNum> { contactPhoneNum },
                ContactAddresses = new List<ContactAddress> { contactAddress }
            };
            return contactDetailResponse;
        } 


        public  Contacts AddContact(string contactName)
        {
      
            Con.Open();
            string SqlQuery = "INSERT INTO ContactTbl(ContactName)VALUES(@contactName)";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@contactName", contactName);
            cmd.ExecuteNonQuery();
            Con.Close();

            Contacts contact = GetContactByName(contactName);
            Console.WriteLine("Contact Saved!!!");
            //return GetContactByName(contactName)
            return contact;
        }
       
        
        public Contacts GetContactByName(string contactName)
        {
            List<Contacts> contacts = new List<Contacts>();
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactName FROM ContactTbl WHERE ContactName = '{contactName}'";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows) 
                {
                    Contacts newContact = new Contacts { 
                    
                      Id = Convert.ToInt32(dr["Id"].ToString()),
                      ContactName = dr["ContactName"].ToString()
                    
                    };
                    contacts.Add(newContact);

                }
                //Contacts.ContactName = contactName;
            }
            Con.Close();
            return contacts.FirstOrDefault();

        }


        public Contacts GetContactByContactId(int id)
        {
            List<Contacts> contactsList = new List<Contacts>();
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactName FROM ContactTbl WHERE Id = '{id}'";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery , Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows) 
                {
                    Contacts contacts = new Contacts
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactName = dr["ContactName"].ToString()

                    };
                    contactsList.Add(contacts);

                }

            }
            Con.Close();
            return contactsList.FirstOrDefault();
        }






        public ContactDetailResponse GetContactDetailsById(int contactId)
        {
            Contacts contacts = GetContactByContactId(contactId);
            List<ConEmail> contactEmail = _emailRepo.GetContactEmailsByContactId(contactId);
            List<ContactPhoneNum> contactPhoneNum = _phoneNumber.GetContactPhoneNumbersByContactId(contactId);
            List<ContactAddress> contactAddress = _addressRepo.GetContactAddressesByContactId(contactId);

            ContactDetailResponse contactDetailResponse = new ContactDetailResponse
            {
                Id = contacts.Id,
                ContactName = contacts.ContactName,
                ContactEmail = contactEmail,
                ContactPhones = contactPhoneNum,
                ContactAddresses = contactAddress
            };

            return contactDetailResponse;
        }












        //public Contacts GetContact(string contactName)
        //{
        //    List<Contacts> contactsList = new List<Contacts>();
        //    Con.Open();
        //    string SqlQuery = $"SELECT Id, ContactName FROM ContactTbl WHERE ContactName={contactName}";
        //    SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            Contacts contacts = new Contacts
        //            {
        //                Id = Convert.ToInt32(dr["Id"].ToString()),
        //                ContactName = dr["ContactName"].ToString()
        //            };
        //            contactsList.Add(contacts);
        //        }
        //    }
        //    Con.Close();
        //    return contactsList.FirstOrDefault();
        //}
        public List<Contacts> GetAllContacts()
        {
            List<Contacts> contactsList = new List<Contacts>();
            Con.Open();
            string SqlQuery = "SELECT Id, ContactName FROM ContactTbl";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Contacts contacts = new Contacts
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactName = dr["ContactName"].ToString()
                    };
                    contactsList.Add(contacts);
                }
            }
            Con.Close();
            return contactsList;
        }



        public void DeleteContact(int id)
        {
            Con.Open();
            string SqlQuery = $"DELETE FROM ContactTbl WHERE Id={id}";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public void DeleteAllContacts()
        {
            Con.Open();
            string SqlQuery = "DELETE FROM ContactTbl";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public void UpdateContact(int id, string contactName)
        {
            Con.Open();
            string SqlQuery = "UPDATE ContactTbl SET ContactName=@contactName WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@contactName", contactName);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public List<Contacts> SearchContact(string contactName)
        {
            List<Contacts> contactsList = new List<Contacts>();

            Con.Open();
            string SqlQuery = $"SELECT Id, ContactName FROM ContactTbl WHERE ContactName LIKE '%{contactName}%'";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Contacts contacts = new Contacts
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactName = dr["ContactName"].ToString()
                    };
                    contactsList.Add(contacts);
                          
                }

            }
            Con.Close();
            return contactsList;
        }
        public void SearchContact(int contactId)
        {
            Contacts contact = GetContactByContactId(contactId);
            if (contact != null)
            {
                Console.WriteLine($"Contact Found: ID: {contact.Id}, Name: {contact.ContactName}");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        //public void DisplayAllContacts()
        //{
        //    List<Contacts> contacts = GetAllContacts();
        //    foreach (var contact in contacts)
        //    {
        //        Console.WriteLine($"ID: {contact.Id}, Name: {contact.ContactName}");
        //    }
        //} 

        //public void ViewAllContactDetails()
        //{
        //    List<Contacts> contacts = GetAllContacts();
        //    foreach (var contact in contacts)
        //    {
        //        var contactEmails = _emailRepo.GetContactEmails(contact.Id);
        //        var contactPhoneNumbers = _phoneNumber.GetContactPhoneNumbers(contact.Id);
        //        var contactAddresses = _addressRepo.GetContactAddresses(contact.Id);
        //        Console.WriteLine($"ID: {contact.Id}, Name: {contact.ContactName}");
        //        Console.WriteLine("Emails:");
        //        foreach (var email in contactEmails)
        //        {
        //            Console.WriteLine($"\t{email.ContactEmail}");
        //        }
        //        Console.WriteLine("Phone Numbers:");
        //        foreach (var phone in contactPhoneNumbers)
        //        {
        //            Console.WriteLine($"\t{phone.PhoneNumber}");
        //        }
        //        Console.WriteLine("Addresses:");
        //        foreach (var address in contactAddresses)
        //        {
        //            Console.WriteLine($"\t{address.Address1}, {address.City}, {address.State}, {address.Country}");
        //        }
        //        Console.WriteLine("--------------------------------------------------");
        //    }
        //}



    }

}
