using EasyConsole;
using ProjectDiff.Lib;
using System.IO;

namespace ProjectDiff.Menus
{
    class AddToSolDiff : Page
    {
        public AddToSolDiff(Program program) : base("Add Solution", program)
        {
        }

        public override void Display()
        {
            base.Display();
            string slnPath = Input.ReadString("Provide full path to Solution (.sln) File: ");

            if (!File.Exists(slnPath))
            {
                Output.WriteLine(System.ConsoleColor.Red, "Invalid file path!");
                Input.ReadString("Press [Enter] to go to Main Menu");
                Program.NavigateHome();
            }
            else if (EFLib.SolutionExists(Path.GetFileName(slnPath), slnPath))
            {
                Output.WriteLine(System.ConsoleColor.Red, "Solution has already been added to Repo.");
                Input.ReadString("Press [Enter] to go to Main Menu");
                Program.NavigateHome();
            }
            else
            {
                EFLib.Create(Path.GetFileName(slnPath), slnPath);
                Output.WriteLine(System.ConsoleColor.Green, "{0} added to Repository!", Path.GetFileName(slnPath));
                Input.ReadString("Press [Enter] to go to Main Menu");
                Program.NavigateHome();
            }
        }
    }
}