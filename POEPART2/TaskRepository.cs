using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace POEPART2
{
    internal class TaskRepository
    {
        private readonly string connectionString = "Server=localhost;Database=cybersafe;Uid=root;Pwd=12345;";

        public int AddTask(string title, string description, DateTime? reminderDate)    //function to add a new task to the database, returns the new task's ID
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO tasks (Title, Description, ReminderDate, IsCompleted) 
                                  VALUES (@title, @description, @reminderDate, 0);
                                  SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@reminderDate", reminderDate ?? (object)DBNull.Value);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void SetReminder(int taskId, DateTime reminderDate)  //function for setting the reminder date for a task in the database
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE tasks SET ReminderDate = @reminderDate WHERE Id = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@reminderDate", reminderDate);
                    command.Parameters.AddWithValue("@id", taskId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<TaskItem> GetAllTasks() // function to retrive all tasks from the database
        {
            var tasks = new List<TaskItem>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Title, Description, ReminderDate, IsCompleted, CreatedAt FROM tasks ORDER BY CreatedAt DESC";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TaskItem
                        {
                            Id = reader.GetInt32("Id"),
                            Title = reader.GetString("Title"),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                            ReminderDate = reader.IsDBNull(reader.GetOrdinal("ReminderDate")) ? (DateTime?)null : reader.GetDateTime("ReminderDate"),
                            IsCompleted = reader.GetBoolean("IsCompleted"),
                            CreatedAt = reader.GetDateTime("CreatedAt")
                        });
                    }
                }
            }

            return tasks;
        }

        public bool MarkTaskCompleted(int taskId)   //function to mark a task as completed in the database, returns true if the update was successful
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE tasks SET IsCompleted = 1 WHERE Id = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", taskId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteTask(int taskId) //function to delete a task from the database, returns true if the deletion was successful
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM tasks WHERE Id = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", taskId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public TaskItem GetTaskById(int taskId) //function to retrieve a specific task from the database by its ID, returns a TaskItem object if found, otherwise null
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Title, Description, ReminderDate, IsCompleted, CreatedAt FROM tasks WHERE Id = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", taskId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TaskItem
                            {
                                Id = reader.GetInt32("Id"),
                                Title = reader.GetString("Title"),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                                ReminderDate = reader.IsDBNull(reader.GetOrdinal("ReminderDate")) ? (DateTime?)null : reader.GetDateTime("ReminderDate"),
                                IsCompleted = reader.GetBoolean("IsCompleted"),
                                CreatedAt = reader.GetDateTime("CreatedAt")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}