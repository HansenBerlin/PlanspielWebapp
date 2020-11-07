using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class TemporaryData
    {
        public static double TemporaryStorageChipOneLeftUnits { get; set; }
        public static double TemporaryStorageChipTwoLeftUnits { get; set; }
        public static double TemporaryStoragePLTLeftUnits { get; set; }
        public static double TemporaryStorageWorkersLeft { get; set; }

        public TemporaryData()
        {            
        }

        public void ResetData()
        {
            TemporaryStorageChipOneLeftUnits = SetupData.Chip1Storage;
            TemporaryStorageChipTwoLeftUnits = SetupData.Chip2Storage;
            TemporaryStoragePLTLeftUnits = SetupData.PLTStorage;
            TemporaryStorageWorkersLeft = SetupData.CurrentWorkers;
        }

    }
}
