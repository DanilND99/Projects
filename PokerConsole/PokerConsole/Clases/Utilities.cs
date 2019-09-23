using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsole.Clases
{
    class Utilities
    {
        public List<string> palos;
        public List<string> valores;
        public List<string> niveles;
        public Utilities()
        {//Valores de las cartas dependiendo de su byte
            palos = new List<string> { "Corazón", "Rombo", "Trébol", "Pica" };
            niveles = new List<string> { "","Carta Alta", "Par", "Dos Pares", "Tercia","Escalera","Color","Full House","Poker","Escalera Real" };
            valores = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        }
        public string getPalo(byte index)
        {
            return palos[index];
        }
        public string getValor(byte index)
        {
            return valores[index];
        }
        public string getNivel(int index)
        {
            return niveles[index];
        }
    }
}
