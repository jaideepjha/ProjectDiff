using EasyConsole;
using ProjectDiff.Entities;
using ProjectDiff.Lib;
using ProjectDiff.Menus;
using ProjectDiff.Utilities;
using ProjectDiff.ViewModels;
using System;
using System.Collections.Generic;

namespace ProjectDiff
{
    class SnapshotPageSecond : Page
    {
        public SnapshotPageSecond(Program program):base("Remove Snapshot from Repository", program)
        {

        }
        public override void Display()
        {
            base.Display();

            var sols = SnapshotHolder.SS;
            List<string> options = new List<string>();
            foreach (var sol in sols)
            {
                options.Add(String.Format("{0} ==== {1}", sol.SnapshotID, sol.Timestamp));
            }
            var firstChoice = Input.ReadString("Select first snapshot: ", options);
            string[] separators = { "=" };
            string[] fSnap = firstChoice.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            //Input.ReadString("1 Sol");
            //Input.ReadString("2 Sol         
            long Id1 = Convert.ToInt64(fSnap[0].Trim());
            List<FileProperty> files = EFLib.GetSnapshotFiles(Id1);
            EFLib.ClearSnapshot(Id1);
            Output.WriteLine(System.ConsoleColor.Green, "Snapshot removed from repository");


            //Output.WriteLine(System.ConsoleColor.Green, "New Snapshot for {0} added to Repository!", soln[0]);
            Input.ReadString("Press [Enter] to go to Main Menu");
            Program.NavigateHome();



        }
    }
}