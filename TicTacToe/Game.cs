using System.Xml;

namespace TicTacToe;

public class Game
{
    private static string[,] _ticTacToe = new string[3, 3];
    private bool _gameOver;
    private int _numberOfChoices = 1;
    private string _player1;
    private string _player2;
    
    public Game()
    {
        Reset();
    }

    private void Reset()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _ticTacToe[i, j] = "-";
            }
        }
    }
    public void PrintGame(string player1, string player2)
    {
        Console.WriteLine($"{player1} |VS| {player2}");
        int lineCounter = 1;
        Console.WriteLine("   1 | 2 | 3 |");
        for (int k = 0; k < 3; k++)
        {
            Console.Write($"{lineCounter}  ");
            //Console.Write("| ");
            for (int l = 0; l < 3; l++)
            {
                Console.Write($"{_ticTacToe[k, l]} | ");
            }

            Console.Write($"\n  ___|___|___\n");
            lineCounter++;
        }
    }

    public void PlayGame(string playerOne, string playerTwo)
    {
        _player1 = playerOne;
        _player2 = playerTwo;

        while (true)
        {
            if (_gameOver) break;
            if(_numberOfChoices % 2 == 0)  Player2Turn();
            else Player1Turn();
        }
        

    }

    private void Player1Turn()
    {
        Console.WriteLine($"{_player1}, seu símbolo para jogo é X");
        Console.Write($"{_player1} escolhe linha:");
        int linePlayer1 = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write($"{_player1} escolhe coluna:");
        int columnPlayer1 = int.Parse(Console.ReadLine() ?? string.Empty);
        
        while (linePlayer1 is < 1 or > 3 || columnPlayer1 is < 1 or > 3)
        {
            Console.WriteLine("Erro! Insira uma posição válida!");
            Console.Write($"{_player1} escolhe linha:");
            linePlayer1 = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write($"{_player1} escolhe coluna:");
            columnPlayer1 = int.Parse(Console.ReadLine() ?? string.Empty);
        }
        
        if (_ticTacToe[linePlayer1 - 1, columnPlayer1 - 1] == "-")
        {
            _ticTacToe[linePlayer1 - 1, columnPlayer1 - 1] = "X";
            UpdateGameProgress();
        }
        

        Console.Clear();
        PrintGame(_player1,_player2);

    }

    private void Player2Turn()
    {
        Console.WriteLine($"{_player2}, seu símbolo para jogo é O");
        Console.Write($"{_player2} escolhe linha: ");
        int linePlayer2 = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write($"{_player2} escolhe coluna: ");
        int columnPlayer2 = int.Parse(Console.ReadLine() ?? string.Empty);
        
        while (linePlayer2 is < 1 or > 3 || columnPlayer2 is < 1 or > 3)
        {
            Console.WriteLine("Erro! Insira uma posição válida!");
            Console.Write($"{_player2} escolhe linha:");
            linePlayer2 = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write($"{_player2} escolhe coluna:");
            columnPlayer2 = int.Parse(Console.ReadLine() ?? string.Empty);
        }
        
        if (_ticTacToe[linePlayer2 - 1, columnPlayer2 - 1] == "-")
        {
            _ticTacToe[linePlayer2 - 1, columnPlayer2 - 1] = "O";
            UpdateGameProgress();
            
        }
        Console.Clear();
        PrintGame(_player1, _player2);
    }

    private void UpdateGameProgress()
    {
        _numberOfChoices++;
        if(_numberOfChoices >= 6) VerifyGameWinner();
    }

    private void VerifyGameWinner()
    {
        CheckLines();
        CheckColumns();
        CheckLeftDiagonal();
        CheckRightDiagonal();
    }

    private void CheckLines()
    {
        int xCount = 0;
        int oCount = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_ticTacToe[i, j] == "X") xCount++;
                else if (_ticTacToe[i, j] == "O") oCount++;
            }

            if (xCount == 3) WriteHistory(_player1, _player2);
            else if (oCount == 3) WriteHistory(_player2, _player1);
            xCount = 0;
            oCount = 0;
        }
    }

    private void CheckColumns()
    {
        int xCount = 0;
        int oCount = 0;
        for (int k = 0; k < 3; k++)
        {
            for (int l = 0; l < 3; l++)
            {
                if(_ticTacToe[l,k] == "X") xCount++;
                else if(_ticTacToe[l,k] == "O") oCount++;
            }
            if (xCount == 3) WriteHistory(_player1, _player2);
            else if (oCount == 3) WriteHistory(_player2, _player1);
            xCount = 0;
            oCount = 0;
        }
    }

    private void CheckLeftDiagonal()
    {
        int xCount = 0;
        int oCount = 0;
        
        for (int m = 0; m < 3; m++)
        {
            if (_ticTacToe[m, m] == "X") xCount++;
            else if(_ticTacToe[m, m] == "O") oCount++;
        }
        if (xCount == 3) WriteHistory(_player1, _player2);
        else if (oCount == 3) WriteHistory(_player2, _player1);
        
    }

    private void CheckRightDiagonal()
    {
        int xCount = 0;
        int oCount = 0;
        
        int rightDiagonalColumnsCounter = 2;
        for (int n = 0; n < 3; n++)
        {
            if (_ticTacToe[n, rightDiagonalColumnsCounter] == "X") xCount++;
            else if (_ticTacToe[n, rightDiagonalColumnsCounter] == "O") oCount++;
            rightDiagonalColumnsCounter--;
        }
        if (xCount == 3) WriteHistory(_player1, _player2);
        else if (oCount == 3) WriteHistory(_player2, _player1);
    }


    private void WriteHistory(string winner, string looser)
    {
        Console.WriteLine($"{winner} ganhou!");
        System.Threading.Thread.Sleep(5000);
        
        Database database = new();
        database.UpdateScore(winner, looser);
        database.UpdateHistory(winner,looser);
        PrintGame(_player1, _player2);
        System.Threading.Thread.Sleep(5000);
        Reset();
        _gameOver = true;
        
    }
}