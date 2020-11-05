using Plotly.Blazor.Examples.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class SetupData
    {
        public static int CurrentGameRound { get; set; } = 0;
        public static double CurrentWage { get; set; } = 17250;
        public static int CurrentWorkers { get; set; } = 250;
        public static int PCDemandNextRound { get; set; } = 90;
        public static int Efficiency { get; set; } = 100;
        public static int PCMachinesDyingNextRound { get; set; } = 5;
        public static int PCMachinesDyingInTwoRounds { get; set; } = 0;
        public static int PLTMachinesDyingNextRound { get; set; } = 0;
        public static int PLTMachinesDyingInTwoRounds { get; set; } = 10;

        public SetupData()
        {
            // Always keep GameRound at 1 and change in XML Table
            CurrentGameRound = Convert.ToInt32(FetchTableDataController.ReadValueFromXML("generalData.xml", 1, 1, "CurrentGameRound"));
        }
    }
}
