using System.Collections.ObjectModel;
using System.Data.SqlClient;
using User.Model;

namespace User.Services
{
    // Clasa DataService gestionează operațiile cu baza de date pentru utilizatori
    public class DataService
    {
        // String de conexiune la baza de date SQL Server
        private string connectionString = @"Data Source=ILIN-TECHASUS\ILINSQL;Initial Catalog=ADMIN_1;Integrated Security=True";

        public Users ValidateUser(string username, string password)
        {
            // Deschide conexiunea cu baza de date
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Interogare SQL pentru a verifica utilizatorul și parola
                string query = "SELECT * FROM Users WHERE UsernAME = @username AND uSERpASS = @password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Verificare dacă valorile sunt null sau goale pentru a evita erorile
                    cmd.Parameters.AddWithValue("@username", string.IsNullOrEmpty(username) ? (object)DBNull.Value : username);
                    cmd.Parameters.AddWithValue("@password", string.IsNullOrEmpty(password) ? (object)DBNull.Value : password);

                    // Execută interogarea și citește rezultatele
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Verifică dacă există un rezultat valid
                        if (reader.Read())
                        {
                            // Returnează un obiect Users cu datele citite
                            return new Users
                            {
                                ID = reader.GetInt32(0),       // ID-ul utilizatorului
                                UsernAME = reader.GetString(1), // Numele utilizatorului
                                uSERpASS = reader.GetString(2), // Parola utilizatorului
                                IsAdmin = reader.GetBoolean(3)  // Statutul de administrator
                            };
                        }
                        // Dacă nu există nicio potrivire, returnează null
                        return null;
                    }
                }
            }
        }


        // Metodă pentru a obține toți utilizatorii din baza de date
        public ObservableCollection<Users> GetAllUsers()
        {
            var users = new ObservableCollection<Users>();

            // Deschide conexiunea cu baza de date
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Interogare SQL pentru a selecta toți utilizatorii
                string query = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Execută interogarea și citește rezultatele
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Adaugă fiecare utilizator în lista observabilă
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
            // Returnează lista de utilizatori
            return users;
        }

        // Metodă pentru actualizarea unui utilizator existent
        public void UpdateUser(Users user)
        {
            // Deschide conexiunea cu baza de date
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Comandă SQL pentru actualizarea unui utilizator existent
                string query = "UPDATE Users SET UsernAME = @username, uSERpASS = @password, IsAdmin = @isAdmin WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Adăugare parametri pentru prevenirea SQL Injection
                cmd.Parameters.AddWithValue("@username", user.UsernAME);
                cmd.Parameters.AddWithValue("@password", user.uSERpASS);
                cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                cmd.Parameters.AddWithValue("@id", user.ID);

                // Execută comanda de actualizare
                cmd.ExecuteNonQuery();
            }
        }

        // Metodă pentru adăugarea unui nou utilizator în baza de date
        public void AddUser(Users user)
        {
            // Deschide conexiunea cu baza de date
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Comandă SQL pentru adăugarea unui utilizator nou
                string query = "INSERT INTO Users (UsernAME, uSERpASS, IsAdmin) VALUES (@username, @password, @isAdmin)";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Adăugare parametri pentru prevenirea SQL Injection
                cmd.Parameters.AddWithValue("@username", user.UsernAME);
                cmd.Parameters.AddWithValue("@password", user.uSERpASS);
                cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

                // Execută comanda de inserare
                cmd.ExecuteNonQuery();
            }
        }
    }
}
