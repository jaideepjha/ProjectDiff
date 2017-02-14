using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ProjectDiff.Utilities
{
    public class ParseSln
    {
        public class SolutionProject
        {
            public string ParentProjectGuid;
            public string ProjectName;
            public string RelativePath;
            public string ProjectGuid;

            public string AsSlnString()
            {
                return "Project(\"" + ParentProjectGuid + "\") = \"" + ProjectName + "\", \"" + RelativePath + "\", \"" + ProjectGuid + "\"";
            }
        }

        /// <summary>
        /// .sln loaded into class.
        /// </summary>
        public class Solution
        {
            public List<object> slnLines;       // List of either String (line format is not intresting to us), or SolutionProject.

            /// <summary>
            /// Loads visual studio .sln solution
            /// </summary>
            /// <param name="solutionFileName"></param>
            /// <exception cref="System.IO.FileNotFoundException">The file specified in path was not found.</exception>
            public Solution(string solutionFileName)
            {
                slnLines = new List<object>();
                String slnTxt = File.ReadAllText(solutionFileName);
                string[] lines = slnTxt.Split('\n');
                //Match string like: Project("{66666666-7777-8888-9999-AAAAAAAAAAAA}") = "ProjectName", "projectpath.csproj", "{11111111-2222-3333-4444-555555555555}"
                Regex projMatcher = new Regex("Project\\(\"(?<ParentProjectGuid>{[A-F0-9-]+})\"\\) = \"(?<ProjectName>.*?)\", \"(?<RelativePath>.*?)\", \"(?<ProjectGuid>{[A-F0-9-]+})");

                Regex.Replace(slnTxt, "^(.*?)[\n\r]*$", new MatchEvaluator(m =>
                {
                    String line = m.Groups[1].Value;

                    Match m2 = projMatcher.Match(line);
                    if (m2.Groups.Count < 2)
                    {
                        slnLines.Add(line);
                        return "";
                    }

                    SolutionProject s = new SolutionProject();
                    foreach (String g in projMatcher.GetGroupNames().Where(x => x != "0")) /* "0" - RegEx special kind of group */
                        s.GetType().GetField(g).SetValue(s, m2.Groups[g].ToString());

                    slnLines.Add(s);
                    return "";
                }),
                    RegexOptions.Multiline
                );
            }

            public List<SolutionProject> GetProjects(bool bGetAlsoFolders = false)
            {
                var q = slnLines.Where(x => x is SolutionProject).Select(i => i as SolutionProject);

                if (!bGetAlsoFolders)  // Filter away folder names in solution.
                    q = q.Where(x => x.RelativePath != x.ProjectName);

                return q.ToList();
            }
        }

        static List<string> GetProjectFiltered(bool web, string slnpath)
        {
            //String projectFile = @"E:\codebase\i4a\Dev-I4\Src\eServices\Application\CPF.eServices.Application.sln";
            List<string> p = new List<string>();
            try
            {
                Solution s = new Solution(slnpath);
                foreach (var proj in s.GetProjects())
                {
                    p.Add(Path.Combine(Path.GetDirectoryName(slnpath), proj.RelativePath));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return p;
        }

        public static List<string> GetProjects(string slnpath)
        {
            List<string> p = new List<string>();
            try
            {
                Solution s = new Solution(slnpath);
                foreach (var proj in s.GetProjects())
                {
                    p.Add(Path.Combine(Path.GetDirectoryName(slnpath), proj.RelativePath));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return p;
            //s = GetProjectFiltered(includeWeb, slnpath);
            //List<string> res = new List<string>();
            ////var t = new List<string>();
            //if (includeWeb)
            //{
            //    var tr = s.Where(item => item.Contains("Web") && !item.Contains("Test"));
            //    foreach (string r1 in tr)
            //    {
            //        res.Add(r1);
            //    }
            //}
            //else
            //{
            //    res = s;
            //}
            //return res;
        }

        public static List<string> GetFilteredProjects(string solutionFile, string projectFolderFilter)
        {
            List<string> filteredProjectsList = new List<string>();
            try
            {
                Solution sol = new Solution(solutionFile);
                if (projectFolderFilter.Equals(string.Empty))
                {
                    filteredProjectsList = sol.GetProjects()
                        .Select(proj => Path.Combine(Path.GetDirectoryName(solutionFile), proj.RelativePath)).ToList();
                }
                else
                {
                    filteredProjectsList = sol.GetProjects()
                        .Where(proj => Path.Combine(Path.GetDirectoryName(solutionFile), Path.GetDirectoryName(proj.RelativePath)).Contains(projectFolderFilter))
                        .Select(proj => Path.Combine(Path.GetDirectoryName(solutionFile), proj.RelativePath)).ToList();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return filteredProjectsList;
        }
    }
}
