using System.Collections.ObjectModel;
using System.Data.SqlClient;
using User.Model;

namespace User.Services
{
    // The DataService class manages database operations for users
    public class DataService
    {
        // Connection string to the SQL Server database
        private string connectionString = @"Data Source=ILIN-TECHASUS\ILINSQL;Initial Catalog=ADMIN_1;Integrated Security=True";

        public Users ValidateUser(string username, string password)
        {
            // Opens the connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // SQL query to check the username and password
                string query = "SELECT * FROM Users WHERE UsernAME = @username AND uSERpASS = @password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Check if the values are null or empty to avoid errors
                    cmd.Parameters.AddWithValue("@username", string.IsNullOrEmpty(username) ? (object)DBNull.Value : username);
                    cmd.Parameters.AddWithValue("@password", string.IsNullOrEmpty(password) ? (object)DBNull.Value : password);

                    // Executes the query and reads the results
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if a valid result exists
                        if (reader.Read())
                        {
                            // Returns a Users object with the read data
                            return new Users
                            {
                                ID = reader.GetInt32(0),        // User ID
                                UsernAME = reader.GetString(1), // Username
                                uSERpASS = reader.GetString(2), // User password
                                IsAdmin = reader.GetBoolean(3)  // Admin status
                            };
                        }
                        // If no match is found, return null
                        return null;
                    }
                }
            }
        }

        // Method to retrieve all users from the database
        public ObservableCollection<Users> GetAllUsers()
        {
            var users = new ObservableCollection<Users>();

            // Opens the connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // SQL query to select all users
                string query = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Executes the query and reads the results
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Adds each user to the observable collection
                        users.Add(new Users
                        {
                            ID = reader.GetInt32(0),
                            UsernAME = reader.GetString(1),
                            uSERpASS = reader.GetString(2),
                            IsAdmin = reader.GetBoolean(3)
                        });
                    }
                }
            }
            // Returns the list of users
            return users;
        }

        // Method to update an existing user
        public void UpdateUser(Users user)
        {
            // Opens the connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // SQL command to update an existing user
                string query = "UPDATE Users SET UsernAME = @username, uSERpASS = @password, IsAdmin = @isAdmin WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Add parameters to prevent SQL Injection
                cmd.Parameters.AddWithValue("@username", user.UsernAME);
                cmd.Parameters.AddWithValue("@password", user.uSERpASS);
                cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                cmd.Parameters.AddWithValue("@id", user.ID);

                // Executes the update command
                cmd.ExecuteNonQuery();
            }
        }

        // Method to add a new user to the database
        public void AddUser(Users user)
        {
            // Opens the connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // SQL command to insert a new user
                string query = "INSERT INTO Users (UsernAME, uSERpASS, IsAdmin) VALUES (@username, @password, @isAdmin)";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Add parameters to prevent SQL Injection
                cmd.Parameters.AddWithValue("@username", user.UsernAME);
                cmd.Parameters.AddWithValue("@password", user.uSERpASS);
                cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

                // Executes the insert command
                cmd.ExecuteNonQuery();
            }
        }
    }
}
