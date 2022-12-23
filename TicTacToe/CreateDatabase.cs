using System.Data.SQLite;
namespace JogoDaVelha;

public class CreateDatabase
{
    public static void Create()
    {
        try
        {
            string cs = $"URI=file:/home/ajsalmeida/RiderProjects/JogoDaVelha/JogoDaVelha/test.db";
            using var con = new SQLiteConnection(cs);
        
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = "DROP TABLE IF EXISTS users";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "DROP TABLE IF EXISTS gamehistory";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE users(id INTEGER PRIMARY KEY,name TEXT,highestScore INT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE gamestory(id INTEGER PRIMARY KEY,player1 TEXT,player2 TEXT,scorePlayer1 INT, scorePlayer2 INT)";
            cmd.ExecuteNonQuery();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }

}