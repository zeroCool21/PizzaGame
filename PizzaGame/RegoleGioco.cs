using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaGame
{
    public class RegoleGioco
    {
        #region Testo Gioco
        /*
         * PizzaGame
         * 
         * Al centro di una stanza è presente un tavolo sopra il quale sono posizionate n di pizze impilate una sopra l'altra.
         * Attenzione, l'ultima pizza di questa pila è avvelenata!!!
         * Il primo giocatore che mangia la pizza avvelenata perde.
         * 
         * 
         * Regole del gioco
         * 
         * 1) I giocatori in gioco devono essere 2 --------------------------------------------------------------------------------> OK
         * 2) Il numero di pizze viene determinato randomicamente all'inizio del gioco e deve essere sempre maggiore di 10  -------> OK
         * 3) Ogni giocatore, durante il proprio turno, deve mangiare 1, 2 o 3 pizze ----------------------------------------------> OK
         * 4) Ogni giocatore non puó saltare il proprio turno ---------------------------------------------------------------------> OK
         * 5) Ogni giocatore non puó ripetere la scelta fatta in precedenza dall'avversario ---------------------------------------> OK
         *      (per esempio: se il giocatore di turno decide di mangiare 1 pizza, 
         *      allora l'altro giocatore è obbligato a mangiare 2 oppure 3 pizze)
         * 6) Se un giocatore durante il proprio turno non ha mosse valide --------------------------------------------------------> OK
         *      (per esempio: rimane ancora 1 pizza sul tavolo e il giocatore 
         *      precedente ha mangiato 1 pizza) allora viene obbligato a 
         *      saltare il proprio turno. A questo punto l'altro giocatore sarà 
         *      obbligato a mangiare la pizza avvelenata e, di conseguenza, a perdere. 
         *      Questo è l'unico caso nel quale è consentito il salto del turno
         * 
         * Esempio
         * 
         * 2 giocatori (A e B)
         * sul tavolo sono presenti 12 pizze
         * inizia il giocatore A
         * il giocatore A decide di mangiare 1 pizza (rimangono 11 pizze sul tavolo)
         * il giocatore B decide di mangiare 3 pizze (rimangono 8 pizze sul tavolo)
         * il giocatore A decide di mangiare 2 pizze (rimangono 6 pizze sul tavolo)
         * il giocatore B decide di mangiare 1 pizza (rimangono 5 pizze sul tavolo)
         * il giocatore A decide di mangiare 3 pizze (rimangono 2 pizze sul tavolo)
         * il giocatore B ha perso. Se mangia 2 pizze perde. Se decide di mangiare 1 pizza, allora il giocatore A è costretto a saltare il turno in quanto non puó applicare le proprie mosse (ovvero non puó mangiare 2 oppure 3 pizze) e costringe il giocatore B a mangiare l'ultima pizza avvelenata
         */
        #endregion

        public bool Rigioca(Player playerDellaMano, Player playerAvversario)
        {
            // Punto 5
            return playerDellaMano.PizzeMangiateNellaMano == playerAvversario.PizzeMangiateNellaMano;
        }

        public Utility.OperazioniGioco SaltaTurno(Player playerGiocante, Player playerAvversario, Stack<string> pilaPizze)
        {
            // caso 4 e 6

            /*
             * 0 = non salto il turno, false
             * 1 = salto il turno, avversario vince
             * 2 = rigioco, numero pizze da mangiare = all'avversario
             * 3 = fine, il giocante ha perso perchè ha mangiato la pizza avvelenata 
             */

            // 1- pizze da mangiare giocante > pizze rimaste e pizze da mangiare giocante != pizze mangiate avversario
            // 2- pizze da mangiare giocante > pizze rimaste e pizze da mangiare giocante == pizze mangiate avversario
            // 3- pizze da mangiare giocante = pizze rimaste e pizze da mangiare giocante != pizze mangiate avversario

            // caso 1
            if (playerGiocante.PizzeMangiateNellaMano > pilaPizze.Count && playerGiocante.PizzeMangiateNellaMano != playerAvversario.PizzeMangiateNellaMano)
            {
                Console.WriteLine($"{playerGiocante.NomePlayer} salta il turno e perde");
                Console.WriteLine($"Complimenti {playerAvversario.NomePlayer}, hai vinto!!");

                return Utility.OperazioniGioco.VittoriaAvversario;
            }

            // caso 2
            if (playerGiocante.PizzeMangiateNellaMano > pilaPizze.Count && playerGiocante.PizzeMangiateNellaMano == playerAvversario.PizzeMangiateNellaMano)
            {                
                Console.WriteLine($"Rigioca, pizze da mangiare uguali a quelli mangiati dell'avversario!!");

                return Utility.OperazioniGioco.Rigioca;
            }

            // caso 3
            if (playerGiocante.PizzeMangiateNellaMano == pilaPizze.Count && playerGiocante.PizzeMangiateNellaMano != playerAvversario.PizzeMangiateNellaMano)
            {
                Console.WriteLine($"{playerGiocante.NomePlayer} ha mangiato la pizza avvelenata e perde, {playerAvversario.NomePlayer} ha vinto, Complimenti!!");

                return Utility.OperazioniGioco.PizzaAvvelenataMangiata;
            }

            return Utility.OperazioniGioco.Continua;
        }
    }
}
