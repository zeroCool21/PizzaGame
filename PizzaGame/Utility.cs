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
            Continua,               // 0
            VittoriaAvversario,     // 1
            Rigioca,                // 2
            VittoriaGiocante        // 3
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
            Console.WriteLine($"Pizze rimaste: {pizze}\n");
            return pizze;
        }

        public static void ImpilaPizze(Stack<string> PilaPizze, int numMinPizze, int numMaxPizze)
        {
            //la prima pizza inserita è quella generata
            var numPizze = GeneraNumeroPizzeDaImpilare(numMinPizze, numMaxPizze);
            Console.WriteLine($"Sono state generate \"{numPizze}\" pizze, inizio ad impilarle\n");

            for (var i = 1; i < numPizze + 1; i++)
            {
                var txt = $"Pizza {i}";
                txt += (i == 1) ? " <Avvelenata>" : "";

                PilaPizze.Push(txt);
            }

            Console.WriteLine($"Pizze impilate con successo\nI GIOCATORI POSSONO COMINCIARE A GIOCARE\n\n");
        }

        public static void GetPizze(Stack<string> pizze)
        {
            foreach(var pizza in pizze)
                Console.WriteLine(pizza);
        }
    }
}
