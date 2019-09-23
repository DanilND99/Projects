using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsole.Clases
{
    class Player
    {
        string name;
        List<Card> hand;
        List<int> repetidos;
        int nivel;
        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            Repetidos = new List<int>();
            Nivel = 0;
        }
        public string Name { get => name; set => name = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public List<int> Repetidos { get => repetidos; set => repetidos = value; }
        internal List<Card> Hand { get => hand; set => hand = value; }
    }
}
