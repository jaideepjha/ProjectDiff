using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectDiff.Entities;

namespace ProjectDiff.ViewModels
{
    public enum Chronology
    {
        Earlier,
        Later
    }
    public class InterimAnalysisResults
    {
        public List<FileProperty> fp { get; set; }
        public long SnapshotID { get; set; }
        public DateTime Timestamp { get; set; }
        public Chronology when { get; set; }

    }
}
