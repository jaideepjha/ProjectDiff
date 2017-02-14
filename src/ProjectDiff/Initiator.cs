using ProjectDiff.Entities;
using ProjectDiff.Lib;
using ProjectDiff.Utilities;
using System;
using System.Collections.Generic;
using ProjectDiff.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using EasyConsole;
using ProjectDiff.Menus;

namespace ProjectDiff
{
    public class Initiator : Program
    {
        public Initiator() : base("Solution Diff", breadcrumbHeader: true)
        {
            AddPage(new MainMenu(this));
            AddPage(new AddToSolDiff(this));
            AddPage(new CreateSnapshot(this));
            AddPage(new CompareSnapshotPage(this));
            AddPage(new SnapshotPageFirst(this));
            AddPage(new RemoveSolutionPage(this));
            AddPage(new RemoveSnapshotPage(this));
            AddPage(new SnapshotPageSecond(this));

            SetPage<MainMenu>();
        }
        //public static void Main(string[] args)
        //{
            //EFLib.Create();
            //EFLib.ClearSnapshot("2017-01-17 14:24:06.5581164");
            //EFLib.Clear();
            //List <Project> projects = EFLib.GetSnapshotProjects(5


            //List<FileProperty> files = EFLib.GetSnapshotFiles(1);
            //Snapshot sn = EFLib.GetSnapshot(1);
            //InterimAnalysisResults asr1 = new InterimAnalysisResults();
            //asr1.fp = files;
            //asr1.SnapshotID = sn.SnapshotID;
            //asr1.Timestamp = sn.Timestamp;

            //List<FileProperty> files2 = EFLib.GetSnapshotFiles(2);
            //Snapshot sn2 = EFLib.GetSnapshot(2);
            //InterimAnalysisResults asr2 = new InterimAnalysisResults();
            //asr2.fp = files2;
            //asr2.SnapshotID = sn2.SnapshotID;
            //asr2.Timestamp = sn2.Timestamp;

            //if (DateTime.Compare(sn.Timestamp, sn2.Timestamp) < 0)
            //{
            //    asr1.when = Chronology.Earlier;
            //    asr2.when = Chronology.Later;
            //}
            //else
            //{
            //    asr1.when = Chronology.Later;
            //    asr2.when = Chronology.Earlier;
            //}

            //CompareSnapshots.Compare(asr1, asr2);

            //List<FileProperty> files = EFLib.GetSnapshotFiles(5);
            //foreach (var f in files)
            //{
            //    Console.WriteLine(f.path);
            //}

            //Console.ReadKey();
        //}
    }
}
