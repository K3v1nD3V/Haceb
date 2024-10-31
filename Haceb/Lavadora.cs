using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading;

namespace Haceb
{
    internal class Lavadora
    {
        private int kilos;
        private Dictionary<string, string> types = new Dictionary<string, string>
        {
            { "Ropa de color", "agua fría (hasta 20°)" },
            { "Algodón", "agua fría (hasta 20°)" },
            { "Lycra", "agua fría (hasta 20°)" },
            { "Sedas", "agua fría (hasta 20°)" },
            { "Jean", "agua tibia (entre 30 a 50°)" },
            { "Camperas", "agua tibia (entre 30 a 50°)" },
            { "Toallas", "agua caliente (entre 55 a 90°)" },
            { "Sábanas", "agua caliente (entre 55 a 90°)" },
            { "Acolchados", "agua caliente (entre 55 a 90°)" },
            { "Telas blancas gruesas", "agua caliente (entre 55 a 90°)" },
            { "Cortinas de tela", "agua caliente (entre 55 a 90°)" }
        };
        private int clientType;
        private int temperature;

        private int MinuteTime;

        private string waterFillingAudio = @"C:\Users\Usuario\Downloads\waterFilling.wav";
        private string washingAudio = @"C:\Users\Usuario\Downloads\washing.wav";
        private string rinseAudio = @"C:\Users\Usuario\Downloads\rise.wav";
        private string dryAudio = @"C:\Users\Usuario\Downloads\dry.wav";
        private string finishAudio = @"C:\Users\Usuario\Downloads\finish.wav";

        public Lavadora(){}

        public List<string> getTypes()
        {
            return this.types.Keys.ToList();
        }
        public List<string> getRecomendations()
        {
            return types.Values.ToList();
        }

        public void setMinuteTime(int Minutes)
        {
            MinuteTime = Minutes;
        } 
        public void setTemperature(int temperature)
        {
            this.temperature = temperature;
        }  
        public int setKilos(int kilos)
        {
            while (kilos < 5 || kilos > 40)
            {
                Utilities.ShowMessage("La cantidad ingresada esta fuera de rango");
                Console.WriteLine("Ingrese la cantidad de kilos a lavar (No debe ser mayor a 40kg ni menor a 5kg): ");

                string input = Console.ReadLine();
                int.TryParse(input, out int newKilos);
                
                kilos = newKilos;
            }

            this.kilos = kilos;
            return this.kilos;
        }
        public int setClientType(int type)
        {
            List<string> types = getTypes();

            while (type < 1 || type > this.types.Count)
            {
                Utilities.ShowMessage("El tipo ingresado está fuera de rango");
                Console.WriteLine("Ingrese una de las siguientes opciones.");

                for (int i = 0; i < this.types.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {types[i]}");
                }
                Console.Write("Ingrese el número correspondiente al tipo de lavado: ");

                string input = Console.ReadLine();
                int.TryParse(input, out int newType);

                type = newType;
            }

            clientType = type;

            return clientType;
        }

        public void WashingCicle()
        {
            this.WaterFilling();
            Console.Clear();
            this.Washing();
            Console.Clear();
            this.Rinse();
            Console.Clear();

            // Preguntar al usuario si desea comenzar el ciclo de secado
            while (true)
            {
                Console.WriteLine("¿Desea comenzar el ciclo de secado? (s/n)");
                string dryInput = Console.ReadLine();
                Console.Clear();

                if (dryInput == "s")
                {
                    this.Dry();
                    Console.Clear();
                    break; 
                }
                else if (dryInput == "n")
                {
                    Utilities.ShowMessage("El sistema se ha pausa...");
                    Console.WriteLine("Precione enter para continuar con el ciclo de secado.");
                    Console.ReadLine();
                    Console.Clear();
                    this.Dry();
                    Console.Clear();
                    break; 
                }
                else
                {
                    Utilities.ShowMessage("Selección no válida.Intente de nuevo.");
                }
            }
        }

        private void WaterFilling()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(waterFillingAudio);
                Thread blinkThread = new Thread(() => Utilities.BlinkText("Llenando...", 3));
                blinkThread.Start();

                player.PlaySync(); 

                blinkThread.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir el audio: " + ex.Message);
            }
        }
        private void Washing()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(washingAudio);
                Thread blinkThread = new Thread(() => Utilities.BlinkText("Lavando...", 3));
                blinkThread.Start();

                player.PlaySync();

                blinkThread.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir el audio: " + ex.Message);
            }
        }
        private void Rinse()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(rinseAudio);
                Thread blinkThread = new Thread(() => Utilities.BlinkText("Enjuagando...", 3));
                blinkThread.Start();

                player.PlaySync();

                blinkThread.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir el audio: " + ex.Message);
            }
        }
        private void Dry()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(dryAudio);
                Thread blinkThread = new Thread(() => Utilities.BlinkText("Secando...", 3));
                blinkThread.Start();

                player.PlaySync();

                blinkThread.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir el audio: " + ex.Message);
            }
        }
        public void Finish()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(finishAudio);
                Console.WriteLine("Se aterminado el ciclo de lavado...");
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir el audio: " + ex.Message);
            }
        }
    }
}