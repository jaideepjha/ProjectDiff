using EasyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDiff.Menus
{
    public class MainMenu : MenuPage
    {
        public MainMenu(Program program) : base("Main Menu", program,
            new Option("Add Solution to Repository", ()=>program.NavigateTo<AddToSolDiff>()),
            new Option("Create Snapshot", ()=>program.NavigateTo<CreateSnapshot>()),
            new Option("Compare Snapshots", () => program.NavigateTo<CompareSnapshotPage>()),
            new Option("Remove Solution", () => program.NavigateTo<RemoveSolutionPage>()),
            new Option("Remove Snapshot", () => program.NavigateTo<RemoveSnapshotPage>()),
            new Option("Exit", () => Environment.Exit(0))
            )
        {

        }
    }
}
