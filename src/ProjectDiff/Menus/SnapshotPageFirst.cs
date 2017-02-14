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
    class SnapshotPageFirst : Page
    {
        public SnapshotPageFirst(Program program):base("Snapshot List", program)
        {

        }
        public override void Display()
        {
            base.Display();

            var sols = SnapshotHolder.SS;
            if (sols.Count <= 1)
            {
                Output.WriteLine(System.ConsoleColor.Red, "Not enough snapshots for Snapshot comparison!");
                Input.ReadString("Press [Enter] to go to Main Menu");
                Program.NavigateHome();
            }
            List<string> options = new List<string>();
            foreach (var sol in sols)
            {
                options.Add(String.Format("{0} ==== {1}", sol.SnapshotID, sol.Timestamp));
            }
            var firstChoice = Input.ReadString("Select first snapshot: ", options);
            var secondChoice = Input.ReadString("Select second snapshot: ", options);
            string[] separators = { "=" };
            string[] fSnap = firstChoice.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string[] sSnap = secondChoice.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            //Input.ReadString("1 Sol");
            //Input.ReadString("2 Sol         
            long Id1 = Convert.ToInt64(fSnap[0].Trim());
            long Id2 = Convert.ToInt64(sSnap[0].Trim());
            //EFLib.GetSnapshot(Id1);
            if (Id1 == Id2)
            {
                Output.WriteLine(System.ConsoleColor.Red, "Snapshot comparison can't be performed for the same Snapshot ID!");
                Input.ReadString("Press [Enter] to go to Main Menu");
                Program.NavigateHome();
            }

            List<FileProperty> files = EFLib.GetSnapshotFiles(Id1);
            Snapshot sn = EFLib.GetSnapshot(Id1);
            InterimAnalysisResults asr1 = new InterimAnalysisResults();
            asr1.fp = files;
            asr1.SnapshotID = sn.SnapshotID;
            asr1.Timestamp = sn.Timestamp;

            List<FileProperty> files2 = EFLib.GetSnapshotFiles(Id2);
            Snapshot sn2 = EFLib.GetSnapshot(Id2);
            InterimAnalysisResults asr2 = new InterimAnalysisResults();
            asr2.fp = files2;
            asr2.SnapshotID = sn2.SnapshotID;
            asr2.Timestamp = sn2.Timestamp;

            if (DateTime.Compare(sn.Timestamp, sn2.Timestamp) < 0)
            {
                asr1.when = Chronology.Earlier;
                asr2.when = Chronology.Later;
            }
            else
            {
                asr1.when = Chronology.Later;
                asr2.when = Chronology.Earlier;
            }

            var str = CompareSnapshots.Compare(asr1, asr2);
            Output.WriteLine(System.ConsoleColor.Green, "Snapshot comparison results saved at: {0}", str);


            //Output.WriteLine(System.ConsoleColor.Green, "New Snapshot for {0} added to Repository!", soln[0]);
            Input.ReadString("Press [Enter] to go to Main Menu");
            Program.NavigateHome();



        }
    }
}