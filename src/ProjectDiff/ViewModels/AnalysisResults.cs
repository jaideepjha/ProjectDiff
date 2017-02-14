using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.ViewModels
{
    public class AnalysisResults
    {
        public string Project { get; set; }
        public long FilePropertyID { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Checksum { get; set; }
        public long SnapshotID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
    }
}
