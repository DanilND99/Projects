using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsole.Clases
{
    static class WinCondition
    {
        static Utilities ut = new Utilities();
        public static List<Player> desempatar(List<Player> survivors, int valorComparar)
        {//Regresa al jugador o jugadores con la carta mas alta en el lugar de la mano especificado
            int winner = -1;
            for(int i = 0; i < survivors.Count; ++i)
            {
                if(survivors[i].Hand[valorComparar].Value > winner)
                {
                    winner = survivors[i].Hand[valorComparar].Value;
                }
            }
            for (int i = 0; i < survivors.Count; ++i)
            {
                if (survivors[i].Hand[valorComparar].Value != winner)
                {
                    survivors.Remove(survivors[i]);
                    --i;
                }
            }
            return survivors;
        }
        public static int desempatador1V1(int jugador1, int jugador2)
        {//Uso exclusivo de pares desempata los pares
            if(jugador1 > jugador2)
            {
                return 0;
            }else if(jugador1 < jugador2)
            {
                return 1;
            }
            return 2;
        }
        public static List<Card> reacomodar(List<Card> mano)
        {//Acomoda la mano en el caso de una corrida (2,3,4,5,A) a (A,2,3,4,5)
            if (mano[4].Value == 12 && mano[0].Value == 0)
            {
                Card temp = new Card(0, 0);
                temp = mano[4];
                mano[4] = mano[3];
                mano[3] = mano[2];
                mano[2] = mano[1];
                mano[1] = mano[0];
                mano[0] = temp;
            }
            return mano;
        }
        public static List<Player> VsCartaAlta(List<Player> rivales,int c)
        {//C es el tamaño de la mano -1
            do
            {
                rivales = desempatar(rivales, c);
                if (rivales.Count == 1)
                {
                    c = -1;
                }
                else
                {
                    --c;
                }
            } while (c != -1);
            return rivales;
        }//Regresa a los jugadores o jugador que tengan las mismas cartas altas
        public static int CartaAltaJugador(Player jugador,int previaCartaAlta, int tipoCarta)
        {//Obtiene la carta mas alta tomando en cuenta la carta mas alta anterior
            int cartaAlta = -1;
            for (int i = 4; i >= 0; --i)
            {
                if(jugador.Repetidos[i] == tipoCarta && jugador.Hand[i].Value < previaCartaAlta && jugador.Hand[i].Value > cartaAlta)
                {
                    cartaAlta = jugador.Hand[i].Value;
                }
            }
            return cartaAlta;
        }
        public static List<Player> GetGanadores(List<Player> jugadores)
        {//Obtiene el jugador o los jugadores que ganaron la partida
            int nivel = jugadores[0].Nivel;
            int[] cartasAltas = new int[5];
            switch (nivel)
            {
                case 9:
                    foreach(Player jugador in jugadores)
                    {
                        jugador.Hand = reacomodar(jugador.Hand);
                    }
                    jugadores = desempatar(jugadores,4);
                    break;
                case 8:
                    jugadores = desempatar(jugadores, 3);
                    break;
                case 7:
                    jugadores = desempatar(jugadores, 2);
                    break;
                case 6:
                    jugadores = VsCartaAlta(jugadores, 4);
                    break;
                case 5:
                    foreach (Player jugador in jugadores)
                    {
                        jugador.Hand = reacomodar(jugador.Hand);
                    }
                    jugadores = desempatar(jugadores, 4);
                    break;
                case 4:
                    jugadores = desempatar(jugadores, 2);
                    break;
                case 3:
                    jugadores = desempatar(jugadores, 3);
                    if(jugadores.Count != 1)
                    {
                        jugadores = desempatar(jugadores, 1);
                        if(jugadores.Count != 1)
                        {
                            cartasAltas[0] = CartaAltaJugador(jugadores[0], 13, 1);
                            cartasAltas[1] = CartaAltaJugador(jugadores[1], 13, 1);
                            if (desempatador1V1(cartasAltas[0],cartasAltas[1]) == 0)
                            {
                                jugadores.Remove(jugadores[1]);
                            }else if (desempatador1V1(cartasAltas[0], cartasAltas[1]) == 1)
                            {
                                jugadores.Remove(jugadores[0]);
                            }
                        }
                    }
                    break;
                case 2:
                    if (jugadores.Count != 1)
                    {
                        for (int i = 0; i < jugadores.Count; ++i)
                        {
                            cartasAltas[i] = CartaAltaJugador(jugadores[i], 13, 2);
                        }
                        for (int i = 0; i < jugadores.Count - 1; ++i)
                        {
                            if (desempatador1V1(cartasAltas[i], cartasAltas[i+1]) == 0)
                            {
                                jugadores.Remove(jugadores[i+1]);
                                --i;
                            }
                            else if (desempatador1V1(cartasAltas[i], cartasAltas[i+1]) == 1)
                            {
                                jugadores.Remove(jugadores[i]);
                                --i;
                            }
                        }
                        if (jugadores.Count != 1)
                        {
                            if (desempatador1V1(cartasAltas[0], cartasAltas[1]) == 0)
                            {
                                jugadores.Remove(jugadores[1]);
                            }
                            else if (desempatador1V1(cartasAltas[0], cartasAltas[1]) == 1)
                            {
                                jugadores.Remove(jugadores[0]);
                            }
                            else
                            {
                                cartasAltas[0] = 13;
                                cartasAltas[1] = 13;
                                bool finalizo = false;
                                int c = 0;
                                do
                                {
                                    c++;
                                    cartasAltas[0] = CartaAltaJugador(jugadores[0], cartasAltas[0], 1);
                                    cartasAltas[1] = CartaAltaJugador(jugadores[1], cartasAltas[1], 1);
                                    if (desempatador1V1(cartasAltas[0], cartasAltas[1]) == 0)
                                    {
                                        jugadores.Remove(jugadores[1]);
                                        finalizo = true;
                                    }
                                    else if (desempatador1V1(cartasAltas[0], cartasAltas[1]) == 1)
                                    {
                                        jugadores.Remove(jugadores[0]);
                                        finalizo = true;
                                    }
                                    if (c == 3)
                                    {
                                        finalizo = true;
                                    }
                                } while (!finalizo);
                            }
                        }
                        
                    }
                    break;
                default:
                    jugadores = VsCartaAlta(jugadores, 4);
                    break;
            }
            return jugadores;
        }
        public static List<Player> revisarEmpate(Queue<Player> jugadores)
        {//Regresa los jugadores que hay en el nivel más alto
            List<Player> survivors = new List<Player>();
            int nivelAlto = 0;
            foreach (Player jugador in jugadores)
            {
                survivors.Add(jugador);
                if (nivelAlto < jugador.Nivel)
                {
                    nivelAlto = jugador.Nivel;
                }
            }
            foreach (Player jugador in jugadores)
            {
                if(nivelAlto != jugador.Nivel)
                {
                    survivors.Remove(jugador);
                }
            }
            return survivors;
        }
        public static void acomodarMano(Player jugador)
        {//Ordena las cartas de menor a mayor en su valor
            List<Card> tempo = jugador.Hand.OrderBy(o => o.Value).ToList();
            jugador.Hand = tempo;
            List<int> repe = revisarRepetidos(tempo);
            jugador.Repetidos = repe;
        }
        public static List<int> revisarRepetidos(List<Card> mano)
        {//Revisa cuantas veces se repite el mismo valor de cada carta en la mano
            List<int> temp = new List<int>();
            for (int i = 0; i < mano.Count; ++i)
            {
                int c = 1;
                for (int j = 0; j < mano.Count; ++j)
                {
                    if (i != j && mano[i].Value == mano[j].Value)
                    {
                        c++;
                    }
                }
                temp.Add(c);
            }
            return temp;
        }
        public static int asignarNivel(Player jugador)
        {//Le dice que nivel de mano saco al jugador
            acomodarMano(jugador);
            if (esReal(jugador.Hand))
            {
                return 9;
            }else if (esPoker(jugador.Repetidos))
            {
                return 8;
            }else if (esFullHouse(jugador.Repetidos))
            {
                return 7;
            }else if (esColor(jugador.Hand))
            {
                return 6;
            }
            else if (esEscalera(jugador.Hand))
            {
                return 5;
            }
            else if (esTercia(jugador.Repetidos))
            {
                return 4;
            }
            else if (es2par(jugador.Repetidos))
            {
                return 3;
            }
            else if (esPar(jugador.Repetidos))
            {
                return 2;
            }
            return 1;
        }
        public static bool esReal(List<Card> mano)
        {
            if (esColor(mano) && esEscalera(mano))
            {
                return true;
            }
            return false;
        }
        public static bool esPoker(List<int> rep)
        {
            if(rep[2] == 4)
            {
                return true;
            }
            return false;
        }
        public static bool esFullHouse(List<int> rep)
        {
            if ((rep[0] == 3 && rep[4] == 2)||(rep[0] == 2 && rep[4] == 3))
            {
                return true;
            }
            return false;
        }
        public static bool esColor(List<Card> mano)
        {
            if (mano[0].Shape == mano[1].Shape && mano[0].Shape == mano[2].Shape && mano[0].Shape == mano[3].Shape && mano[0].Shape == mano[4].Shape)
            {
                return true;
            }
            return false;
        }
        public static bool esEscalera(List<Card> mano)
        {
            if((mano[0].Value == mano[1].Value - 1 && mano[1].Value == mano[2].Value - 1 &&
                    mano[2].Value == mano[3].Value - 1 && mano[3].Value == mano[4].Value - 1)
                    || (mano[0].Value == 0 && mano[1].Value == 1 && mano[2].Value == 2 &&
                    mano[3].Value == 3 && mano[4].Value == 12))
            {
                return true;
            }
            return false;
        }
        public static bool esTercia(List<int> rep)
        {
            if (rep[2] == 3)
            {
                return true;
            }
            return false;
        }
        public static bool es2par(List<int> rep)
        {
            if(rep[1] == 2 && rep[3] == 2)
            {
                return true;
            }
            return false;
        }
        public static bool esPar(List<int> rep)
        {
            if(rep[0] == 2 || rep[1] == 2 || rep[2] == 2 || rep[3] == 2)
            {
                return true;
            }
            return false;
        }
    }
}