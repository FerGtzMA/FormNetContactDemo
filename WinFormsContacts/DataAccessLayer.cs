using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    // Aqui tendremos la concexion a la BD
    internal class DataAccessLayer
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinFormsContacts;Data Source=MiLap");

        public void InsertContact(Contact contact)
        {
            try
            {
                conn.Open();
                // El '@' permite escribir un string de mas de una linea
                string query = @"
                                    INSERT INTO Contacts(FirstName, LastName, Phone, Adress)
                                    VALUES (@FirstName, @LastName, @Phone, @Adress)";
                SqlParameter firstName = new SqlParameter();
                firstName.ParameterName = "@FirstName";
                firstName.Value = contact.FirstName;
                firstName.DbType = System.Data.DbType.String;

                /*
                SqlParameter lastName = new SqlParameter();
                lastName.ParameterName = "@LastName";
                lastName.Value = contact.LastName;
                lastName.DbType = System.Data.DbType.String;

                SqlParameter phone = new SqlParameter();
                phone.ParameterName = "@Phone";
                phone.Value = contact.Phone;
                phone.DbType = System.Data.DbType.String;

                SqlParameter adress = new SqlParameter();
                adress.ParameterName = "@Adress";
                adress.Value = contact.Address;
                adress.DbType = System.Data.DbType.String;
                */

                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Adress", contact.Address);

                SqlCommand sqlCommand = new SqlCommand(query, conn);
                sqlCommand.Parameters.Add(firstName);
                sqlCommand.Parameters.Add(lastName);
                sqlCommand.Parameters.Add(phone);
                sqlCommand.Parameters.Add(address);

                // Devuelve la cantidad de filas afectadas
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
            finally 
            { 
                conn.Close(); 
            }
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @"   UPDATE Contacts 
                                    SET FirstName = @FirstName, 
                                    LastName = @LastName, 
                                    Phone = @Phone, 
                                    Adress = @Adress 
                                    WHERE Id = @Id";
                SqlParameter id = new SqlParameter("@Id", contact.Id);
                SqlParameter FirstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Adress", contact.Address);

                // no importa el orden, mientras esten y coincidan, todo bien
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(FirstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"Delete FROM Contacts WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, conn); 
                command.Parameters.Add(new SqlParameter("@Id", id));

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }finally { conn.Close(); }
        }

        public List<Contact> GetContacts(string searchText = null)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                conn.Open();
                String query = @"SELECT Id, FirstName, LastName, Phone, Adress FROM Contacts ";

                SqlCommand command = new SqlCommand();

                // Si no es nulo
                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " WHERE FirstName LIKE @Search OR LastName LIKE @Search OR Phone LIKE @Search OR Adress LIKE @Search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{searchText}%"));
                }

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contacts.Add(new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Adress"].ToString(),
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally {  conn.Close(); }

            return contacts;
        }
    }

}
