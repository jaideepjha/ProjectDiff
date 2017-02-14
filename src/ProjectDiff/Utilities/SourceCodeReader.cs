using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectDiff.Utilities
{
    public class SourceCodeReader
    {
        private readonly List<string> _sourceFiles = new List<string>();
        private readonly List<string> _csHtmlFilesList = new List<string>();
        // public string projectNameSpace { get; set; }

        public List<string> SourceFiles
        {
            get { return _sourceFiles; }
        }
        public List<string> CsHtmlFilesList
        {
            get { return _csHtmlFilesList; }
        }


        public SourceCodeReader(string projectFile)
        {
            _sourceFiles = LocateSourceFiles(projectFile);
            _csHtmlFilesList = LocateCshtmlFiles(projectFile);
        }

        public SourceCodeReader(List<string> projectFile)
        {
            foreach (var proj in projectFile)
            {
                _sourceFiles.AddRange(LocateSourceFiles(proj));
            }
        }
        private List<string> LocateSourceFiles(string projectFile)
        {
            var sourceFiles = new List<string>();
            var path = Path.GetDirectoryName(projectFile);//GetDirectory(projectFile);
            if (projectFile.EndsWith("xproj"))
            {
                sourceFiles = RetrieveFiles.GetFiles(path);   
            }
            else
            {

                var projectDefinition = XDocument.Load(projectFile);

                //projectNameSpace = projectDefinition.Descendants()
                //    .Where(n => n.Name.LocalName.Equals("RootNamespace"))
                //    .Select(n => n.Value).First();

                 sourceFiles = projectDefinition.Descendants()
                    .Where(n => n.Name.LocalName.Equals("Compile"))
                    .Select(n => String.Format("{0}\\{1}", path, n.Attribute("Include").Value))
                    .ToList();
            }

            return sourceFiles;
        }

        private List<string> LocateCshtmlFiles(string projectFile)
        {
            var path = GetDirectory(projectFile);

            var projectDefinition = XDocument.Load(projectFile);
            var csHtmlFiles = projectDefinition.Descendants()
                .Where(n => n.Name.LocalName.Equals("Content") && n.Attribute("Include").Value.Contains(".cshtml"))
                .Select(n => String.Format("{0}\\{1}", path, n.Attribute("Include").Value))
                .ToList();

            return csHtmlFiles;
        }

        private string GetDirectory(string file)
        {
            return new FileInfo(file).Directory.FullName;
        }
    }
}
