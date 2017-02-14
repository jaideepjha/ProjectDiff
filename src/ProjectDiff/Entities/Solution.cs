using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class Solution
    {
        public long SolutionID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Snapshot> Snapshots { get; set; }
    }
}
