using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsole.Clases
{
    class Dealer
    {
        public Queue<Player> jugadores;
        public Player jugadorActivo;
        public Deck deck;
        public Dealer()
        {
            jugadores = new Queue<Player>();
            deck = new Deck();
            deck.Shuffle();
        }
        public List<Player> Reiniciar(List<Player> reset)
        {
            Shuffle();
            foreach (Player jugador in jugadores)
            {
                jugador.Hand.RemoveRange(0, jugador.Hand.Count);
            }
            reset.RemoveRange(0, reset.Count);
            return reset;
        }
        public void meetPlayers()
        {
            for (int i = 0; i < 2; ++i)//Cambiar el numero en i < X con los jugadores que quieras hasta 6
            {//Crea los 4 jugadores de la partida
                Console.WriteLine("Escribe tu nombre");
                jugadores.Enqueue(new Player(Console.ReadLine()));
            }
        }
        public void Shuffle()
        {
            deck.Shuffle();
        }
        public void Repartir()
        {
            for (int i = 0; i < 5; ++i)
            {
                foreach (Player jugador in jugadores)
                {
                    deck.deadDeck.Add(deck.mazo.Peek());
                    jugador.Hand.Add(deck.mazo.Pop());
                }
            }
        }
        public void tirarCarta(int indice)
        {
            jugadorActivo.Hand.RemoveAt(indice);
        }
        public void turno()
        {
            jugadorActivo = jugadores.Dequeue();
            Console.WriteLine("\nTurno de {0}",jugadorActivo.Name);
            mostrarCartas(jugadorActivo);
            Console.WriteLine("\nDeseas tirar alguna carta\n1. Si\n2. No");
            int accion = 0;//Pregunta si el jugador desea tirar algo de su mano
            bool flag = false;
            do
            {
                try
                {
                    accion = int.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (Exception)
                {//Evita la introducción de letras
                    Console.WriteLine("Error por favor introduzca un número");
                }
            }while(!flag);
            if (accion == 1)
            {
                Console.WriteLine("\nCuantas cartas vas a tirar (Maximo 3)");
                int max = -1;
                do
                {//Evita la introducción de letras y de números fuera del rango
                    do
                    {
                        flag = false;
                        try
                        {
                            max = int.Parse(Console.ReadLine());
                            flag = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error, por favor introduzca un número");
                        }
                    } while (!flag);
                    if (max > 3 || max < 0)
                    {
                        max = -1;
                        Console.WriteLine("Error, Por favor introduzca un número entre 0 y 3");
                    }
                } while (max == -1);
                int[] indices = new int[max];
                for(int i = 0; i < max; ++i)
                {
                    indices[i] = -1;
                }
                for(int i = 0; i < max; ++i)
                {//Pregunta que cartas se van a tirar
                    int temp = -1;
                    bool iguales = false;
                    do
                    {
                        flag = false;
                        try
                        {
                            Console.WriteLine("\nCual carta vas a tirar");
                            temp = int.Parse(Console.ReadLine()) - 1;
                            if (temp < 0 || temp > 4)
                            {
                                Console.WriteLine("Error, Por favor introduzca un número entre 1 y 5");
                            }
                            else
                            {
                                flag = true;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error, por favor introduzca un número");
                        }
                    } while (!flag);
                    for (int j = 0; j < max; ++j)
                    {
                        if (temp == indices[j])
                        {
                            Console.WriteLine("Error, ya seleccionaste esa carta");
                            iguales = true;
                        }
                    }
                    if (iguales)
                    {
                        --i;
                    }
                    else
                    {
                        indices[i] = temp;
                    }
                }
                Array.Sort(indices);//Ordena los indices de las cartas para tirarlas
                for (int i = 0; i < max; ++i)
                {
                    tirarCarta(indices[i]-i);
                }//Tira las cartas
                if(max != 0)
                {
                    mostrarCartas(jugadorActivo);
                    Console.WriteLine("\nAhora se te entregaran las nuevas cartas");
                    for (int i = 0; i < max; ++i)
                    {//Entrega las nuevas cartas
                        deck.deadDeck.Add(deck.mazo.Peek());
                        jugadorActivo.Hand.Add(deck.mazo.Pop());
                    }
                }
                mostrarCartas(jugadorActivo);
            }
            jugadores.Enqueue(jugadorActivo);//Regresa al jugador al Queue de jugadores
        }
        public void mostrarCartas(Player jugador)
        {
            foreach (Card carta in jugador.Hand)
            {
                Console.WriteLine("Carta no. {0} de la mano, Carta: {1}, Figura: {2}", jugador.Hand.IndexOf(carta) + 1, deck.ut.getValor(carta.Value), deck.ut.getPalo(carta.Shape));
            }
        }
    }
}
