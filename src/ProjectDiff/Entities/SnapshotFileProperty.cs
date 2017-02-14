using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class SnapshotFileProperty
    {
        public long SnapshotID { get; set; }
        public Snapshot Snapshot { get; set; }
        public long FilePropertyID { get; set; }
        public FileProperty FileProperty { get; set; }
    }
}
