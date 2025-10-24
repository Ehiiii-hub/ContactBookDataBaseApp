

using ContactBookDBApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace ContactBookDBApp.Repository
{
    public class ContactEmailRepo
    {
        private readonly string ConnectionString = "Data Source=DESKTOP-AHS4PRH;Initial Catalog=ContactBookDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlConnection Con = new SqlConnection();

        public ContactEmailRepo()
        {
            Con = new SqlConnection(ConnectionString);
        }


        public ConEmail AddContactEmail(int contactId, string contactEmail)
        {

            Con.Open();
            string SqlQuery = "INSERT INTO ContactEmailTbl(ContactId,ContactEmail)VALUES(@contactId,@contactEmail)";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@contactId", contactId);
            cmd.Parameters.AddWithValue("@contactEmail", contactEmail);
            cmd.ExecuteNonQuery();
            Con.Close();
            ConEmail Email = GetContactEmailByEmail(contactEmail);
            return Email;
        }

        public ConEmail GetContactEmailByEmail(string contactEmail)
        {
            List<ConEmail> contactEmails = new List<ConEmail>();
            Con.Open();
            string SqlQuery = $"SELECT Id,ContactId, ContactEmail FROM ContactEmailTbl WHERE ContactEmail = '{contactEmail}'";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ConEmail newContactEmail = new ConEmail
                    {

                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactEmail = dr["ContactEmail"].ToString()

                    };
                    contactEmails.Add(newContactEmail);

                }
                //Contacts.ContactName = contactName;
            }
            Con.Close();
            return contactEmails.FirstOrDefault();

        }


        public ConEmail GetContactEmailById(int id)
        {
            List<ConEmail> contactEmailList = new List<ConEmail>();
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId,ContactEmail FROM ContactTbl WHERE Id={id}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ConEmail contactEmail = new ConEmail
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactId = Convert.ToInt32(dr["ContactId"].ToString()),
                        ContactEmail = dr["ContactEmail"].ToString()

                    };
                    contactEmailList.Add(contactEmail);

                }

            }
            Con.Close();
            return contactEmailList.FirstOrDefault();
        }

        public List<ConEmail> GetContactEmailsByContactId(int contactId)
        {
            List<ConEmail> contactEmails = new List<ConEmail>(contactId);
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId, ContactEmail FROM ContactEmailTbl WHERE ContactId={contactId}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ConEmail contactEmail = new ConEmail
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),

                        ContactId = Convert.ToInt32(dr["ContactId"].ToString()),
                        ContactEmail = dr["ContactEmail"].ToString()


                    };
                    contactEmails.Add(contactEmail);
            }   }  
            Con.Close();
            return contactEmails;
        } 




        public string DeleteContactEmail(int id)
        {
            Con.Open();
            string SqlQuery = "DELETE FROM ContactEmailTbl WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Con.Close();
            return "Contact Email Deleted Successfully";
        }


        public string DeleteAllContactEmails(int contactId)
        {
            Con.Open();
            string SqlQuery = "DELETE FROM ContactEmailTbl WHERE ContactId=@contactId";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@contactId", contactId);
            cmd.ExecuteNonQuery();
            Con.Close();
            return "All Contact Emails Deleted Successfully";
        }
        public string UpdateContactEmail(int id, string newContactEmail)
        {
            Con.Open();
            string SqlQuery = "UPDATE ContactEmailTbl SET ContactEmail=@newContactEmail WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@newContactEmail", newContactEmail);
            cmd.ExecuteNonQuery();
            Con.Close();
            return "Contact Email Updated Successfully";
        }   
        
        public void SearchContactEmail(int id)
        {
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId,ContactEmail FROM ContactEmailTbl WHERE Id={id}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine("Contact Email Details:");
                    Console.WriteLine($"Id: {dr["Id"].ToString()}");
                    Console.WriteLine($"ContactId: {dr["ContactId"].ToString()}");
                    Console.WriteLine($"ContactEmail: {dr["ContactEmail"].ToString()}");
                }
            }
            else
            {
                Console.WriteLine("No Contact Email found with the given Id.");
            }
            Con.Close();
        }

        public void SearchAllContactEmails(int contactId)
        {
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId,ContactEmail FROM ContactEmailTbl WHERE ContactId={contactId}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("Contact Emails:");
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine($"Id: {dr["Id"].ToString()}, ContactId: {dr["ContactId"].ToString()}, ContactEmail: {dr["ContactEmail"].ToString()}");
                }
            }
            else
            {
                Console.WriteLine("No Contact Emails found for the given ContactId.");
            }
            Con.Close();
        }
    }
}
