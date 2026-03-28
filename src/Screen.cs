using System.Timers;
namespace RemoteControlProject
{
    internal class Screen : IPrintable
    {
        private int _volume = 100;
        //Using properties this time lets me put the value cap on the volume inside the setter itself
        private int Volume { get => _volume;
            set {
                if (value < 0) {_volume = 0;}
                else if (value > 100) {_volume = 100;}
                else {_volume = value;}
            } 
        }
        private int _channel = 001;
        //same goes for including the channel rollover logic 
        private int Channel {get => _channel;
            set
            {
                if (value < 1) {_channel = 999;}
                else if (value >999) {_channel = 1;}
                else {_channel = value;}
            }
        }
        private MenuFacade _menus;
        private System.Timers.Timer _channelTimoutTimer;
        //In this version of the project, i'm trying to use more C# naming conventions, here all the private internal only classes start with _
        private bool _isPowered = false;
        private bool _isMuted = false;
        private bool _captionsEnabled = false;
        private string _interimChannelValue ="";
        private readonly Remote remote;
        public Screen(Remote remote)
        {
            this.remote = remote;
            remote.ButtonPressed += new EventHandler<ButtonType>(Remote_ButtonPress);
            _menus = new MenuFacade();
            _channelTimoutTimer = new System.Timers.Timer(2000);
            _channelTimoutTimer.Elapsed += new System.Timers.ElapsedEventHandler(SetChannel);
        }

        private void PowerToggle()
        {
            _isPowered = !_isPowered;
        }
        private void MuteToggle()
        {
            _isMuted = !_isMuted;
        }
        private void CaptionToggle()
        {
            _captionsEnabled = !_captionsEnabled;
        }
        private void VolumeIncrement()
        {
            Volume++;
        }
        private void VolumeDecrement()
        {
            Volume--;
        }
        private void ChannelIncrement()
        {
            Channel++;
        }
        private void ChannelDecrement()
        {
            Channel--;
        }
        private void SetChannel(int channel)
        {
            Channel = channel;
            _interimChannelValue = "";
            _menus.TvTime();
        }
        private void SetChannel(object? sender, ElapsedEventArgs e)
        {
            Channel = UInt16.Parse(_interimChannelValue);
            _interimChannelValue = "";
            _menus.TvTime();
        }

        void Remote_ButtonPress(object? sender, ButtonType button)
        {
            if (!_isPowered)
            {
                if(button == ButtonType.Power)
                {
                    PowerToggle();
                }
            }
            else
            {
                switch (button)
                {
                    case ButtonType.Power:
                    PowerToggle();
                    break;
                case ButtonType.VolumeUp:
                    VolumeIncrement();
                    break;
                case ButtonType.VolumeDown:
                    VolumeDecrement();
                    break;
                case ButtonType.Mute:
                    MuteToggle();
                    break;
                case ButtonType.Caption:
                    CaptionToggle();
                    break;
                case ButtonType.ChannelUp:
                    ChannelIncrement();
                    break;
                case ButtonType.ChannelDown:
                    ChannelDecrement();
                    break;
                case ButtonType.One:
                    _interimChannelValue+="1";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Two:
                    _interimChannelValue+="2";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Three:
                    _interimChannelValue+="3";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Four:
                    _interimChannelValue+="4";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Five:
                    _interimChannelValue+="5";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Six:
                    _interimChannelValue+="6";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Seven:
                    _interimChannelValue+="7";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Eight:
                    _interimChannelValue+="8";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Nine:
                    _interimChannelValue+="9";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                case ButtonType.Zero:
                    _interimChannelValue+="0";
                    _channelTimoutTimer.Stop();
                    _channelTimoutTimer.Start();
                    break;
                }
            }
        }
        public void PrintState()
        {
            string[] settingsValues = _menus.SettingsValues();
            string[] smartValues = _menus.SmartValues();

            if (_isPowered)
            {
                Console.WriteLine("                       O    O");
                Console.WriteLine("                        \\  /");
                Console.WriteLine(" ________________________\\/________________________");
                Console.Write("/");
                    if(smartValues.All(x => x == "Disabled")&settingsValues[0]=="Antenna"){Console.WriteLine("Channel:{0}                                       \\", this.Channel.ToString().PadRight(3));}
                    else {Console.WriteLine("                                                  \\");}
            }
            else
            {
                Console.WriteLine("                       O    O");
                Console.WriteLine("                        \\  /");
                Console.WriteLine(" ________________________\\/________________________");
                Console.WriteLine("/                                                  \\");
                for(int i = 0; i<=7; i++)
                {
                    Console.WriteLine("|                                                  |");
                }
                Console.WriteLine("\\__________________________________________________/");
            }
        }
    }
}