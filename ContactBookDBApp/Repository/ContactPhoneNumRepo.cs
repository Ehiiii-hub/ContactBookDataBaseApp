

using ContactBookDBApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace ContactBookDBApp.Repository
{
    public class ContactPhoneNumRepo
    {
        private readonly string ConnectionString = "Data Source=DESKTOP-AHS4PRH;Initial Catalog=ContactBookDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlConnection Con = new SqlConnection();

        public ContactPhoneNumRepo()
        {
            Con = new SqlConnection(ConnectionString);
        }
        public ContactPhoneNum AddPhonenumber(int ContactId,string contactPhoneNumber)
        {
            
           
                Con.Open();
                string Query = "INSERT INTO ContactPhoneNumberTbl(ContactId,ContactPhoneNumber)VALUES(@contactId,@contactPhoneNumber)";
                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@contactId", ContactId);
                cmd.Parameters.AddWithValue("@contactPhoneNumber", contactPhoneNumber);
                cmd.ExecuteNonQuery();
                Con.Close();
               Console.WriteLine("Contact Phone Number Saved!!!");
              ContactPhoneNum phoneNumber = GetPhoneNumberByPhoneNumber(contactPhoneNumber);
               return phoneNumber;
            //return GetPhoneNumberByPhoneNumber(contactPhoneNumber);

        }

        public ContactPhoneNum GetPhoneNumberByPhoneNumber(string contactPhoneNumber)
        {

            List<ContactPhoneNum> phoneNumbers = new List<ContactPhoneNum>();
            Con.Open();
            string Query = $"SELECT Id, ContactId,ContactPhoneNumber FROM ContactPhoneNumberTbl WHERE ContactPhoneNumber= '{contactPhoneNumber}'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ContactPhoneNum phoneNumber = new ContactPhoneNum
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactId = Convert.ToInt32(dr["contactId"].ToString()),
                        PhoneNumber = dr["ContactPhoneNumber"].ToString()
                    };
                    phoneNumbers.Add(phoneNumber);
                }

            }


                 Con.Close();
                return phoneNumbers.FirstOrDefault();
        }




        public List<ContactPhoneNum> GetContactPhoneNumbersByContactId(int contactId)
        {
            List<ContactPhoneNum> contactPhoneNumbers = new List<ContactPhoneNum>();
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId, ContactPhoneNumber FROM ContactPhoneNumberTbl WHERE ContactId={contactId}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ContactPhoneNum contactPhoneNum = new ContactPhoneNum
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactId = Convert.ToInt32(dr["ContactId"].ToString()),
                        PhoneNumber = dr["ContactPhoneNumber"].ToString()
                    }; 
                    contactPhoneNumbers.Add(contactPhoneNum);

                }
            }
            Con.Close();
            return contactPhoneNumbers;
        }
        public void DeleteContactPhoneNumberById(int id)
        {
            Con.Open();
            string SqlQuery = $"DELETE FROM ContactPhoneNumberTbl WHERE Id = '{id}' ";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public void DeleteContactPhoneNumbersByContactId(int contactId)
        {
            Con.Open();
            string SqlQuery = $"DELETE FROM ContactPhoneNumberTbl WHERE ContactId = '{contactId}' ";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.ExecuteNonQuery();            Con.Close();
        }
        public void UpdateContactPhoneNumber(int id, string newPhoneNumber)
        {
            Con.Open();
            string SqlQuery = "UPDATE ContactPhoneNumberTbl SET ContactPhoneNumber=@newPhoneNumber WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@newPhoneNumber", newPhoneNumber);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public void UpdateContactPhoneNumberByContactId(int contactId, string newPhoneNumber)
        {
            Con.Open();
            string SqlQuery = "UPDATE ContactPhoneNumberTbl SET ContactPhoneNumber=@newPhoneNumber WHERE ContactId=@contactId";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@newPhoneNumber", newPhoneNumber);
            cmd.Parameters.AddWithValue("@contactId", contactId);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public void SearchContactPhoneNumber(int id)
        {
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId, ContactPhoneNumber FROM ContactPhoneNumberTbl WHERE Id={id}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine($"ID: {dr["Id"]}, Contact ID: {dr["ContactId"]}, Phone Number: {dr["ContactPhoneNumber"]}");
                }
            }
            else
            {
                Console.WriteLine("No matching phone numbers found.");
            }
            Con.Close();
        }
        public ContactPhoneNum GetContactPhoneNumberById(int id)
        {
            List<ContactPhoneNum> contactPhoneNumbers = new List<ContactPhoneNum>();
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId, ContactPhoneNumber FROM ContactPhoneNumberTbl WHERE Id={id}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ContactPhoneNum contactPhoneNum = new ContactPhoneNum
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactId = Convert.ToInt32(dr["ContactId"].ToString()),
                        PhoneNumber = dr["ContactPhoneNumber"].ToString()
                    };
                    contactPhoneNumbers.Add(contactPhoneNum);
                }
            }
            Con.Close();
            return contactPhoneNumbers.FirstOrDefault();
        }
    }
 
}
    