

using ContactBookDBApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace ContactBookDBApp.Repository
{
    public class ContactAddressRepo
    {
        private readonly string ConnectionString = "Data Source=DESKTOP-AHS4PRH;Initial Catalog=ContactBookDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlConnection Con = new SqlConnection();

        public ContactAddressRepo()
        {
            Con = new SqlConnection(ConnectionString);
        }

        //public static ContactDetailResponse CreateContact(CreateContactRequest contactRequest)
        //{

        //} 


        public ContactAddress AddContactAddress(int contactId, string contactAddress1, string country, string state, string city)
        {

            Con.Open();
            string SqlQuery = @"INSERT
                                INTO
                                     ContactAddressTbl
                                    (ContactId
                                    ,ContactAddress1
                                    ,Country
                                    ,State
                                    ,City)
                            VALUES(
                                     @contactId
                                    ,@contactAddress1
                                    ,@country
                                    ,@state,@city)";


            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@contactId", contactId);
            cmd.Parameters.AddWithValue("@contactAddress1", contactAddress1);
            cmd.Parameters.AddWithValue("@Country", country);
            cmd.Parameters.AddWithValue("@state", state);
            cmd.Parameters.AddWithValue(@"city", city);
            cmd.ExecuteNonQuery();
            Con.Close();
            ContactAddress contactAddress = GetContactAddress(contactAddress1, state, city);
            return contactAddress;
        }

        public ContactAddress GetContactAddress(string contactAddress1,string state, string city)
        {
            List<ContactAddress> ContactAddress = new List<ContactAddress>();
            Con.Open();
            string SqlQuery = @$"SELECT 
                                       Id,ContactId, 
                                       ContactAddress1, 
                                       Country, 
                                       State, 
                                       City 
                                FROM ContactAddressTbl 
                                WHERE ContactAddress1 = '{contactAddress1}' 
                                      AND State = '{state} '
                                      AND City = '{city}'";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ContactAddress newContactAddress = new ContactAddress
                    {

                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactId = Convert.ToInt32(dr["contactId"].ToString()),
                        ContactAddress1 = dr["contactAddress1"].ToString(),
                        Country = dr["Country"].ToString(),
                        State = dr["State"].ToString(),
                        City = dr["City"].ToString()
                    };
                    ContactAddress.Add(newContactAddress);

                }
                //ContactAddress.ContactName = contactName;
            }
            Con.Close();
            return ContactAddress.FirstOrDefault();

        }


        public ContactAddress GetContactAddressById(int id)
        {
            List<ContactAddress> ContactAddressList = new List<ContactAddress>();
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId,ContactAddress1,Country,State,City FROM ContactTbl WHERE Id={id}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ContactAddress ContactAddress = new ContactAddress
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactId = Convert.ToInt32(dr["contactId"].ToString()),
                        ContactAddress1 = dr["ContactAddress1"].ToString(),
                        Country = dr["Country"].ToString(),
                        State = dr["State"].ToString(),

                    };
                    ContactAddressList.Add(ContactAddress);

                }

            }
            Con.Close();
            return ContactAddressList.FirstOrDefault();
        }
        public List<ContactAddress> GetContactAddressesByContactId(int contactId)
        {
            List<ContactAddress> contactAddresses = new List<ContactAddress>();
            Con.Open();
            string SqlQuery = $"SELECT Id, ContactId, ContactAddress1, Country, State, City FROM ContactAddressTbl WHERE ContactId={contactId}";
            SqlDataAdapter sda = new SqlDataAdapter(SqlQuery, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ContactAddress contactAddress = new ContactAddress
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        ContactId = Convert.ToInt32(dr["ContactId"].ToString()),
                        ContactAddress1 = dr["ContactAddress1"].ToString(),
                        Country = dr["Country"].ToString(),
                        State = dr["State"].ToString(),
                        City = dr["City"].ToString()
                    };
                    contactAddresses.Add(contactAddress);
                }
            }
            Con.Close();
            return contactAddresses;
        }





        public void DeleteContactAddress(int id)
        {
            Con.Open();
            string SqlQuery = "DELETE FROM ContactAddressTbl WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void DeleteContactAddressesByContactId(int contactId)
        {
            Con.Open();
            string SqlQuery = "DELETE FROM ContactAddressTbl WHERE ContactId=@contactId";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@contactId", contactId);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public void UpdateContactAddress(int id, string contactAddress1, string city, string state, string country)
        {
            Con.Open();
            string SqlQuery = "UPDATE ContactAddressTbl SET ContactAddress1=@contactAddress1, Country=@country, State=@state, City=@city WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@contactAddress1", contactAddress1);
            cmd.Parameters.AddWithValue("@country", country);
            cmd.Parameters.AddWithValue("@state", state);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        //public void UpdateContactAddressesByContactId(int contactId, string contactAddress1, string country, string state, string city)
        //{
        //    Con.Open();
        //    string SqlQuery = "UPDATE ContactAddressTbl SET ContactAddress1=@contactAddress1, Country=@country, State=@state, City=@city WHERE ContactId=@contactId";
        //    SqlCommand cmd = new SqlCommand(SqlQuery, Con);
        //    cmd.Parameters.AddWithValue("@contactId", contactId);
        //    cmd.Parameters.AddWithValue("@contactAddress1", contactAddress1);
        //    cmd.Parameters.AddWithValue("@country", country);
        //    cmd.Parameters.AddWithValue("@state", state);
        //    cmd.Parameters.AddWithValue("@city", city);
        //    cmd.ExecuteNonQuery();
        //    Con.Close();
        //}

        public void SearchContactAddress(int id)
        {
            Con.Open();
            string SqlQuery = "SELECT Id, ContactId, ContactAddress1, Country, State, City FROM ContactAddressTbl WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader["Id"]}, ContactId: {reader["ContactId"]}, ContactAddress1: {reader["ContactAddress1"]}, Country: {reader["Country"]}, State: {reader["State"]}, City: {reader["City"]}");
            }
            reader.Close();
            Con.Close();
        }

        public void SearchContactAddressesByContactId(int contactId)
        {
            Con.Open();
            string SqlQuery = "SELECT Id, ContactId, ContactAddress1, Country, State, City FROM ContactAddressTbl WHERE ContactId=@contactId";
            SqlCommand cmd = new SqlCommand(SqlQuery, Con);
            cmd.Parameters.AddWithValue("@contactId", contactId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader["Id"]}, ContactId: {reader["ContactId"]}, ContactAddress1: {reader["ContactAddress1"]}, Country: {reader["Country"]}, State: {reader["State"]}, City: {reader["City"]}");
            }
            reader.Close();
            Con.Close();
        }


    }




}
