using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class SnapshotProject
    {
        public long SnapshotID { get; set; }
        public Snapshot Snapshot { get; set; }
        public long ProjectID { get; set; }
        public Project Project { get; set; }
    }
}
