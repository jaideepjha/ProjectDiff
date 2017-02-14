using EasyConsole;
using ProjectDiff.Lib;
using System;
using System.Collections.Generic;

namespace ProjectDiff.Menus
{
    class RemoveSnapshotPage : Page
    {
        public RemoveSnapshotPage(Program program) : base("Remove Snapshot", program)
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
            var choice = Input.ReadString("Select Solution for which you wisht to remove the snapshot\n", options);
            string[] separators = { "=" };
            string[] soln = choice.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var snapshots = EFLib.GetSnapshots(soln[0].Trim(), soln[1].Trim());
            if (snapshots == null || snapshots.Count == 0)
            {
                Output.WriteLine(System.ConsoleColor.Red, "No snapshots added for this Solution yet!");
                Input.ReadString("Press [Enter] to go to Main Menu");
                Program.NavigateHome();
            }
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
            Program.NavigateTo<SnapshotPageSecond>();

        }
    }
}