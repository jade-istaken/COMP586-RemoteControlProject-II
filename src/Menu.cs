namespace RemoteControlProject
{
    internal interface IMenu
    {
        protected MenuOptions[] MenuOptions {get;}
        protected int[] MenuOptionValues {get;}
        protected int[] MenuOptionMaxValues {get;}
        public int SelectedOption {get; set;}
        public string[] GetMenuOptionPrintableValues();
        public string[] GetMenuOptionPrintables();
        public void SelectionIncrement();
        public void SelectionDecrement();
        public void OptionIncrement();
        public void OptionDecrement();
        public void Reset();
    }

    internal abstract class Menu : IMenu
    {
        public  MenuOptions[] MenuOptions {get;}
        public int[] MenuOptionValues {get;set;}
        public int[] MenuOptionMaxValues {get;}
        public int SelectedOption {get; set;}
        public abstract string[] GetMenuOptionPrintableValues();
        public string[] GetMenuOptionPrintables()
        {
            string[] retval = new string[MenuOptions.Length];
            for (int i = 0; i < MenuOptions.Length; i++)
            {
                retval[i] = MenuOptions[i].ToString();
            }
            return retval;
        }
        public void SelectionIncrement()
        {
            this.SelectedOption++;
            if (this.SelectedOption >= this.MenuOptions.Length)
            {
                this.SelectedOption = 0;
            }
        }
        public void SelectionDecrement()
        {
            this.SelectedOption--;
            if (this.SelectedOption < 0)
            {
                this.SelectedOption = this.MenuOptions.Length-1;
            }
        }
        public virtual void OptionIncrement()
        {
            this.MenuOptionValues[this.SelectedOption]++;
            if (this.MenuOptionValues[this.SelectedOption] > MenuOptionMaxValues[this.SelectedOption])
            {
                this.MenuOptionValues[this.SelectedOption] = 0;
            }
        }
        public virtual void OptionDecrement()
        {
            this.MenuOptionValues[this.SelectedOption]--;
            if (this.MenuOptionValues[this.SelectedOption] < 0)
            {
                this.MenuOptionValues[this.SelectedOption] = MenuOptionMaxValues[this.SelectedOption];
            }
        }
        public Menu(MenuOptions[] menuOptions, int[] maxValues)
        {
            SelectedOption = 0;
            MenuOptions = menuOptions;
            MenuOptionValues = new int[MenuOptions.Length];
            MenuOptionMaxValues = maxValues;
        }

        public void Reset()
        {
            foreach (var item in MenuOptionValues)
            {
                MenuOptionValues[item] = 0;
            }
        }
    }

    internal class SmartMenu : Menu
    {
        public SmartMenu() : base([RemoteControlProject.MenuOptions.Netflix, RemoteControlProject.MenuOptions.TVPlus, RemoteControlProject.MenuOptions.AmazonPrime, RemoteControlProject.MenuOptions.HBOMax],
            [1,1,1,1])
            {}

        public override string[] GetMenuOptionPrintableValues()
        {
            return [.. this.MenuOptionValues.Select(val => val == 1 ? "Enabled" : "Disabled")];
        }
        //I want to override the base option increment/decrement functions so that only one streaming service can be open at a time
        public override void OptionIncrement()
        {
            base.OptionDecrement();
            if (base.MenuOptionValues[SelectedOption] == 1)
            {
                for (int i=0; i <MenuOptionValues.Length; i++)
                {
                    if (i != SelectedOption)
                    {
                        MenuOptionValues[i] = 0;    
                    }
                }
            }
        }
        public override void OptionDecrement()
        {
            base.OptionDecrement();
            if (base.MenuOptionValues[SelectedOption] == 1)
            {
                for (int i=0; i <MenuOptionValues.Length; i++)
                {
                    if (i != SelectedOption)
                    {
                        MenuOptionValues[i] = 0;    
                    }
                }
            }
        }
    }

    internal class SettingsMenu : Menu
    {
        public SettingsMenu() : base([RemoteControlProject.MenuOptions.Input, RemoteControlProject.MenuOptions.HDR, RemoteControlProject.MenuOptions.MotionSmoothing, RemoteControlProject.MenuOptions.GameEnhancer, RemoteControlProject.MenuOptions.PurColor, RemoteControlProject.MenuOptions.CrystalProcessor4k], 
            [Enum.GetNames<Inputs>().Length-1,1,1,1,1,1])
            {} // I want the inputs to have a max value of just, however many versions of it there are so that if I add more inputs it adapts
        public override string[] GetMenuOptionPrintableValues()
        {
            int inputsIndex = this.MenuOptions.IndexOf(RemoteControlProject.MenuOptions.Input);
            string[] retval = [.. this.MenuOptionValues.Select(val => val == 1 ? "Enabled" : "Disabled")];
            retval[inputsIndex] = ((Inputs)MenuOptionValues[inputsIndex]).ToString(); 
            return retval;
        }
    }

    //Time to implement a factory for these menus as a fun way to use the factory design pattern
    abstract class MenuCreator
    {
        public abstract IMenu CreateMenu(MenuTypes menuType);
    }
    internal class TVMenuCreator : MenuCreator
    {
        public override IMenu CreateMenu(MenuTypes menuType)
        {
            return menuType switch
            {
              MenuTypes.Settings => new SmartMenu(),
              MenuTypes.Smart => new SmartMenu(),
              _ => throw new Exception("Invalid Menu Type")
            };
        }
    }

    internal class MenuFacade //A Facade for holding all the menus that the tv will have
    {
        protected IMenu[] menus;
        protected MenuTypes ActiveMenu {get;set;}
        protected bool IsMenuOpen {get;set;}
        public int SelectedOption
        {
            get => menus[(int)ActiveMenu].SelectedOption;
        }
        public string[] MenuPrintableOptions
        {
            get => menus[(int)ActiveMenu].GetMenuOptionPrintables();
        }
        public string[] MenuPrintableValues
        {
            get => menus[(int)ActiveMenu].GetMenuOptionPrintableValues();
        }
        public MenuFacade()
        {
            MenuCreator menuCreator = new TVMenuCreator();
            menus = [menuCreator.CreateMenu(MenuTypes.Settings), menuCreator.CreateMenu(MenuTypes.Smart)];
        }
        public void OpenMenu(MenuTypes menuType)
        {
            ActiveMenu = menuType;
            IsMenuOpen = true;
        }
        public void CloseMenu()
        {
            IsMenuOpen = false;
        }

        public void SelectionIncrement()
        {
            if (IsMenuOpen)
            {
                menus[(int)ActiveMenu].SelectionIncrement();
            }
        }
        public void SelectionDecrement()
        {
            if (IsMenuOpen)
            {
                menus[(int)ActiveMenu].SelectionDecrement();
            }
        }
        public void OptionIncrement()
        {
            if (IsMenuOpen)
            {
                menus[(int)ActiveMenu].OptionIncrement();
            }
        }
        public void OptionDecrement()
        {
            if (IsMenuOpen)
            {
                menus[(int)ActiveMenu].OptionDecrement();
            }
        }
        public void TvTime()
        {
            menus[(int)MenuTypes.Smart].Reset();
        }
    }

    enum MenuOptions
    {
        Netflix,
        TVPlus,
        AmazonPrime,
        HBOMax,
        Alexa,
        GoogleAssistant,
        CrystalProcessor4k,
        Input,
        HDR,
        PurColor,
        GameEnhancer,
        MotionSmoothing
    }

    enum Inputs
    {
        Antenna,
        HDMI1,
        HDMI2
    }
    enum MenuTypes
    {
        Settings,
        Smart
    }
}