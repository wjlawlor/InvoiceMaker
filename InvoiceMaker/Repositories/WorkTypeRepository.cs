using InvoiceMaker.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace InvoiceMaker.Repositories
{
    public class WorkTypeRepository
    {
        public WorkTypeRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["InvoiceMaker"].ConnectionString;
        }

        private string _connectionString;

        public List<WorkType> GetWorkTypes()
        {
            List<WorkType> workTypes = new List<WorkType>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        SELECT Id, WorkTypeName, Rate
                        FROM WorkType
                        ORDER BY WorkTypeName
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    decimal rate = reader.GetDecimal(2);
                    WorkType workType = new WorkType(id, name, rate);
                    workTypes.Add(workType);
                }
            }
            return workTypes;
        }

        public void Insert(WorkType workType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        INSERT INTO WorkType(WorkTypeName, Rate)
                        VALUES (@WorkTypeName, @Rate)
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@WorkTypeName", workType.Name);
                command.Parameters.AddWithValue("@Rate", workType.Rate);
                command.ExecuteNonQuery();
            }
        }

        public void Update(WorkType workType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        UPDATE WorkType
                        SET WorkTypeName = @WorkTypeName,
                            Rate = @Rate
                        WHERE Id = @Id
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@WorkTypeName", workType.Name);
                command.Parameters.AddWithValue("@Rate", workType.Rate);
                command.Parameters.AddWithValue("@Id", workType.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(WorkType workType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        DELETE FROM WorkType
                        WHERE Id = @Id
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", workType.Id);
                command.ExecuteNonQuery();
            }
        }

        public WorkType GetById(int id)
        {
            List<WorkType> workTypes = GetWorkTypes();

            foreach (var workType in workTypes)
            {
                if (id == workType.Id)
                {
                    return workType;
                }
            }
            return null;
        }
    }
}
