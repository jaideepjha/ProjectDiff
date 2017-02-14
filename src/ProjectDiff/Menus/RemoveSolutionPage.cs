using EasyConsole;
using ProjectDiff.Lib;
using System;
using System.Collections.Generic;

namespace ProjectDiff
{
    class RemoveSolutionPage : Page
    {
        public RemoveSolutionPage(Program program) : base("Remove Solution from Repository", program)
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
            var choice = Input.ReadString("Select Solution which you wish to remove from Repository\n", options);
            string[] separators = { "=" };
            string[] soln = choice.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            EFLib.ClearSolution(soln[0].Trim(), soln[1].Trim());           
            Output.WriteLine(System.ConsoleColor.Green, "Solution {0} removed from Repository!", soln[0]);
            Input.ReadString("Press [Enter] to go to Main Menu");
            Program.NavigateHome();
        }

    }
}