using EasyConsole;
using ProjectDiff.Lib;
using ProjectDiff.Menus;
using System;
using System.Collections.Generic;

namespace ProjectDiff
{
    class CompareSnapshotPage : Page
    {
        //private Initiator initiator;

        public CompareSnapshotPage(Program program): base("Compare Snapshot", program)
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
            var choice = Input.ReadString("Select Solution for which snapshot comparison has to be performed\n", options);
            string[] separators = { "="};
            string[] soln = choice.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var snapshots = EFLib.GetSnapshots(soln[0].Trim(), soln[1].Trim());
            //SnapshotHolder sh = new SnapshotHolder();
            if (SnapshotHolder.SS != null || SnapshotHolder.SS.Count > 0)
            {
                SnapshotHolder.SS.Clear();
            }
            else
            {
                SnapshotHolder.SS = new List<Entities.Snapshot>();
            }
            SnapshotHolder.SS.AddRange(snapshots);
            Program.NavigateTo<SnapshotPageFirst>();
        }
    }
}