// See https://aka.ms/new-CConsole-template for more information

using PizzaGame;
using System.Text.RegularExpressions;

args = new string[] { "12", "24" };

if (args == null || args.Length == 0)
{
    Utility.ConsoleColorText($"args is null:yellow", ConsoleColor.Magenta); // Check for null array
}
else
{
    try
    {
        var rx = new Regex(@"^\d+$");
        var result = rx.IsMatch(args[0]) && rx.IsMatch(args[1]);

        if (result)
        {
            var valMin = Convert.ToInt32(args[0]);
            var valMax = Convert.ToInt32(args[1]);

            var player1 = new Player("Gianluca");
            var player2 = new Player("Mariachiara");

            // per accedere ad args da qualsiasi parte del codice
            //string name = Environment.GetCommandLineArgs()[1];

            var campo = new CampoDiGioco(player1, player2, valMin, valMax);

            while (!campo.FineGioco)
            {
                campo.Go(player1, player2);
                campo.GetScore();

                var tmp = player1;
                player1 = player2;
                player2 = tmp;

                Console.WriteLine($"----------------------------------------------");
            }
        }
        else
        {
            Utility.ConsoleColorText($"Input array args invalid", ConsoleColor.Magenta);
        }
    }
    catch (Exception ex)
    {
        Utility.ConsoleColorText("Errore imprevisto!!!", ConsoleColor.Magenta);
        Utility.ConsoleColorText(ex.Message, ConsoleColor.Magenta);
    }
}
