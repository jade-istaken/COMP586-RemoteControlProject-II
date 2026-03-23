using System;
namespace RemoteControlProject
{
    internal interface IMenu
    {
        protected MenuOptions[] MenuOptions {get;}
        protected int[] MenuOptionValues {get; set;}
        protected int[] MenuOptionMaxValues {get;}
        protected int SelectedOption {get; set;}
        public string[] GetMenuOptionPrintableValues();
        public void SelectionIncrement();
        public void SelectionDecrement();
        public void OptionIncrement();
        public void OptionDecrement();
    }

    internal abstract class Menu : IMenu
    {
        public  MenuOptions[] MenuOptions {get;}
        public int[] MenuOptionValues {get;set;}
        public int[] MenuOptionMaxValues {get;}
        public int SelectedOption {get; set;}
        public abstract string[] GetMenuOptionPrintableValues();
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
        public void OptionIncrement()
        {
            this.MenuOptionValues[this.SelectedOption]++;
            if (this.MenuOptionValues[this.SelectedOption] > MenuOptionMaxValues[this.SelectedOption])
            {
                this.MenuOptionValues[this.SelectedOption] = 0;
            }
        }
        public void OptionDecrement()
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
    }

    internal class SmartMenu : Menu
    {
        public SmartMenu() : base([RemoteControlProject.MenuOptions.Netflix, RemoteControlProject.MenuOptions.TVPlus, RemoteControlProject.MenuOptions.Alexa, RemoteControlProject.MenuOptions.GoogleAssistant],
            [1,1,1,1])
            {}

        public override string[] GetMenuOptionPrintableValues()
        {
            throw new NotImplementedException();
        }
    }

    internal class SettingsMenu : Menu
    {
        public SettingsMenu() : base([RemoteControlProject.MenuOptions.Input, RemoteControlProject.MenuOptions.HDR, RemoteControlProject.MenuOptions.MotionSmoothing, RemoteControlProject.MenuOptions.GameEnhancer, RemoteControlProject.MenuOptions.PurColor, RemoteControlProject.MenuOptions.CrystalProcessor4k], 
            [Enum.GetNames<Inputs>().Length-1,1,1,1,1,1])
            {}
        public override string[] GetMenuOptionPrintableValues()
        {
            throw new NotImplementedException();
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

    enum MenuOptions
    {
        Netflix,
        TVPlus,
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