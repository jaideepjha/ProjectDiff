using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class Snapshot
    {
        public int SnapshotID { get; set; }
        public DateTime Timestamp { get; set; }
        public int SolutionID { get; set; }
    }
}
