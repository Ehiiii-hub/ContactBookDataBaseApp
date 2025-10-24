

using ContactBookDBApp.DataTransferObject.RequestObject;
using ContactBookDBApp.DataTransferObject.ResponseObject;
using ContactBookDBApp.Models;
using ContactBookDBApp.Repository;

namespace ContactBookDBApp.Presentation
{
    public class ConsoleContactBookApp
    {
        private readonly ContactRepo _contactRepo;
        private readonly ContactEmailRepo _contactEmailRepo;
        private readonly ContactPhoneNumRepo _contactPhoneNumRepo;
        private readonly ContactAddressRepo _contactAddressRepo;

        public ConsoleContactBookApp()
        {
            _contactRepo = new ContactRepo();
            _contactEmailRepo = new ContactEmailRepo();
            _contactPhoneNumRepo = new ContactPhoneNumRepo();
            _contactAddressRepo = new ContactAddressRepo();
        }
        public void DisplayMenu()
        {
            Console.WriteLine("=== Contact Book Application ===");
            Console.WriteLine("1. Add New Contact");
            Console.WriteLine("2. View All Contacts");
            Console.WriteLine("3. Search Contact");
            Console.WriteLine("4. Exit");
            Console.WriteLine("================================");
        }


        public void ViewAllContacts()
        {
            var contacts = _contactRepo.GetAllContacts();

            Console.WriteLine("=== All Contacts ===");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"ID: {contact.Id}, Name: {contact.ContactName}");
            }
            Console.WriteLine("==========END========== \r\n");
            DisplayAllContactMenu();
            ManageDisplayedAllContactMenu();

        }


        private void DisplayAllContactMenu()
        {
            Console.WriteLine("=== Contact Details Menu ===");
            Console.WriteLine("1. View Contact Details");
            Console.WriteLine("2. Delete a Contact");
            Console.WriteLine("3. Clear Screen");
            Console.WriteLine("4. Back to Main Menu");
            Console.WriteLine("=============================");
        }


        private void ManageDisplayedAllContactMenu()
        {
            int detailOption = Utilities.GetIntUserChoice("Select an option (1-4): ", 1, 4);

            if (detailOption == 3)
            {
                Console.Clear();
                return;                         
            }
            else
            {
                switch (detailOption)
                {
                    case 1:
                        int contactId = Utilities.GetIntUserChoice("Enter Contact ID to view details: ", 0, int.MaxValue);
                        DisplayContactDetails(contactId);
                        break;
                    case 2:
                       int contactToDeleteId = Utilities.GetIntUserChoice("Enter Contact ID to delete: ",0,int.MaxValue);
                        ViewContactDetails(contactToDeleteId);
                        string confirmDelete = Utilities.ReadStringUserInput("Are you sure you want to delete this contact? (yes/no): ");
                        if (confirmDelete.ToLower() == "yes")
                        {
                            DeleteContact(contactToDeleteId);

                        }else
                        {
                            return;
                        }
                            break;
                        case 4:
                            Console.Clear();
                            return;
                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
            }
        }












        public void DeleteContact(int contactId)
        {
            _contactRepo.DeleteContact(contactId);
            _contactPhoneNumRepo.DeleteContactPhoneNumbersByContactId(contactId);
            _contactEmailRepo.DeleteContactEmail(contactId);
            _contactAddressRepo.DeleteContactAddress(contactId);

            Console.WriteLine("Contact deleted succesfully");
        }



         public void ViewContactDetails(int contactId)
        {
            ContactDetailResponse contactDetailsResponse = _contactRepo.GetContactDetailsById(contactId);

            Console.WriteLine("=== Contact Details  ===");
            Console.WriteLine($"ID: {contactDetailsResponse.Id}, Name: {contactDetailsResponse.ContactName}");
            Console.WriteLine("=====================");
            Console.WriteLine("=== Contact Phones ===");
            foreach (var phone in contactDetailsResponse.ContactPhones)
            {
                Console.WriteLine($"ID {phone.Id} Contact ID: {phone.ContactId}, Phone Number: {phone.PhoneNumber}");
            }
            Console.WriteLine("=== Contant Emails ===");
            foreach (var email in contactDetailsResponse.ContactEmail)
            {
                Console.WriteLine($"ID: {email.Id}    Contact ID: {email.ContactId}, Email: {email.ContactEmail}");
            }
            Console.WriteLine("=== Contact Addresses ===");
            Console.WriteLine("========================");
            foreach (var address in contactDetailsResponse.ContactAddresses)
            {
                Console.WriteLine($"ID {address.Id} Contact ID: {address.ContactId}, Address: {address.ContactAddress1}, City: {address.City}, State: {address.State}, Country: {address.Country}");
            }
            Console.WriteLine("==============END=========== \r\n");
        }




        public void DisplayContactDetails(int contactId)
        {

            ViewContactDetails(contactId);
            ViewContactDetailsOptionsMenu();
            ManageViewContactDetailsOptionsMenu(contactId);
        }


        public void ViewContactDetailsOptionsMenu()
        {
            Console.WriteLine("1. Update Name");
            Console.WriteLine("2. Update Email");
            Console.WriteLine("3. Update Phone Number");
            Console.WriteLine("4. Update Address");
            Console.WriteLine("5. Delete Phone Number");
            Console.WriteLine("6. Delete Email");
            Console.WriteLine("7. Delete Address");
            Console.WriteLine("8. Add Email");
            Console.WriteLine("9. Add Phone Number");
            Console.WriteLine("10. Add Address");
            Console.WriteLine("11. Back To Main Menu");

        }


        public void ManageViewContactDetailsOptionsMenu(int contactId)
        {
            int detailOption = Utilities.GetIntUserChoice("Select an option (1-11): ", 1, 11);

            if (detailOption == 11)
            {
                Console.Clear();
                return;
            }
            else
            {
                switch (detailOption)
                {
                    case 1:
                        UpdateContactName(contactId);
                        break;
                    case 2:
                        int emailId = Utilities.GetIntUserChoice("Enter Email ID to update Email: ", 0, int.MaxValue);
                        UpdateContactEmail(emailId);
                        break;
                        case 3:
                        int phoneId = Utilities.GetIntUserChoice("Enter Phone ID to update Phone Number: ", 0, int.MaxValue);
                        UpdateContactPhoneNumber(phoneId);
                        break;
                        case 4:
                            int addressId = Utilities.GetIntUserChoice("Enter Address ID to update Address: ", 0, int.MaxValue);
                            UpdateContactAddress(addressId);
                        break;
                        case 5:
                            int delPhoneId = Utilities.GetIntUserChoice("Enter Phone ID to delete Phone Number: ", 0, int.MaxValue);
                            DeleteContactPhoneNumberById(delPhoneId);
                        break;
                        case 6:
                        int delEmailId = Utilities.GetIntUserChoice("Enter Email ID to Delete Email: ", 0, int.MaxValue);
                        DeleteContactEmailById(delEmailId);
                        break;

                        case 7:
                        int delAddressId = Utilities.GetIntUserChoice("Enter Address ID to delete Address: ", 0, int.MaxValue);
                        DeleteContactAddressById(delAddressId);
                        break;
                        case 8:
                            AddContactEmail(contactId);
                        break;
                        case 9:
                            AddContactPhoneNumber(contactId);
                        break;
                        case 10:
                            AddContactAddress(contactId);
                        break;
                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
            }
        }


        public void DisplayContactSearchResults(string contactName)
        {
            Console.Clear();
            List<Contacts> contactsList = _contactRepo.SearchContact(contactName);
            if (contactsList.Count > 0)
            {
                Console.WriteLine("=== Search Results ===");
                foreach (var contact in contactsList)
                {
                    Console.WriteLine($"ID: {contact.Id}, Name: {contact.ContactName}");
                }
                Console.WriteLine("==========End========== \r\n");
                DisplayAllContactMenu();
                ManageDisplayedAllContactMenu();
            }
            else
            {
                Console.WriteLine("No matching contacts found.");
            }




        }










        public void AddContact()
        {
            string name = Utilities.ReadStringUserInput("Enter Contact Name: ");
            string email = Utilities.ReadStringUserInput("Enter Contact Email: ");
            string phoneNumber = Utilities.ReadStringUserInput("Enter Contact Phone Number: ");
            string address = Utilities.ReadStringUserInput("Enter Contact Address1: ");
            string city = Utilities.ReadStringUserInput("Enter City: ");
            string state = Utilities.ReadStringUserInput("Enter State: ");
            string country = Utilities.ReadStringUserInput("Enter Country: ");

            CreateContactRequest contactRequest = new CreateContactRequest
            {
                ContactName = name,
                ContactEmail = email,
                ContactPhone = phoneNumber,
                ContactAddress1 = address,
                City = city,
                State = state,
                Country = country
            };

            _contactRepo.CreateContact(contactRequest);
        }

      
        public void UpdateContactName(int Id1)
        {
            string newName = Utilities.ReadStringUserInput("Enter new Contact Name: ");
            _contactRepo.UpdateContact(Id1, newName);
            Console.WriteLine("Contact Name updated successfully.");
            Console.Clear();
        }

        public void UpdateContactEmail(int Id2)
        {
            string newEmail = Utilities.ReadStringUserInput("Enter new Contact Email: ");
            _contactEmailRepo.UpdateContactEmail(Id2, newEmail);
            Console.WriteLine("Contact Email updated successfully.");
            Console.Clear();
        }
        public void UpdateContactPhoneNumber(int Id3)
        {
            string newPhoneNumber = Utilities.ReadStringUserInput("Enter new Contact Phone Number: ");
            _contactPhoneNumRepo.UpdateContactPhoneNumber(Id3, newPhoneNumber);
            Console.WriteLine("Contact Phone Number updated successfully.");
            Console.Clear();
        }

        public void UpdateContactAddress(int Id4)
        {
            string newAddress = Utilities.ReadStringUserInput("Enter new Contact Address1: ");
            string newCity = Utilities.ReadStringUserInput("Enter new City: ");
            string newState = Utilities.ReadStringUserInput("Enter new State: ");
            string newCountry = Utilities.ReadStringUserInput("Enter new Country: ");
            _contactAddressRepo.UpdateContactAddress(Id4, newAddress, newCity, newState, newCountry);
            Console.WriteLine("Contact Address updated successfully.");
            Console.Clear();

        }

  

        public void DeleteContactPhoneNumberById(int Id)
        {
            _contactPhoneNumRepo.DeleteContactPhoneNumberById(Id);
            Console.WriteLine("Contact Phone Number deleted successfully.");
        }

        public void DeleteContactEmailById(int Id)
        {
            _contactEmailRepo.DeleteContactEmail(Id);
            Console.WriteLine("Contact Email deleted successfully.");

        }
        
        public void DeleteContactAddressById(int Id)
        {
            _contactAddressRepo.DeleteContactAddress(Id);
            Console.WriteLine("Contact Address deleted successfully.");
        }




        public void AddContactEmail(int contactId)
        {
            string contactEmail = Utilities.ReadStringUserInput("Enter Contact Email: ");
            _contactEmailRepo.AddContactEmail(contactId, contactEmail);
            Console.WriteLine("Contact Email added successfully.");
            Console.Clear();
        }


        public void AddContactPhoneNumber(int contactId)
        {
            string contactPhoneNumber = Utilities.ReadStringUserInput("Enter Contact Phone Number: ");
            _contactPhoneNumRepo.AddPhonenumber(contactId, contactPhoneNumber);
            Console.WriteLine("Contact Phone Number added successfully.");
            Console.Clear();
        }

        public void AddContactAddress(int contactId)
        {
            string contactAddress1 = Utilities.ReadStringUserInput("Enter Contact Address1: ");
            string city = Utilities.ReadStringUserInput("Enter City: ");
            string state = Utilities.ReadStringUserInput("Enter State: ");
            string country = Utilities.ReadStringUserInput("Enter Country: ");
            _contactAddressRepo.AddContactAddress(contactId, contactAddress1, city, state, country);
            Console.WriteLine("Contact Address added successfully.");
            Console.Clear();
        }

        public void Run()
        {
            bool exit = false;
            while (exit == false)
            {
                DisplayMenu();
                int choice = Utilities.GetIntUserChoice("Select an option (1-4): ", 1, 4);
                switch (choice)
                {
                    case 1:
                        AddContact();
                        break;
                    case 2:
                        ViewAllContacts();
                        break;
                    case 3:
                        //int contactId = Utilities.GetIntUserChoice("Enter Contact Id to search: ", 0, int.MaxValue);
                        string contactName = Utilities.ReadStringUserInput("Enter Contact Name to search: ");
                        DisplayContactSearchResults(contactName);

                        //_contactRepo.SearchContact(contactName);
                        break;
                    case 4:
                        exit = true;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }



        

        //public void SearchContact()
        //{
        //    int contactId = Utilities.GetIntUserChoice("Enter Contact Id to search: ", 0, int.MaxValue);
        //    var contact = _contactRepo.GetContactByContactId(contactId);
        //    if (contact != null)
        //    {
        //        Console.WriteLine($"Contact Found: ID: {contact.Id}, Name: {contact.ContactName}");
        //        //string updateChoice = Utilities.ReadStringUserInput("Do you want to update contact info? (yes/no): ");
        //        //if (updateChoice.ToLower() == "yes")
        //        //{
        //        //    DisplayContactDetails(contactId);
        //        //}
        //    }
        //    else
        //    {
        //        Console.WriteLine("Contact not found.");
        //    }
        //}



        //public void SearchContact(int contactId, string contactName)
        //{
        //     int ContactId = Utilities.GetIntUserChoice("Enter Contact Id to search: ",0, int.MaxValue);
        //    _contactRepo.SearchContact(contactId);
        //    if (ContactId == contactId )
        //    {
        //       Console.WriteLine($"Contact Found: ID: {contactId}, Name: {contactName}");
        //        //Console.WriteLine("1. Update Contact  Info");
        //       string UpdateChoice = Utilities.ReadStringUserInput("Do you want to update contact info? (yes/no): ");
        //        if (UpdateChoice.ToLower() == "yes")
        //        {

        //            OptionsMenu();
        //            int option = Utilities.GetIntUserChoice("Select an option to update (1-4): ", 1, 4);
        //            switch (option)
        //            {
        //                    case 1:
        //                    UpdateContactName();
        //                    break;
        //                    case 2:
        //                    UpdateContactEmail();
        //                    break;
        //                    case 3:
        //                    UpdateContactPhoneNumber();
        //                    break;
        //                    case 4:
        //                    UpdateContactAddress();
        //                    break;
        //                    default:
        //                    Console.WriteLine("Invalid option selected.");
        //                    break;
        //            }
        //        }

        //    }
        //}

    }
}
