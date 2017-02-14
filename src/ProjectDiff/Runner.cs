using ProjectDiff.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff
{
    public class Runner
    {
        static void Main(string[] args)
        {
            SnapshotHolder.SS = new List<Entities.Snapshot>();
            new Initiator().Run();
        }
    }
}
