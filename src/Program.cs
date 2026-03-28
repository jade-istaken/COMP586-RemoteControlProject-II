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
                        case ConsoleKey.Multiply:
                            remote.VolumeUp();
                            break;
                        case ConsoleKey.Divide:
                            remote.VolumeDown();
                            break;
                        case ConsoleKey.OemPeriod:
                            remote.MuteButton();
                            break;
                        case ConsoleKey.OemComma:
                            remote.CaptionButton();
                            break;
                        case ConsoleKey.Add:
                            remote.ChannelUp();
                            break;
                        case ConsoleKey.Subtract:
                            remote.ChannelDown();
                            break;
                        case ConsoleKey.D1:
                            remote.ButtonOne();
                            break;
                        case ConsoleKey.D2:
                            remote.ButtonTwo();
                            break;
                        case ConsoleKey.D3:
                            remote.ButtonThree();
                            break;
                        case ConsoleKey.D4:
                            remote.ButtonFour();
                            break;
                        case ConsoleKey.D5:
                            remote.ButtonFive();
                            break;
                        case ConsoleKey.D6:
                            remote.ButtonSix();
                            break;
                        case ConsoleKey.D7:
                            remote.ButtonSeven();
                            break;
                        case ConsoleKey.D8:
                            remote.ButtonEight();
                            break;
                        case ConsoleKey.D9:
                            remote.ButtonNine();
                            break;
                        case ConsoleKey.D0:
                            remote.ButtonZero();
                            break;
                        case ConsoleKey.Home:
                            remote.MenuButton();
                            break;
                        case ConsoleKey.End:
                            remote.SettingsButton();
                            break;
                        case ConsoleKey.UpArrow:
                            remote.DPadUp();
                            break;
                        case ConsoleKey.DownArrow:
                            remote.DPadDown();
                            break;
                        case ConsoleKey.LeftArrow:
                            remote.DPadLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            remote.DPadRight();
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