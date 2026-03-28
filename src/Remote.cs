using System.Timers;
namespace RemoteControlProject
{
    internal class Remote : IPrintable
    {
        public Remote()
        {
            this.inputTimer = new System.Timers.Timer(200);
            inputTimer.Elapsed+=new ElapsedEventHandler(StateReset);
        }
        private ButtonType lastPressedButton = ButtonType.None;
        public EventHandler<RemoteControlProject.ButtonType>? ButtonPressed;
        private readonly System.Timers.Timer inputTimer;

        private void StateReset(object? sender, ElapsedEventArgs e)
        {
            this.lastPressedButton = ButtonType.None;
        }
        public void PowerButton()
        {
            lastPressedButton = ButtonType.Power;
            OnButtonPress(ButtonType.Power);
        }
        public void VolumeUp()
        {
            lastPressedButton = ButtonType.VolumeUp;
            OnButtonPress(ButtonType.VolumeUp);
        }
        public void VolumeDown()
        {
            lastPressedButton=ButtonType.VolumeDown;
            OnButtonPress(ButtonType.VolumeDown);
        }
        public void MuteButton()
        {
            lastPressedButton=ButtonType.Mute;
            OnButtonPress(ButtonType.Mute);
        }
        public void CaptionButton()
        {
            lastPressedButton=ButtonType.Caption;
            OnButtonPress(ButtonType.Caption);
        }
        public void ChannelUp()
        {
            lastPressedButton=ButtonType.ChannelUp;
            OnButtonPress(ButtonType.ChannelUp);
        }
        public void ChannelDown()
        {
            lastPressedButton=ButtonType.ChannelDown;
            OnButtonPress(ButtonType.ChannelDown);
        }
        public void ButtonOne()
        {
            lastPressedButton=ButtonType.One;
            OnButtonPress(ButtonType.One);
        }
        public void ButtonTwo()
        {
            lastPressedButton=ButtonType.Two;
            OnButtonPress(ButtonType.Two);
        }
        public void ButtonThree()
        {
            lastPressedButton=ButtonType.Three;
            OnButtonPress(ButtonType.Three);
        }
        public void ButtonFour()
        {
            lastPressedButton=ButtonType.Four;
            OnButtonPress(ButtonType.Four);
        }
        public void ButtonFive()
        {
            lastPressedButton=ButtonType.Five;
            OnButtonPress(ButtonType.Five);
        }
        public void ButtonSix()
        {
            lastPressedButton=ButtonType.Six;
            OnButtonPress(ButtonType.Six);
        }
        public void ButtonSeven()
        {
            lastPressedButton=ButtonType.Seven;
            OnButtonPress(ButtonType.Seven);
        }
        public void ButtonEight()
        {
            lastPressedButton=ButtonType.Eight;
            OnButtonPress(ButtonType.Eight);
        }
        public void ButtonNine()
        {
            lastPressedButton=ButtonType.Nine;
            OnButtonPress(ButtonType.Nine);
        }
        public void ButtonZero()
        {
            lastPressedButton=ButtonType.Zero;
            OnButtonPress(ButtonType.Zero);
        }

        public void DPadUp()
        {
            // Console.WriteLine("Sent Up");
            lastPressedButton=ButtonType.DPadUp;
            OnButtonPress(ButtonType.DPadUp);
        }
        public void DPadDown()
        {
            // Console.WriteLine("Sent Down");
            lastPressedButton=ButtonType.DPadDown;
            OnButtonPress(ButtonType.DPadDown);
        }
        public void DPadLeft()
        {
            lastPressedButton=ButtonType.DPadLeft;
            OnButtonPress(ButtonType.DPadLeft);
        }
        public void DPadRight()
        {
            lastPressedButton=ButtonType.DPadRight;
            OnButtonPress(ButtonType.DPadRight);
        }
        public void MenuButton()
        {
            lastPressedButton=ButtonType.Menu;
            OnButtonPress(ButtonType.Menu);
        }
        public void SettingsButton()
        {
            lastPressedButton=ButtonType.Settings;
            OnButtonPress(ButtonType.Settings);
        }
        protected virtual void OnButtonPress(RemoteControlProject.ButtonType button)
        {
            inputTimer.Stop();
            inputTimer.Start();
            ButtonPressed?.Invoke(this, button);
        }

        public void PrintState()
        {
            //This all prints out an ASCII art representation of a TV remote that reacts to button presses
            Console.WriteLine(".-=======-.");
            Console.Write("| "); writeButton(ButtonType.Power,"P"); Console.WriteLine("       |");
            Console.Write("|  "); writeButton(ButtonType.One,"1"); Console.Write(" "); writeButton(ButtonType.Two,"2");  Console.Write(" "); writeButton(ButtonType.Three,"3"); Console.WriteLine("  |");
            Console.Write("|  "); writeButton(ButtonType.Four,"4"); Console.Write(" "); writeButton(ButtonType.Five,"5");  Console.Write(" "); writeButton(ButtonType.Six,"6"); Console.WriteLine("  |");
            Console.Write("|  "); writeButton(ButtonType.Seven,"7"); Console.Write(" "); writeButton(ButtonType.Eight,"8");  Console.Write(" "); writeButton(ButtonType.Nine,"9"); Console.WriteLine("  |");
            Console.Write("|    "); writeButton(ButtonType.Zero,"0"); Console.WriteLine("    |");
            Console.Write("|  "); writeButton(ButtonType.VolumeUp,"+"); Console.Write(" "); writeButton(ButtonType.Mute,"M");  Console.Write(" "); writeButton(ButtonType.ChannelUp,"^"); Console.WriteLine("  |");
            Console.WriteLine("|  V   C  |");
            Console.Write("|  "); writeButton(ButtonType.VolumeDown,"-"); Console.Write(" "); writeButton(ButtonType.Caption,"©");  Console.Write(" "); writeButton(ButtonType.ChannelDown,"⌄"); Console.WriteLine("  |");
            Console.Write("|  "); writeButton(ButtonType.Settings,"⚙"); Console.Write("   "); writeButton(ButtonType.Menu,"S"); Console.WriteLine("  |");
            Console.Write("|    "); writeButton(ButtonType.DPadUp,"↑"); Console.WriteLine("    |");
            Console.Write("|  "); writeButton(ButtonType.DPadLeft,"←"); Console.Write("   "); writeButton(ButtonType.DPadRight,"→"); Console.WriteLine("  |");
            Console.Write("|    "); writeButton(ButtonType.DPadDown,"↓"); Console.WriteLine("    |");
            Console.WriteLine("|         |");
            Console.WriteLine("'-=======-'");

        }

        private void writeButton(ButtonType button, string output)
        {
            if (this.lastPressedButton == button)
            {
                WriteInverted(output);
            }
            else
            {
                Console.Write(output);
            }
        }
        
        private void WriteInverted(string word)
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