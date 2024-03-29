﻿using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace InvoiceMaker.Repositories
{
    public class WorkDoneRepository
    {
        public WorkDoneRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["InvoiceMaker"].ConnectionString;
        }

        private string _connectionString;

        public List<WorkDone> GetWorkDones()
        {
            List<WorkDone> workDones = new List<WorkDone>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        SELECT WorkDone.Id, WorkDone.ClientId, WorkDone.WorkTypeId, WorkDone.StartedOn,
                            WorkDone.EndedOn, Client.ClientName, Client.IsActivated, WorkType.WorkTypeName, WorkType.Rate
                        FROM WorkDone
                        JOIN Client ON Client.Id = WorkDone.ClientId
                        JOIN WorkType ON WorkType.Id = WorkDone.WorkTypeId
                        ORDER BY WorkDone.Id;
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int Id = reader.GetInt32(0);
                    int ClientId = reader.GetInt32(1);
                    int WorkTypeId = reader.GetInt32(2);
                    DateTimeOffset StartedOn = reader.GetDateTimeOffset(3);
                    DateTimeOffset? EndedOn = null;
                    if (!reader.IsDBNull(4))
                    {
                        EndedOn = reader.GetDateTimeOffset(4);
                    }
                    string ClientName = reader.GetString(5);
                    bool IsActivated = reader.GetBoolean(6);
                    string WorkTypeName = reader.GetString(7);
                    decimal Rate = reader.GetDecimal(8);
                    Client client = new Client(ClientId, ClientName, IsActivated);
                    WorkType workType = new WorkType(WorkTypeId, WorkTypeName, Rate);
                    if (EndedOn.HasValue)
                    {
                        WorkDone workDone = new WorkDone(Id, client, workType, StartedOn, EndedOn.Value);
                        workDones.Add(workDone);
                    }
                    else
                    {
                        WorkDone workDone = new WorkDone(Id, client, workType, StartedOn);
                        workDones.Add(workDone);
                    }
                }
            }
            return workDones;
        }

        public void Insert(WorkDone workDone)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        INSERT INTO WorkDone(ClientId, WorkTypeId, StartedOn)
                        VALUES (@ClientId, @WorkTypeId, @StartedOn)
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ClientId", workDone.ClientId);
                command.Parameters.AddWithValue("@WorkTypeId", workDone.WorkTypeId);
                command.Parameters.AddWithValue("@StartedOn", workDone.StartedOn);
                command.ExecuteNonQuery();
            }
        }

        public void Update(WorkDone workDone)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        UPDATE WorkDone
                        SET ClientId = @ClientId,
                            WorkTypeId = @WorkTypeId,
                            StartedOn = @StartedOn,
                            EndedOn = @EndedOn
                        WHERE Id = @Id
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ClientId", workDone.ClientId);
                command.Parameters.AddWithValue("@WorkTypeId", workDone.WorkTypeId);
                command.Parameters.AddWithValue("@StartedOn", workDone.StartedOn);
                if (workDone.EndedOn == null) { command.Parameters.AddWithValue("@EndedOn", DBNull.Value); }
                    else { command.Parameters.AddWithValue("@EndedOn", workDone.EndedOn); }
                command.Parameters.AddWithValue("@Id", workDone.Id);
                command.ExecuteNonQuery();
            }
        }

        public WorkDone GetById(int id)
        {
            List<WorkDone> workDones = new List<WorkDone>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        SELECT WorkDone.Id, WorkDone.ClientId, WorkDone.WorkTypeId, WorkDone.StartedOn,
                            WorkDone.EndedOn, Client.ClientName, Client.IsActivated, WorkType.WorkTypeName, WorkType.Rate
                        FROM WorkDone
                        JOIN Client ON Client.Id = WorkDone.ClientId
                        JOIN WorkType ON WorkType.Id = WorkDone.WorkTypeId
                        WHERE WorkDone.Id = @Id
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int Id = reader.GetInt32(0);
                    int ClientId = reader.GetInt32(1);
                    int WorkTypeId = reader.GetInt32(2);
                    DateTimeOffset StartedOn = reader.GetDateTimeOffset(3);
                    DateTimeOffset? EndedOn = null;
                    if (!reader.IsDBNull(4))
                    {
                        EndedOn = reader.GetDateTimeOffset(4);
                    }
                    string ClientName = reader.GetString(5);
                    bool IsActivated = reader.GetBoolean(6);
                    string WorkTypeName = reader.GetString(7);
                    decimal Rate = reader.GetDecimal(8);
                    Client client = new Client(ClientId, ClientName, IsActivated);
                    WorkType workType = new WorkType(WorkTypeId, WorkTypeName, Rate);

                    if (EndedOn.HasValue)
                    {
                        return new WorkDone(Id, client, workType, StartedOn, EndedOn.Value);
                    }
                    else
                    {
                        return new WorkDone(Id, client, workType, StartedOn);
                    }
                }
            }
            return null;
        }
    }
}