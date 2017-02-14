using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class Snapshot
    {
        public long SnapshotID { get; set; }
        public DateTime Timestamp { get; set; }
        public long SolutionID { get; set; }
        public ICollection<SnapshotProject> SnapshotProjects { get; set; }
        public ICollection<SnapshotFileProperty> SnapshotFileProperties { get; set; }
    }
}
