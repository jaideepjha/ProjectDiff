using EasyConsole;
using ProjectDiff.Lib;
using System;
using System.Collections.Generic;

namespace ProjectDiff.Menus
{
    class CreateSnapshot : Page
    {
        public CreateSnapshot(Program program):base("Create Snapshot", program)
        {
        }

        public override void Display()
        {
            base.Display();
            var sols = EFLib.GetSolutions();
            if (sols.Count == 0)
            {
                Output.WriteLine(System.ConsoleColor.Red, "No Solution has been added to the repository yet!");
                Input.ReadString("Press [Enter] to go to Main Menu");
                Program.NavigateHome();
            }
            List<string> options = new List<string>();
            foreach (var sol in sols)
            {
                options.Add(String.Format("{0} ==== {1}", sol.Name, sol.Path));
            }
            var choice = Input.ReadString("Select Solution for which snapshot has to be created\n", options);
            string[] separators = { "=" };
            string[] soln = choice.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            //Input.ReadString("1 Sol");
            //Input.ReadString("2 Sol                
            EFLib.Create(soln[0].Trim(), soln[1].Trim());
            Output.WriteLine(System.ConsoleColor.Green, "New Snapshot for {0} added to Repository!", soln[0]);
            Input.ReadString("Press [Enter] to go to Main Menu");
            Program.NavigateHome();



        }
    }
}