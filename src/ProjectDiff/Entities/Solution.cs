using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class Solution
    {
        public int SolutionID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Project> Projects { get; set; }
    }
}
