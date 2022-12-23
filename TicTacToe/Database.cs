using System.Data.SQLite;

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

            cmd.CommandText = @"CREATE TABLE users(id INTEGER PRIMARY KEY,name TEXT,highestScore INT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE gamestory(id INTEGER PRIMARY KEY,player1 TEXT,player2 TEXT,scorePlayer1 INT, scorePlayer2 INT)";
            cmd.ExecuteNonQuery();
        }
        
        public void CreateUser(string username)
        {
            try
            {
                Console.WriteLine("Criando usuario..."+username);
                using var cmd = new SQLiteCommand(_connection);
                cmd.CommandText = $"INSERT INTO users (name,highestscore) VALUES ({username},{0})";
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

        public void UpdateScore()
        {

        }

        public void UpdateHistory()
        {
            
        }

        public void PrintScore()
        {
            string statement = $"SELECT 5 FROM gamestory";
            using var cmd = new SQLiteCommand(statement, _connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            Console.WriteLine(reader);
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetInt32(3)} {reader.GetInt32(4)}");
            }
        }
    }
}