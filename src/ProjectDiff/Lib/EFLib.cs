using ProjectDiff.Entities;
using ProjectDiff.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProjectDiff.Lib
{
    public static class EFLib
    {
        public static void Create(string name = "ZetaTfsAPI.sln", string path = @"D:\tmp\ZetaTfsApi_source\Source\ZetaTfsApi.sln")
        {
            #region
            using (var context = new ProjDiffDBContext())
            {
                var sol = new Solution();
                var _s = new Solution();
                if (context.Solutions.Count() == 0)
                {
                    sol = new Solution { Name = name, Path = path };
                    _s = sol;
                    context.Solutions.Add(sol);
                }
                else
                {
                    //context.Solutions.Contains(s => s.Name == name && s.Path == path)
                    _s = context.Solutions.FirstOrDefault(s => s.Name == name && s.Path == path);
                    sol = _s != null ? _s : new Solution { Name = name, Path = path };
                }
                //context.Solutions.Add(sol);
                //context.SaveChanges();
                var _snap = new Snapshot { Timestamp = DateTime.Now };
                sol.Snapshots = new List<Snapshot>();
                sol.Snapshots.Add(_snap);

                List<Project> projs = new List<Project>();
                List<string> projects = ParseSln.GetProjects(sol.Path);
                projects.RemoveAll(x => x.ToLower().Contains("test"));
                SourceCodeReader scr;
                List<FileProperty> fp = new List<FileProperty>();
                foreach (var p in projects)
                {
                    scr = new SourceCodeReader(p);
                    foreach (var f in scr.SourceFiles)
                    {
                        var fname = Path.GetFileName(f);
                        var chksm = GenUtil.GetHashForFile(f);
                        var _proj = p;
                        fp.Add(new FileProperty
                        {
                            Name = fname,
                            checksum = chksm,
                            path = f,
                            project = Path.GetFileName(_proj)
                        });
                    }
                }
                projs = new List<Project>();
                foreach (var p in projects)
                {
                    projs.Add(new Project { Name = Path.GetFileName(p) });
                };
                foreach (var p in projs)
                {
                    var f = fp.FindAll(x => x.project == p.Name);
                    p.ProjectFiles = f;
                };
                sol.Projects = projs;

                if (_s == null)
                {
                    context.Solutions.Add(sol);
                }
                context.Projects.AddRange(projs);
                context.Snapshots.Add(_snap);
                context.SaveChanges();

                foreach (var p in projs)
                {
                    var snP = new SnapshotProject
                    {
                        SnapshotID = _snap.SnapshotID,
                        ProjectID = p.ProjectID
                    };
                    context.SnapshotProjects.Add(snP);
                }

                foreach (var f in fp)
                {
                    var snFP = new SnapshotFileProperty
                    {
                        SnapshotID = _snap.SnapshotID,
                        FilePropertyID = f.FilePropertyID
                    };
                    context.SnapshotFileProperties.Add(snFP);
                }
                context.SaveChanges();
            }
            #endregion

        }

        public static void ClearSolution(string name, string path)
        {
            using (var context = new ProjDiffDBContext())
            {
                var sol = context.Solutions.Single(s => s.Name == name && s.Path==path);
                context.Solutions.Remove(sol);
                context.SaveChanges();
            }
        }
        public static void ClearSnapshot(long SnapshotId)
        {
            using (var context = new ProjDiffDBContext())
            {
                var snapshot = context.Snapshots.FirstOrDefault(s => s.SnapshotID==SnapshotId);
                context.Snapshots.Remove(snapshot);
                context.SaveChanges();
            }
        }

        public static List<Project> GetSnapshotProjects(int snapshotId)
        {
            using (var context = new ProjDiffDBContext())
            {
                var snapshot = context.Snapshots.FirstOrDefault(s => s.SnapshotID == snapshotId);
                //var projects = (from s in context.Snapshots
                //                join sp in context.SnapshotProjects on s.SolutionID equals sp.SnapshotID
                //                join p in context.Projects on sp.ProjectID equals p.ProjectID
                //                where s.SnapshotID == snapshotId
                //                select p).ToList<Project>();
                var projects = (from sp in context.SnapshotProjects
                                join p in context.Projects on sp.ProjectID equals p.ProjectID
                                where sp.SnapshotID == snapshotId
                                select p).ToList();
                return projects;
            }
        }

        public static List<FileProperty> GetSnapshotFiles(long snapshotId)
        {
            using (var context = new ProjDiffDBContext())
            {
                var files = (from sfp in context.SnapshotFileProperties
                             join f in context.FileProperties on sfp.FilePropertyID equals f.FilePropertyID
                             where sfp.SnapshotID == snapshotId
                             select f).ToList();
                return files;
            }
        }

        public static Snapshot GetSnapshot(long snapshotId)
        {
            using (var context = new ProjDiffDBContext())
            {
                return context.Snapshots.Single(X => X.SnapshotID == snapshotId);
            }
        }

        public static List<Snapshot> GetSnapshots(string name, string path)
        {
            using (var context = new ProjDiffDBContext())
            {
                //context.Snapshots.Select(x=>x.)
                var snapshots = (from sns in context.Snapshots
                                 join sol in context.Solutions on sns.SolutionID equals sol.SolutionID
                                 where sol.Name == name && sol.Path == path
                                 select sns).ToList();
                return snapshots;
            }
        }

        public static bool SolutionExists(string name, string path)
        {
            using (var context = new ProjDiffDBContext())
            {
                //return context.Snapshots.Single(X => X.SnapshotID == snapshotId);
                var s = context.Solutions.Where(x => x.Name == name && x.Path == path);
                if (s.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static List<Solution> GetSolutions()
        {
            using (var context = new ProjDiffDBContext())
            {
                //return context.Snapshots.Single(X => X.SnapshotID == snapshotId);
                try
                {
                    return context.Solutions.ToList();
                }
                catch (Exception e)
                {
                    return new List<Solution>();
                }
            }

        }



    }
}
