using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaGame
{
    public static class Utility
    {
        /*
         * 0 = non salto il turno, false
         * 1 = salto il turno, avversario vince
         * 2 = rigioco, numero pizze da mangiare = all'avversario
         * 3 = fine, ha vinto il giocante
         */
        public enum OperazioniGioco
        {
            Continua,                       // 0
            VittoriaAvversario,             // 1
            Rigioca,                        // 2
            PizzaAvvelenataMangiata         // 3
        }

        public static int GeneraNumeroPizzeDaImpilare(int low, int up)
        {
            // Punto 2 Fatto
            // Da mettere i bounds in input e leggere tramite args

            // N.B. Il limite inferiore è inclusivo, quello superiore esclusivo
            var lowerBound = low;  //10;

            //var upperBound = Int16.MaxValue;    //max 65536 = 2^16
            var upperBound = up;   //21;

            var random = new Random();
            return random.Next(lowerBound, upperBound);
        }

        public static int GeneraNumeroPizzeDaMangiarePerGiocatore()
        {
            // Punto 3 Fatto

            // N.B. Il limite inferiore è inclusivo, quello superiore esclusivo
            var lowerBound = 1;
            var upperBound = 4;

            var random = new Random();
            return random.Next(lowerBound, upperBound);
        }

        public static int NumeroDiPizzeRimaste(Stack<string> PilaPizze)
        {
            var pizze = PilaPizze.Count;
            Utility.ConsoleColorText($"Pizze rimaste: {pizze}\n", ConsoleColor.Cyan);
            return pizze;
        }

        public static void ImpilaPizze(Stack<string> PilaPizze, int numMinPizze, int numMaxPizze)
        {
            //la prima pizza inserita è quella generata
            var numPizze = GeneraNumeroPizzeDaImpilare(numMinPizze, numMaxPizze);
            Utility.ConsoleColorText($"Sono state generate \"{numPizze}\" pizze, inizio ad impilarle\n", ConsoleColor.Magenta);

            for (var i = 1; i < numPizze + 1; i++)
            {
                var txt = $"Pizza {i}";
                txt += (i == 1) ? " <Avvelenata>" : "";

                PilaPizze.Push(txt);
            }

            Utility.ConsoleColorText($"Pizze impilate con successo\nI GIOCATORI POSSONO COMINCIARE A GIOCARE\n\n", ConsoleColor.Cyan);
        }

        public static void GetPizze(Stack<string> pizze)
        {
            foreach(var pizza in pizze)
                Utility.ConsoleColorText(pizza, ConsoleColor.DarkYellow);
        }

        #region Colore testo console
        public static void ConsoleColorText(string txt, ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.DarkYellow: ConsoleColorTextYellow(txt); break;
                case ConsoleColor.Green: ConsoleColorTextGreen(txt); break;
                case ConsoleColor.Cyan: ConsoleColorTextCyan(txt); break;
                case ConsoleColor.Magenta: ConsoleColorTextMagenta(txt); break;
            }
        }

        public static void ConsoleColorTextYellow(string txt)
        {
            CConsole.WriteLine($"{txt:DarkYellow}");
        }

        public static void ConsoleColorTextGreen(string txt)
        {
            CConsole.WriteLine($"{txt:Green}");
        }

        public static void ConsoleColorTextCyan(string txt)
        {
            CConsole.WriteLine($"{txt:Cyan}");
        }

        public static void ConsoleColorTextMagenta(string txt)
        {
            CConsole.WriteLine($"{txt:Magenta}");
        }
        #endregion
    }
}
