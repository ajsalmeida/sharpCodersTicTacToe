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
        Fill();
    }

    private void Fill()
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
        if (_ticTacToe[linePlayer1 - 1, columnPlayer1 - 1] == "-")
        {
            _ticTacToe[linePlayer1 - 1, columnPlayer1 - 1] = "X";
            UpdateGameProgress();
        }
        PrintGame(_player1,_player2);

    }

    private void Player2Turn()
    {
        Console.WriteLine($"{_player2}, seu símbolo para jogo é O");
        Console.Write($"{_player2} escolhe linha: ");
        int linePlayer2 = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write($"{_player2} escolhe coluna: ");
        int columnPlayer2 = int.Parse(Console.ReadLine() ?? string.Empty);
        if (_ticTacToe[linePlayer2 - 1, columnPlayer2 - 1] == "-")
        {
            _ticTacToe[linePlayer2 - 1, columnPlayer2 - 1] = "O";
            UpdateGameProgress();
            
        }
        PrintGame(_player1,_player2);
    }

    private void UpdateGameProgress()
    {
        _numberOfChoices++;
        if (_numberOfChoices == 10) _gameOver = true;
        if(_numberOfChoices >= 5) VerifyGameWinner();
    }

    private void VerifyGameWinner()
    {
        int xLineCount=0, oLineCount=0;
        int xColumnCount=0, oColumnCount=0;
        int leftDiagonalXCount = 0, leftDiagonalOCount = 0;
        int rightDiagonalXCount = 0, rightDigonalOCount = 0;
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_ticTacToe[i,j] == "X") xLineCount++;
                else if(_ticTacToe[i, j] == "O") oLineCount++;
            }
        }
        
        for (int k = 0; k < 3; k++)
        {
            for (int l = 0; l < 3; l++)
            {
                if(_ticTacToe[l,k] == "X") xColumnCount++;
                else if(_ticTacToe[l, k] == "O") oColumnCount++;
            }
        }

        for (int m = 0; m < 3; m++)
        {
            if (_ticTacToe[m, m] == "X") leftDiagonalXCount++;
            else if(_ticTacToe[m, m] == "O") leftDiagonalOCount++;
        }
        if (xLineCount == 3 || oLineCount == 3) _gameOver = true;
        if (xColumnCount == 3 || oColumnCount == 3) _gameOver = true;
        if (leftDiagonalXCount == 3 || leftDiagonalOCount == 3) _gameOver = true;
        
        /*
            [0,0][0,1][0,2],
            [1,0][1,1][1,2],
            [2,0][2,1][2,2]
        */
        Console.WriteLine("Verifica se alguém ganhou!");
    }
}