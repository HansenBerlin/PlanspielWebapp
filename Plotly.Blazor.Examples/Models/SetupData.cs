using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class SetupData
    {
        public static int CurrentGameRound { get; set; } = 1;
        public static double CurrentWage { get; set; } = 17250;
        public static int CurrentWorkers { get; set; } = 250;
    }
}
