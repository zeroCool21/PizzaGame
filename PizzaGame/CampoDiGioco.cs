using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaGame
{
    public class CampoDiGioco
    {
        #region Parametri
        private Player Player1 { get; set; }
        private Player Player2 { get; set; }
        public bool FineGioco { get; set; }
        private RegoleGioco RegoleGioco { get; }
        public Stack<string> PilaPizze { get; }
        #endregion

        public CampoDiGioco(Player player1, Player player2, int numMinPizzeDaGenerare, int numMaxPizzeDaGenerare)
        {
            //Struttura LIFO (Last In First Out). Nello stack vengono eseguite tre operazioni di base:
            //Pop: Rimuove e restituisce l'oggetto in cima allo Stack. Gli elementi vengono estratti nell'ordine inverso in cui vengono spinti.
            //Push: Inserisce un oggetto in cima allo Stack.
            //Peek: Restituisce l'oggetto in cima allo Stack senza rimuoverlo.
            PilaPizze = new Stack<string>();

            Player1 = player1;
            Player2 = player2;

            RegoleGioco = new RegoleGioco();

            Utility.ImpilaPizze(PilaPizze, numMinPizzeDaGenerare, numMaxPizzeDaGenerare);
        }


        /// <summary>
        /// Nella giocata possono succedere vari casi:
        /// caso 1: utente avversario non ha ancora giocato - mangio subito
        /// caso 2: scelta uguale dell'aversario -- ripetere
        /// caso 3: scelta diversa dell'avversario -- mangia
        /// </summary>
        /// <param name="playerGiocante"></param>
        /// <param name="playerAvversario"></param>
        public void Go(Player playerGiocante, Player playerAvversario)
        {
            var numPizze = SetPizzeDaMangiare(playerGiocante);

            var stop = RegoleTurno(playerGiocante, playerAvversario);

            if (stop != 0)
                return;

            GiocaMano(playerGiocante, playerAvversario, numPizze);
        }


        #region Azioni
        public void GetScore()
        {
            Console.WriteLine($"Pizze mangiate: <{Player1.NomePlayer} = {Player1.PizzeMangiate}> - <{Player2.NomePlayer} = {Player2.PizzeMangiate}>");
        }

        private int RegoleTurno(Player p1, Player p2)
        {
            switch (RegoleGioco.SaltaTurno(p1, p2, PilaPizze))
            {
                case Utility.OperazioniGioco.VittoriaAvversario: FineGioco = true; return 1;
                case Utility.OperazioniGioco.PizzaAvvelenataMangiata: FineGioco = true; return 1;
                case Utility.OperazioniGioco.Rigioca: Go(p1, p2); break;
                //case Utility.OperazioniGioco.Continua: // non faccio niente, continuo il flusso
            }

            return 0;
        }

        private void Mangia(Player p, int numPizzeDaMangiare)
        {
            p.MangiaPizze(PilaPizze, numPizzeDaMangiare);
            Utility.NumeroDiPizzeRimaste(PilaPizze);
            Utility.GetPizze(PilaPizze);
        }

        private int SetPizzeDaMangiare(Player p, bool ritenta = false)
        {
            // SceltaPizzeDaMangiare - 1, 2 o 3 pizze in modo random
            var numPizze = p.SceltaPizzeDaMangiare();
            p.SetPizzeNellaMano(numPizze, ritenta);

            return numPizze;
        }

        private void GiocaMano(Player p1, Player p2, int numPizze)
        {
            if (!p2.PrimaMano)
            {                
                // L'Avversario ha fatto la prima mano, confronto le giocate, possono accadere i casi 2 e 3
                while (RegoleGioco.Rigioca(p1, p2))
                    numPizze = SetPizzeDaMangiare(p1, true);       // caso 2

                Mangia(p1, numPizze);       // caso 3
            }
            else
                Mangia(p1, numPizze);       // caso 1

        }
        #endregion
    }
}
