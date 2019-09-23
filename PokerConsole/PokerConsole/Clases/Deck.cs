using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsole.Clases
{
    class Deck
    {
        public Stack<Card> mazo;
        public List<Card> deadDeck;
        public Utilities ut;
        public Deck()
        {
            mazo = new Stack<Card>();
            deadDeck = new List<Card>();
            ut = new Utilities();
            for (byte i = 0; i < 4; ++i)
            {//Crea el deck de cartas
                for (byte j = 0; j < 13; ++j)
                {
                    mazo.Push(new Card(j, i));
                }
            }
        }
        public void Shuffle()
        {
            Random num = new Random();
            bool flag = false;
            int tam = mazo.Count;
            for (int i = 0;i < tam; ++i)
            {//Manda todas las cartas que sobran a la pila de descartadas
                deadDeck.Add(mazo.Pop());
            }
            for (int i = 0; i < 52; ++i)
            {//Acomoda el Deck aleatoriamente
                do
                {
                    int x = num.Next(0, 52);
                    if (deadDeck[x] != null)
                    {
                        mazo.Push(deadDeck[x]);
                        deadDeck[x] = null;
                        flag = true;
                    }
                } while (!flag);
                flag = false;
            }
            for (int i = 51; i >= 0; --i)
            {
                deadDeck.Remove(deadDeck[i]);
            }
        }
    }
}
