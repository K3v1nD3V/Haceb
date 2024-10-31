using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Schema;

namespace Haceb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lavadora lavadora = new Lavadora();
            bool continuar = true;
            List<Dictionary<string, dynamic>> clientsInfo = new List<Dictionary<string, dynamic>>();

            Utilities.ShowMessage("Bienvenido al sistema");
            while (continuar)
            {
                Dictionary<string, dynamic> clientInfo = new Dictionary<string, dynamic>
                {
                    {"name", "" },
                    {"date", DateTime.Now},
                    {"kilos", 0},
                    {"type", ""},
                    {"recomendation", "" },
                    {"temperature", 0},
                    {"time", 30 }
                };

                //// Tomar nombre del usuario
                while (true)
                {
                    Console.WriteLine("Ingrese su nombre: ");
                    string name = Console.ReadLine();
                    name = name.Trim();

                    if (name.Length != 0)
                    {
                        Utilities.ShowMessage($"Bienvenido {name}!", 1);
                        clientInfo["name"] = name;
                        break;
                    }
                    else
                    {
                        Utilities.ShowMessage("El nombre no puede estar en blaco. Intente de nuevo.");
                    }

                }

                // Tomar la cantidad de kilos
                while (true)
                {
                    Console.WriteLine("Ingrese la cantidad de kilos a lavar (No debe ser mayor a 40kg ni menor a 5kg, recuerde que cada kilo tiene un costo de $4.000): ");
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
                        Utilities.ShowMessage("Selección no válida. Intente de nuevo.");
                    }
                }
                Thread.Sleep(1000);
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
                        Utilities.ShowMessage("Selección no válida. Intente de nuevo.");
                    }
                }
                Thread.Sleep(1000);
                Console.Clear();

                // Tomar la temperatra
                while (true)
                {
                    Console.WriteLine("¿Qué temperatura desea para el lavado? (debe estar entre 1 y 90°)");
                    Console.WriteLine($"Para {clientInfo["type"]} se recomienda {clientInfo["recomendation"]}");
                    string temperatureInput = Console.ReadLine();

                    if (int.TryParse(temperatureInput, out int temperature))
                    {
                        lavadora.setTemperature(temperature);
                        Console.Clear();
                        Console.WriteLine($"Usted a seleccionado {temperature}° como temperatura de lavado.");
                        clientInfo["temperature"] = temperature;
                        break;
                    }
                    else
                    {
                        Utilities.ShowMessage("Selección no válida. Intente de nuevo.");
                    }
                }
                Thread.Sleep(1000);
                Console.Clear();

                //Tomar el tiempo

                string setTime;

                while (true)
                {
                    // Preguntar al usuario si se quiero ingresar el timpo
                    Console.WriteLine("¿Desea fijar el tiempo de lavado? (s/n)");
                    setTime = Console.ReadLine();

                    if (setTime == "s" || setTime == "n")
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Utilities.ShowMessage("Selección no válida. Intente de nuevo.");
                    }
                }

                if (setTime == "s")
                {
                    while (true)
                    {
                        // Leer el valor ingresado por el usuario
                        Console.WriteLine("Ingrese el tiempo de lavado en minutos (no debe ser menor a 30 min): ");
                        string timeInput = Console.ReadLine();

                        if (int.TryParse(timeInput, out int time))
                        {
                            lavadora.setMinuteTime(time);
                            Console.Clear();
                            Console.WriteLine($"El tiempo de lavadorá de {time} minutos");
                            break;
                        }
                        else
                        {
                            Utilities.ShowMessage("Selección no válida. Intente de nuevo.");
                        }
                    }
                }
                Thread.Sleep(1000);

                // Se ejecuta el cliclo de lavado

                //Utilities.ShowMessage("Comenzará el ciclo de lavado...");
                //lavadora.WashingCicle();

                // Calculo de datos para el cliente 
                //Dictionary<string, dynamic> clientInfo = new Dictionary<string, dynamic>
                //{
                //    {"name", "Kevin" },
                //    {"date", DateTime.Now},
                //    {"kilos", 10},
                //    {"type", "Blanca"},
                //    {"recomendation", "Blablabla" },
                //    {"temperature", 20},
                //    {"time", 30 }
                //};

                double IVA = 0.1;
                double price = 4000;
                List<string> addPercenList = new List<string> { "Blanca", "Algodon", "Tennis" };

                if (addPercenList.Contains(clientInfo["type"]))
                {
                    price = price + (price * 0.03);
                }
                Console.WriteLine(price);

                // Mostrar factura al cliente

                double total = clientInfo["kilos"] * price;
                double IVAtotal = total + (total * IVA);

                Console.WriteLine("Información del Cliente:");
                Console.WriteLine("------------------------");
                Console.WriteLine($"Fecha y Hora del Lavado: {clientInfo["date"]:g}");
                Console.WriteLine($"Nombre del Cliente: {clientInfo["name"]}");
                Console.WriteLine($"Total sin IVA: {total:C}");
                Console.WriteLine($"Total con IVA: {IVAtotal:C}");
                Console.WriteLine("------------------------");

                // Calcular datos para el empresario
                double utilities = total * 0.4;
                double kwm = 0.04;
                double operationKwConsume = kwm * clientInfo["time"] * clientInfo["kilos"];
                double operationEnergyCost = operationKwConsume * 1041;

                Dictionary<string, dynamic> operationInfo = new Dictionary<string, dynamic>
                {
                    { "utilidades", utilities},
                    { "kwmConsume", operationKwConsume },
                    { "energyCost", operationEnergyCost }
                };

                clientsInfo.Add(operationInfo);
                //-------------------------------------------------------------Prueba--------------------------------------------//
                //Dictionary<string, dynamic> client1Info = new Dictionary<string, dynamic>
                //{
                //    { "utilidades", 20000 },
                //    { "kwmConsume", 520 },
                //    { "energyCost", 20820 }
                //};
                //        clientsInfo.Add(client1Info);

                //        // Cliente 2
                //        Dictionary<string, dynamic> client2Info = new Dictionary<string, dynamic>
                //{
                //    { "utilidades", 10500 },
                //    { "kwmConsume", 318.0 },
                //    { "energyCost", 29148.0 }
                //};
                //        clientsInfo.Add(client2Info);

                //        // Cliente 3
                //        Dictionary<string, dynamic> client3Info = new Dictionary<string, dynamic>
                //{
                //    { "utilidades", 34056 },
                //    { "kwmConsume", 15.0 },
                //    { "energyCost", 15615.0 }
                //};
                //        clientsInfo.Add(client3Info);

                //        // Cliente 4
                //        Dictionary<string, dynamic> client4Info = new Dictionary<string, dynamic>
                //{
                //    { "utilidades", 12345 },
                //    { "kwmConsume", 120.0 },
                //    { "energyCost", 31230.0 }
                //};
                //        clientsInfo.Add(client4Info);

                //        // Cliente 5
                //        Dictionary<string, dynamic> client5Info = new Dictionary<string, dynamic>
                //{
                //    { "utilidades", 8456 },
                //    { "kwmConsume", 225.0 },
                //    { "energyCost", 26025.0 }
                //};
                //clientsInfo.Add(client5Info);
                // Preguntar si continuar
                while (true)
                {
                    Console.Write("¿Desea realizar otra operación? (s/n): ");
                    string continuarInput = Console.ReadLine();
                    Console.Clear();

                    if (continuarInput == "s")
                    {
                        break;
                    }
                    else if ( continuarInput == "n" )
                    {
                        Console.WriteLine("Precione escape para confirmar.");
                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Gracias por usar nuestra maquina.");
                            Thread.Sleep(1000);
                            continuar = false;
                            break;
                        }
                        else
                        {
                            Utilities.ShowMessage("Usted a cancelado la salida. Intente de nuevo.");
                        }
                    }
                    else
                    {
                        Utilities.ShowMessage("Selección no válida. Intente de nuevo.");
                    }
                }

                // Mostrar informacion al empresario

                Console.WriteLine("=====================================");
                Console.WriteLine($"Cantidad de clientes atendidos: {clientsInfo.Count}");
                Console.WriteLine("=====================================");

                double totalUtilities = 0;
                double totalEnergyConsume = 0;
                double totalEnergyCost = 0;

                foreach (var client in clientsInfo)
                {
                    Console.WriteLine($"Cliente: ");
                    Console.WriteLine($"  - Utilidades: {client["utilidades"]:C}");
                    Console.WriteLine($"  - Consumo de KW: {client["kwmConsume"]}");
                    Console.WriteLine($"  - Costo de KW: {client["energyCost"]:C}");
                    Console.WriteLine("-------------------------------------");

                    totalUtilities += client["utilidades"];
                    totalEnergyConsume += client["kwmConsume"];
                    totalEnergyCost += client["energyCost"];
                }

                Console.WriteLine("=====================================");
                Console.WriteLine($"Total Utilidades: {totalUtilities:C}");
                Console.WriteLine($"Total Consumo de KW: {totalEnergyConsume}");
                Console.WriteLine($"Total Costo de KW: {totalEnergyCost:C}");
                Console.WriteLine("=====================================");

            }
        }
    }
}