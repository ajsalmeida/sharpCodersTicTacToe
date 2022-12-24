using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace TicTacToe
{
    public class Database
    {
        private const string DATABASE_PATH = $"URI=file:/home/ajsalmeida/RiderProjects/TicTacToe/TicTacToe/test.db";
        private SQLiteConnection _connection;

        public Database()
        {
            Connect();
            //CreateDatabase();
        }

        public void Connect()
        {
            _connection = new SQLiteConnection(DATABASE_PATH);
            _connection.Open();
        }

        public void CreateDatabase()
        {
            using var cmd = new SQLiteCommand(_connection);

            cmd.CommandText = "DROP TABLE IF EXISTS users";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "DROP TABLE IF EXISTS gamehistory";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE users(id INTEGER PRIMARY KEY,name TEXT,score INT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText =
                @"CREATE TABLE gamestory(id INTEGER PRIMARY KEY,player1 TEXT,player2 TEXT,scorePlayer1 INT, scorePlayer2 INT)";
            cmd.ExecuteNonQuery();

        }

        public void CreateUser(string username)
        {
            try
            {
                Console.WriteLine("Criando usuario..." + username);
                using var cmd = new SQLiteCommand(_connection);
                cmd.CommandText = $"INSERT INTO users (name,score) VALUES ({username},{0})";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public bool VerifyUser(string username)
        {
            if (username != "")
            {
                string statement = $"SELECT 1 FROM users WHERE name = {username}";
                using var cmd = new SQLiteCommand(statement, _connection);
                SQLiteDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateScore(string winner, string looser)
        {
            try
            {
                using (var connection = new SQLiteConnection(DATABASE_PATH))
                {
                    string queryWinner = $"UPDATE users SET (score) = score+1000 WHERE name = {winner}";
                    Console.WriteLine(queryWinner);
                    using (var command = new SQLiteCommand(queryWinner, connection))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateHistory(string winner, string looser)
        {
            try
            {
                using (var connection = new SQLiteConnection(DATABASE_PATH))
                {
                    string insertHistory = $"INSERT INTO gamestory (player1,player2,scorePlayer1,scorePlayer2) VALUES ({winner},{looser},{1000},{0})";
                    Console.WriteLine(insertHistory);
                    using (var command = new SQLiteCommand(insertHistory, connection))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void PrintScore(string query)
        {
            SQLiteDataReader reader;
            try
            {
                using (var connection = new SQLiteConnection(DATABASE_PATH))
                {
                    Console.WriteLine();
                    Console.WriteLine("x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x");
                    Console.WriteLine("Ranking de pontuação:");
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Connection.Open();
                        reader = command.ExecuteReader();
                    }

                    while (reader.Read())
                    {

                        Console.WriteLine("____________________________________");
                        Console.WriteLine($"|USUÁRIO:{reader.GetString(0)} | PONTUAÇÃO:{reader.GetInt32(1)}");
                    }

                    Console.WriteLine("x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void PrintHistoric(string query)
        {
            SQLiteDataReader reader;
            try
            {
                using (var connection = new SQLiteConnection(DATABASE_PATH))
                {
                    Console.WriteLine();
                    Console.WriteLine("x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x");
                    Console.WriteLine("Ranking de pontuação:");
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Connection.Open();
                        reader = command.ExecuteReader();
                    }

                    while (reader.Read())
                    {

                        Console.WriteLine("__________________________________________________");
                        Console.WriteLine($"|GANHADOR:{reader.GetString(0)} | PERDEDOR:{reader.GetString(1)} | PONTUAÇÃO DO VENCEDOR {reader.GetInt32(2)} | PONTUAÇÃO DO PERDEDOR:{reader.GetInt32(3)}");
                    }

                    Console.WriteLine("x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x-x");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }

        }
    }
}