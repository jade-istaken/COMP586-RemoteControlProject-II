using System.Timers;
namespace RemoteControlProject
{
    internal class Remote : IPrintable
    {
        public Remote()
        {
            this._inputTimer = new System.Timers.Timer(200);
            _inputTimer.Elapsed+=new ElapsedEventHandler(StateReset);
        }
        private ButtonType _lastPressedButton = ButtonType.None;
        public EventHandler<ButtonType>? ButtonPressed;
        private readonly System.Timers.Timer _inputTimer;

        private void StateReset(object? sender, ElapsedEventArgs e)
        {
            this._lastPressedButton = ButtonType.None;
        }
        public void PowerButton()
        {
            _lastPressedButton = ButtonType.Power;
            OnButtonPress(ButtonType.Power);
        }
        public void VolumeUp()
        {
            _lastPressedButton = ButtonType.VolumeUp;
            OnButtonPress(ButtonType.VolumeUp);
        }
        public void VolumeDown()
        {
            _lastPressedButton=ButtonType.VolumeDown;
            OnButtonPress(ButtonType.VolumeDown);
        }
        public void MuteButton()
        {
            _lastPressedButton=ButtonType.Mute;
            OnButtonPress(ButtonType.Mute);
        }
        public void CaptionButton()
        {
            _lastPressedButton=ButtonType.Caption;
            OnButtonPress(ButtonType.Caption);
        }
        public void ChannelUp()
        {
            _lastPressedButton=ButtonType.ChannelUp;
            OnButtonPress(ButtonType.ChannelUp);
        }
        public void ChannelDown()
        {
            _lastPressedButton=ButtonType.ChannelDown;
            OnButtonPress(ButtonType.ChannelDown);
        }
        public void ButtonOne()
        {
            _lastPressedButton=ButtonType.One;
            OnButtonPress(ButtonType.One);
        }
        public void ButtonTwo()
        {
            _lastPressedButton=ButtonType.Two;
            OnButtonPress(ButtonType.Two);
        }
        public void ButtonThree()
        {
            _lastPressedButton=ButtonType.Three;
            OnButtonPress(ButtonType.Three);
        }
        public void ButtonFour()
        {
            _lastPressedButton=ButtonType.Four;
            OnButtonPress(ButtonType.Four);
        }
        public void ButtonFive()
        {
            _lastPressedButton=ButtonType.Five;
            OnButtonPress(ButtonType.Five);
        }
        public void ButtonSix()
        {
            _lastPressedButton=ButtonType.Six;
            OnButtonPress(ButtonType.Six);
        }
        public void ButtonSeven()
        {
            _lastPressedButton=ButtonType.Seven;
            OnButtonPress(ButtonType.Seven);
        }
        public void ButtonEight()
        {
            _lastPressedButton=ButtonType.Eight;
            OnButtonPress(ButtonType.Eight);
        }
        public void ButtonNine()
        {
            _lastPressedButton=ButtonType.Nine;
            OnButtonPress(ButtonType.Nine);
        }
        public void ButtonZero()
        {
            _lastPressedButton=ButtonType.Zero;
            OnButtonPress(ButtonType.Zero);
        }

        public void DPadUp()
        {
            _lastPressedButton=ButtonType.DPadUp;
            OnButtonPress(ButtonType.DPadUp);
        }
        public void DPadDown()
        {
            _lastPressedButton=ButtonType.DPadDown;
            OnButtonPress(ButtonType.DPadDown);
        }
        public void DPadLeft()
        {
            _lastPressedButton=ButtonType.DPadLeft;
            OnButtonPress(ButtonType.DPadLeft);
        }
        public void DPadRight()
        {
            _lastPressedButton=ButtonType.DPadRight;
            OnButtonPress(ButtonType.DPadRight);
        }
        public void MenuButton()
        {
            _lastPressedButton=ButtonType.Menu;
            OnButtonPress(ButtonType.Menu);
        }
        public void SettingsButton()
        {
            _lastPressedButton=ButtonType.Settings;
            OnButtonPress(ButtonType.Settings);
        }
        protected virtual void OnButtonPress(ButtonType button)
        {
            _inputTimer.Stop();
            _inputTimer.Start();
            ButtonPressed?.Invoke(this, button);
        }

        public void PrintState()
        {
            //This all prints out an ASCII art representation of a TV remote that reacts to button presses
            Console.WriteLine(".-=======-.");
            Console.Write("| "); WriteButton(ButtonType.Power,"P"); Console.WriteLine("       |");
            Console.Write("|  "); WriteButton(ButtonType.One,"1"); Console.Write(" "); WriteButton(ButtonType.Two,"2");  Console.Write(" "); WriteButton(ButtonType.Three,"3"); Console.WriteLine("  |");
            Console.Write("|  "); WriteButton(ButtonType.Four,"4"); Console.Write(" "); WriteButton(ButtonType.Five,"5");  Console.Write(" "); WriteButton(ButtonType.Six,"6"); Console.WriteLine("  |");
            Console.Write("|  "); WriteButton(ButtonType.Seven,"7"); Console.Write(" "); WriteButton(ButtonType.Eight,"8");  Console.Write(" "); WriteButton(ButtonType.Nine,"9"); Console.WriteLine("  |");
            Console.Write("|    "); WriteButton(ButtonType.Zero,"0"); Console.WriteLine("    |");
            Console.Write("|  "); WriteButton(ButtonType.VolumeUp,"+"); Console.Write(" "); WriteButton(ButtonType.Mute,"M");  Console.Write(" "); WriteButton(ButtonType.ChannelUp,"^"); Console.WriteLine("  |");
            Console.WriteLine("|  V   C  |");
            Console.Write("|  "); WriteButton(ButtonType.VolumeDown,"-"); Console.Write(" "); WriteButton(ButtonType.Caption,"©");  Console.Write(" "); WriteButton(ButtonType.ChannelDown,"⌄"); Console.WriteLine("  |");
            Console.Write("|  "); WriteButton(ButtonType.Settings,"⚙"); Console.Write("   "); WriteButton(ButtonType.Menu,"S"); Console.WriteLine("  |");
            Console.Write("|    "); WriteButton(ButtonType.DPadUp,"↑"); Console.WriteLine("    |");
            Console.Write("|  "); WriteButton(ButtonType.DPadLeft,"←"); Console.Write("   "); WriteButton(ButtonType.DPadRight,"→"); Console.WriteLine("  |");
            Console.Write("|    "); WriteButton(ButtonType.DPadDown,"↓"); Console.WriteLine("    |");
            Console.WriteLine("|         |");
            Console.WriteLine("'-=======-'");

        }

        private void WriteButton(ButtonType button, string output)
        {
            if (this._lastPressedButton == button)
            {
                WriteInverted(output);
            }
            else
            {
                Console.Write(output);
            }
        }
        
        private static void WriteInverted(string word)
        {
            var currentForeground = Console.ForegroundColor;
            var currentBackground = Console.BackgroundColor;
            Console.ForegroundColor = currentBackground;
            Console.BackgroundColor = currentForeground;
            Console.Write(word);
            Console.ForegroundColor = currentForeground;
            Console.BackgroundColor = currentBackground;
        }
    }

    enum ButtonType
    {
        None,
        Power,
        VolumeUp,
        VolumeDown,
        Mute,
        Caption,
        ChannelUp,
        ChannelDown,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Zero,
        Menu,
        Settings,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight
    }
}