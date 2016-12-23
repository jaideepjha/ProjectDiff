using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public List<FileProperty> ProjectFiles { get; set; }
        public int SolutionID { get; set; }
        public int SnapshotID { get; set; }
    }
}
