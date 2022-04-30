using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaGame
{
    public class Player
    {
        #region Parametri
        public string NomePlayer { get; set; }
        public int PizzeMangiate { get; set; }
        public bool PrimaMano { get; set; } = true;
        public int PizzeMangiateNellaMano { get; set; }
        #endregion

        public Player(string nomePlayer)
        {
            NomePlayer = nomePlayer;
        }

        #region Motodi
        public void SetPizzeNellaMano(int numPizze, bool ritenta = false)
        {
            // caso in cui il numero di pizze da mangiare del player giocante risultano uguali a quelli mangiati dall'avversario
            var str = (ritenta ? "Ritenta, non puoi mangiare lo stesso numero di pizze appena mangiate dall'avversario: " : "") + $"{NomePlayer} mangierà \"{numPizze}\" pizze";

            Utility.ConsoleColorText(str, ConsoleColor.Cyan);
            PizzeMangiateNellaMano = numPizze;
        }
    
        public int SceltaPizzeDaMangiare()
        {
            return Utility.GeneraNumeroPizzeDaMangiarePerGiocatore();
        }

        public void MangiaPizze(Stack<string> PilaPizze, int pizzeDaMangiare)
        {
            if(PilaPizze.Count < pizzeDaMangiare)
                return;

            // resetto le pizze mangiate nella mano precedente
            PizzeMangiateNellaMano = 0;
            PrimaMano = false;

            do
            {
                PilaPizze.Pop();
                PizzeMangiate++;
                PizzeMangiateNellaMano++;
                --pizzeDaMangiare;
            } while (pizzeDaMangiare != 0);
        }
        #endregion
    }
}
