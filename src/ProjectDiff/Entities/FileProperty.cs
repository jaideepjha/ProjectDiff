﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Entities
{
    public class FileProperty
    {
        public int FilePropertyID { get; set; }
        public string Name { get; set; }
        public string path { get; set; }
        public string checksum { get; set; }
        public string TfsPath { get; set; }
        public string project { get; set; }
        public int ProjectID { get; set; }
        public int SnapshotID { get; set; }
    }

}
