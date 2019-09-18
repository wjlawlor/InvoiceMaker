using InvoiceMaker.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace InvoiceMaker.Repositories
{
    public class ClientRepository
    {
        public ClientRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["InvoiceMaker"].ConnectionString;
        }

        private string _connectionString;

        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        SELECT Id, ClientName, IsActivated
                        FROM Client
                        ORDER BY ClientName
                    ";

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    bool isActivated = reader.GetBoolean(2);

                    Client client = new Client(id, name, isActivated);
                    clients.Add(client);
                }
            }

            return clients;
        }

        public void Insert(Client client)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        INSERT INTO Client (ClientName, IsActivated)
                        VALUES (@ClientName, @IsActivated)
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ClientName", client.Name);
                command.Parameters.AddWithValue("@IsActivated", client.IsActive);
                command.ExecuteNonQuery();
            }
        }

        public void Update(Client client)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        UPDATE Client
                        SET ClientName = @ClientName,
                            IsActivated = @IsActivated
                        WHERE Id = @Id
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ClientName", client.Name);
                command.Parameters.AddWithValue("@IsActivated", client.IsActive);
                command.Parameters.AddWithValue("@Id", client.Id);
                command.ExecuteNonQuery();
            }
        }

        public Client GetById(int id)
        {
            List<Client> clients = GetClients();
           
            foreach (var client in clients)
            {
                if (id == client.Id)
                {
                    return client;
                }
            }

            return null;
        }
    }
}
