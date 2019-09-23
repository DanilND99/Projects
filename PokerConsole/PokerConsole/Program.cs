using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerConsole.Clases;
namespace PokerConsole
{
    class Program
    {
        //Desarrolladores:
        //Daniel Armando Núñez Delgadillo
        //Mario Andres Ruiz Navejas
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();
            Utilities ut = new Utilities();
            bool keepPlaying = false;
            List<Player> analisisResultados = new List<Player>();
            dealer.meetPlayers();
            do
            {
                dealer.Repartir();
                for (int i = 0; i < dealer.jugadores.Count; ++i)
                {
                    dealer.turno();
                    Console.WriteLine("\n\n\n");
                }

                foreach (Player jugador in dealer.jugadores)
                {
                    jugador.Nivel = WinCondition.asignarNivel(jugador);
                }
                analisisResultados = WinCondition.revisarEmpate(dealer.jugadores);
                analisisResultados = WinCondition.GetGanadores(analisisResultados);
                if (analisisResultados.Count == 1)
                {
                    Console.WriteLine("El ganador es {0} con {1} usando la mano:", analisisResultados[0].Name, ut.getNivel(analisisResultados[0].Nivel));
                    dealer.mostrarCartas(analisisResultados[0]);
                }
                else
                {
                    Console.WriteLine("Empate");
                    foreach (Player jugador in analisisResultados)
                    {
                        Console.WriteLine("Gana {0} con {1} usando la mano:", jugador.Name, ut.getNivel(jugador.Nivel));
                        dealer.mostrarCartas(jugador);
                        Console.WriteLine("\n\n");
                    }
                }
                int accion = 0;
                bool flag = false;
                do
                {
                    Console.WriteLine("\n\nDeseas jugar otra vez?\n1. Si\n2. No");
                    try
                    {
                        accion = int.Parse(Console.ReadLine());
                        flag = true;
                    }
                    catch (Exception)
                    {//Evita la introducción de letras
                        Console.WriteLine("Error por favor introduzca un número");
                    }
                } while (!flag);
                if (accion == 1)
                {
                    keepPlaying = true;
                    analisisResultados = dealer.Reiniciar(analisisResultados);
                    Console.Clear();
                }
                else
                {
                    keepPlaying = false;
                }
            } while (keepPlaying);
        }
    }
}