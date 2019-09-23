using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsole.Clases
{
    class Card
    {
        byte value;
        byte shape;
        public Card(byte value, byte shape)
        {
            Value = value;
            Shape = shape;
        }

        public byte Value { get => value; set => this.value = value; }
        public byte Shape { get => shape; set => shape = value; }
    }
}
