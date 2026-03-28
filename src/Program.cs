using System;
namespace RemoteControlProject
{
    class Program
    {
        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleKeyInfo keyinfo;
            Remote remote = new();
            Screen screen = new Screen(remote);
            IPrintable[] printables = [screen, remote];
            bool exit = false;

            do
            {
                if (Console.KeyAvailable)
                {
                    keyinfo = Console.ReadKey();
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.Escape:
                            exit = true;
                            break;
                        case ConsoleKey.Enter:
                            remote.PowerButton();
                            break;
                    }
                }
                Console.Clear();
                foreach (var item in printables)
                {
                    item.PrintState();
                }
                Thread.Sleep(10);
            } while(!exit);
            Console.Clear();
        }
    }
}