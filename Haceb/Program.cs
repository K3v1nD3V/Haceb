using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Haceb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lavadora lavadora = new Lavadora();
            bool continuar = true;
            Dictionary<int, Dictionary<string, dynamic>> clientsInfo;
            while (continuar)
            {
                Dictionary<string, dynamic> clientInfo = new Dictionary<string, dynamic>
                {
                    {"kilos", 0},
                    {"type", ""},
                    {"recomendation", "" },
                    {"temperature", 0},
                    {"time", 30 }
                };

                // Tomar la cantidad de kilos

                while (true)
                {
                    Console.WriteLine("Ingrese la cantidad de kilos a lavar (No debe ser mayor a 40kg ni menor a 5kg): ");
                    string inputKilos = Console.ReadLine();

                    if (int.TryParse(inputKilos, out int kilos))
                    {
                        kilos = lavadora.setKilos(kilos);
                        Console.Clear();
                        Console.WriteLine($"Has ingresado {kilos} kg para lavar.");
                        clientInfo["kilos"] = kilos;
                        break;
                    }
                    else
                    {
                        Utilities.ShowMessage("Valor ingresado no valido.");
                    }
                }
                Thread.Sleep(2000);
                Console.Clear();

                // Tomar el tipo

                while (true)
                {
                    // Mostrar los tipos disponibles
                    Console.WriteLine("Seleccione un tipo de lavado:");
                    List<string> types = lavadora.getTypes();

                    for (int i = 0; i < types.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {types[i]}");
                    }

                    // Leer la selección del usuario
                    Console.Write("Ingrese el número correspondiente al tipo de lavado: ");
                    string typeInput = Console.ReadLine();

                    if (int.TryParse(typeInput, out int selection))
                    {
                        selection = lavadora.setClientType(selection);
                        Console.Clear();

                        string selectedType = lavadora.getTypes()[selection - 1];
                        string recomendation = lavadora.getRecomendations()[selection - 1];

                        Console.WriteLine($"Has seleccionado: {selectedType}");

                        clientInfo["type"] = selectedType;
                        clientInfo["recomendation"] = recomendation;
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Selección no válida. Intente de nuevo.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                }
                Thread.Sleep(2000);
                Console.Clear();

                // Tomar la temperatra
                //while (true)
                //{
                //    Console.WriteLine("¿Qué temperatura desea para el lavado? (debe estar entre 1 y 90°)");
                //    Console.WriteLine($"Para {clientInfo["type"]} se recomienda {clientInfo["recomendation"]}");
                //    string temperatureInput = Console.ReadLine();

                //    if (int.TryParse(temperatureInput, out int temperature))
                //    {
                //        lavadora.setTemperature(temperature);
                //        Console.Clear();
                //        Console.WriteLine($"Usted a seleccionado {temperature}° como temperatura de lavado.");
                //        clientInfo["temperature"] = temperature;
                //        break;
                //    }
                //    else
                //    {
                //        Console.Clear();
                //        Console.WriteLine("Selección no válida.Intente de nuevo.");
                //        Thread.Sleep(2000);
                //        Console.Clear();
                //    }
                //}
                //Thread.Sleep(2000);
                //Console.Clear();

                //Tomar el tiempo

                //string setTime;

                //while (true)
                //{
                //    // Preguntar al usuario si se quiero ingresar el timpo
                //    Console.WriteLine("¿Desea fijar el tiempo de lavado? (s/n)");
                //    setTime = Console.ReadLine();

                //    if (setTime == "s" || setTime == "n")
                //    {
                //        Console.Clear();
                //        break;
                //    }
                //    else
                //    {
                //        Console.Clear();
                //        Console.WriteLine("Selección no válida.Intente de nuevo.");
                //        Thread.Sleep(2000);
                //        Console.Clear();
                //    }
                //}

                //if (setTime == "s")
                //{
                //    while (true)
                //    {
                //        // Leer el valor ingresado por el usuario
                //        Console.WriteLine("Ingrese el tiempo de lavado en minutos (no debe ser menor a 30 min): ");
                //        string timeInput = Console.ReadLine();

                //        if (int.TryParse(timeInput, out int time))
                //        {
                //            lavadora.setMinuteTime(time);
                //            Console.Clear();
                //            Console.WriteLine($"El tiempo de lavadorá de {time} minutos");
                //            break;
                //        }
                //        else
                //        {
                //            Console.Clear();
                //            Console.WriteLine("Selección no válida.Intente de nuevo.");
                //            Thread.Sleep(2000);
                //            Console.Clear();
                //        }
                //    }
                //}
                //Thread.Sleep(2000);
                //Console.Clear();

                // Se ejecuta el cliclo de lavado

                Console.WriteLine("Comenzará el cliclo de lavado...");
                Thread.Sleep(2000);
                Console.Clear();

                lavadora.WashingCicle();

                // Mostrar informacion del cliente
                //foreach (var info in clientInfo)
                //{
                //    Console.WriteLine($"  {info.Key}: {info.Value}");
                //}
                // Mostrar informacion de los clientes
                //foreach (var client in clientsInfo)
                //{
                //    Console.WriteLine($"Cliente ID: {client.Key}");

                //    foreach (var info in client.Value)
                //    {
                //        Console.WriteLine($"  {info.Key}: {info.Value}");
                //    }
                //}
                // tomar el tiempo 

                




                //// Preguntar si continuar
                //Console.Write("¿Desea realizar otra operación? (s/n): ");
                //string continuarInput = Console.ReadLine();
                //continuar = continuarInput?.ToLower() == "s";
            }
        }
    }
}