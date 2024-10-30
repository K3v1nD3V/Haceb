using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Haceb
{
    internal class Utilities
    {
        public static void BlinkText(string text, int durationInSeconds)
        {
            int blinkCount = durationInSeconds * 2; 

            for (int i = 0; i < blinkCount; i++)
            {
                Console.Clear(); 
                Console.WriteLine(text); 
                Thread.Sleep(500); 
                Console.Clear();
                Thread.Sleep(500); 
            }
        }
        public static void ShowMessage(string message, int pauseInSeconds = 2)
        {
            Console.Clear();
            Console.WriteLine(message);
            Thread.Sleep(pauseInSeconds * 1000); 
            Console.Clear();
        }
    }
}
