/*
- Adicionar jogadores (com nome)
- Pontuação total (vitórias, empates e derrotas)
- histórico das últimas partidas 
- opção jogar
 */

namespace TicTacToe;

public class Program
{
    static void Main()
    {
        Console.WriteLine("XOXOXOXO  JOGOdaVELHA OXOXOXOX");
        Database database = new();
        Game game = new();
        while (true)
        {
            Console.WriteLine("_____  MENU PRINCIPAL  _____");
            Console.WriteLine("| 1 - Adicionar Jogador     |");
            Console.WriteLine("| 2 - Ver Pontuação Total   |");
            Console.WriteLine("| 3 - Histórico de Partidas |");
            Console.WriteLine("| 4 - Jogar                 |");
            Console.WriteLine("| 0 - Sair                  |");
            Console.WriteLine("|___________________________|");
            Console.Write("Sua escolha: ");
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.Write("Insira o nome do usuário:");
                    string? username = Console.ReadLine();
                    username = string.Format(@"""{0}""", username);
                    if (database.VerifyUser(username))
                    {
                        Console.WriteLine("Usuário já existe.\nNão foi possível adicionar!");
                    }
                    else
                    {
                        database.CreateUser(username);
                    }
                    break;
                case "2":
                    database.PrintScore();
                    break;
                case "3":
                    database.PrintHistoric();
                    break;
                case "4":
                {
                    Console.Clear();
                    Console.Write("Insira usuário do jogador 1: ");
                    string? player1 = Console.ReadLine();
                    player1 = string.Format(@"""{0}""", player1);
                    if (database.VerifyUser(player1))
                    {
                        Console.Write("Insira o usuário do jogador 2: ");
                        string? player2 = Console.ReadLine();
                        player2 = string.Format(@"""{0}""", player2);
                        if (database.VerifyUser(player2) && player1 != player2)
                        {
                            game.PrintGame(player1,player2);
                            game.PlayGame(player1, player2);
                        }
                        else
                        {
                            if (player1 == player2)
                            {
                                Console.WriteLine("Erro! você não pode jogar com você mesmo!");
                            }
                            else
                            {
                                Console.WriteLine("Erro! jogador 2 não cadastrado!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erro! Jogador 1 não cadastrado!");
                    }
                    break;
                }
            }
            if (option == "0") break;
            if (option is null or " ") continue;
        }
    }
}