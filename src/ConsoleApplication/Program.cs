using System;
using ClassLibrary;

namespace ConsoleApplication
{
    public static class Program
    {
        public static void Main()
        {
            var train = new Train();
            train.StartEngines();
            Console.WriteLine("Hello World!");
        }
    }
}