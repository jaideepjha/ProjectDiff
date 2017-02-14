using ProjectDiff.Entities;
using ProjectDiff.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Utilities
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer)
        {
            return first.Where(x => second.Count(y => comparer(x, y)) == 0);
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer)
        {
            return first.Where(x => second.Count(y => comparer(x, y)) == 1);
        }
    }
    public class CompareSnapshots
    {

        public static string Compare(InterimAnalysisResults earlierSnapshot, InterimAnalysisResults laterSnapshot)
        {
            List<FileProperty> efp = earlierSnapshot.when == Chronology.Earlier ? earlierSnapshot.fp : laterSnapshot.fp;
            List<FileProperty> lfp = earlierSnapshot.when == Chronology.Later ? earlierSnapshot.fp : laterSnapshot.fp;
            var Results = new List<AnalysisResults>();

            var unchangedFilesL = (from e in efp
                                  join l in lfp
                                  on new { e.Name, e.project} equals new { l.Name, l.project }
                                  where e.checksum == l.checksum
                                  select l).ToList();
            var unchangedFilesE = (from e in efp
                                   join l in lfp
                                   on new { e.Name, e.project } equals new { l.Name, l.project }
                                   where e.checksum == l.checksum
                                   select e).ToList();
            foreach (var u in unchangedFilesL)
            {
                var res = new AnalysisResults();
                //res.FilePropertyID = u.FilePropertyID;
                res.Checksum = u.checksum;
                res.FileName = u.Name;
                res.Path = u.path;
                res.Project = u.project;
                res.SnapshotID = laterSnapshot.SnapshotID;
                res.Timestamp = laterSnapshot.Timestamp;
                res.Status = "Unchanged";
                Results.Add(res);
            }
            //var unchangedFiles = lfp.Where(x => efp.Select(y => y.checksum).Equals(x.checksum)).ToList();

            var modifiedFilesL = (from l in lfp
                                 join e in efp
                                 on new { l.Name, l.project } equals new { e.Name, e.project }
                                 where l.checksum != e.checksum
                                 select l).ToList();
            var modifiedFilesE = (from l in lfp
                                  join e in efp
                                  on new { l.Name, l.project } equals new { e.Name, e.project }
                                  where l.checksum != e.checksum
                                  select e).ToList();
            //var modifiedFiles = lfp.Where(x => !efp.Select(y => y.checksum).Equals(x.checksum)).ToList();
            foreach (var u in modifiedFilesL)
            {
                var res = new AnalysisResults();
                //res.FilePropertyID = u.FilePropertyID;
                res.Checksum = u.checksum;
                res.FileName = u.Name;
                res.Path = u.path;
                res.Project = u.project;
                res.SnapshotID = laterSnapshot.SnapshotID;
                res.Timestamp = laterSnapshot.Timestamp;
                res.Status = "Modified";
                Results.Add(res);
            }


            var newFiles1 = lfp.Except(unchangedFilesL).ToList();
            var newFiles = newFiles1.Except(modifiedFilesL).ToList();
            //var newFiles = lfp.Where(x => x.path !)
            //List<string> newPaths = lfp.Where(x => x.));
            //var newFiles = (from l in lfp
            //                     join e in efp
            //                     on l.Name equals e.Name
            //                     where l.checksum != e.checksum
            //                     select l).ToList();
            foreach (var u in newFiles)
            {
                var res = new AnalysisResults();
                res.FilePropertyID = u.FilePropertyID;
                res.Checksum = u.checksum;
                res.FileName = u.Name;
                res.Path = u.path;
                res.Project = u.project;
                res.SnapshotID = laterSnapshot.SnapshotID;
                res.Timestamp = laterSnapshot.Timestamp;
                res.Status = "NewlyAdded";
                Results.Add(res);
            }

            var removedFiles1 = efp.Except(unchangedFilesE).ToList();
            var removedFiles = removedFiles1.Except(modifiedFilesE).ToList();
            foreach (var u in removedFiles)
            {
                var res = new AnalysisResults();
                res.FilePropertyID = u.FilePropertyID;
                res.Checksum = u.checksum;
                res.FileName = u.Name;
                res.Path = u.path;
                res.Project = u.project;
                res.SnapshotID = earlierSnapshot.SnapshotID;
                res.Timestamp = earlierSnapshot.Timestamp;
                res.Status = "Removed";
                Results.Add(res);
            }
            ExcelReport excelReport = new ExcelReport();

            string filename = System.IO.Path.GetTempPath()+ String.Format("{0:ddMMyy-hh-mm-ss}", DateTime.Now)+".xlsx";
            excelReport.createExcel(filename, Results, "UnChanged");
            //Console.WriteLine("Snapshot comparison results saved at: {0}",filename);
            return filename;
        }

    }
}
