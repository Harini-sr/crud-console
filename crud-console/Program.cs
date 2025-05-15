using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


class Program
{
    static string connectionString = "Server=localhost;Database=sqlCrud;User Id=root;Password=harini1020;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nChoose an operation:");
            Console.WriteLine("1. Create Record");
            Console.WriteLine("2. Read Records");
            Console.WriteLine("3. Update Record");
            Console.WriteLine("4. Delete Record");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input! Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Age: ");
                    if (!int.TryParse(Console.ReadLine(), out int age))
                    {
                        Console.WriteLine("Invalid age input!");
                        continue;
                    }
                    CreateRecord(name, age);
                    break;
                case 2:
                    ReadRecords();
                    break;
                case 3:
                    Console.Write("Enter ID to update: ");
                    if (!int.TryParse(Console.ReadLine(), out int updateId))
                    {
                        Console.WriteLine("Invalid ID input!");
                        continue;
                    }
                    Console.Write("Enter New Name: ");
                    string newName = Console.ReadLine();
                    Console.Write("Enter New Age: ");
                    if (!int.TryParse(Console.ReadLine(), out int newAge))
                    {
                        Console.WriteLine("Invalid age input!");
                        continue;
                    }
                    UpdateRecord(updateId, newName, newAge);
                    break;
                case 4:
                    Console.Write("Enter ID to delete: ");
                    if (!int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        Console.WriteLine("Invalid ID input!");
                        continue;
                    }
                    DeleteRecord(deleteId);
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void CreateRecord(string name, int age)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Age", age);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    static void ReadRecords()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM Users";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("\nID | Name | Age");
                Console.WriteLine("----------------");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ID"]} | {reader["Name"]} | {reader["Age"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    static void UpdateRecord(int id, string newName, int newAge)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = "UPDATE Users SET Name = @Name, Age = @Age WHERE ID = @ID";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Name", newName);
            cmd.Parameters.AddWithValue("@Age", newAge);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    static void DeleteRecord(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = "DELETE FROM Users WHERE ID = @ID";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
