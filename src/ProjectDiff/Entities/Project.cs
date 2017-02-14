using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjectDiff.Entities
{
    public class Project
    {
        public long ProjectID { get; set; }
        public string Name { get; set; }
        public ICollection<FileProperty> ProjectFiles { get; set; }
        public long SolutionID { get; set; }
        public ICollection<SnapshotProject> SnapshotProjects { get; set; }
    }
}
