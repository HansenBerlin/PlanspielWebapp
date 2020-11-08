using Plotly.Blazor.Examples.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class SetupHistoricalData
    {
        public static double PCDemandLastRound { get; set; }
        public static double EfficiencyLastRound { get; set; }        
        public static double PPPPCLastRound { get; set; }
        public static double PPPPLTProductionLastRound { get; set; }
        public static double PPPPLTBuyLastRound { get; set; }
        public static double PPPChip1LastRound { get; set; }
        public static double PPPChip2LastRound { get; set; }        
        public static double PLTCapacityLastRound { get; set; }
        public static double PCCapacityLastRound { get; set; }


        public SetupHistoricalData()
        {
            EfficiencyLastRound = FetchTableDataController.ReadValueFromXML("generalData.xml", SetupData.CurrentGameRound - 2, 1, "Efficiency");
            PCDemandLastRound = FetchTableDataController.ReadValueFromXML("generalData.xml", SetupData.CurrentGameRound - 2, 1, "PCDemandThisRound");            

            PPPChip1LastRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Chip1Price");
            PPPChip2LastRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Chip2Price");
            PPPPLTBuyLastRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "PLTPrice");

           
            PCCapacityLastRound = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound -1, 1, "CapacityPC");
            PLTCapacityLastRound = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound -1, 1, "CapacityPLT");

            var pcProductionCalculate = new CalculateProductionController();
            var pltProductionCalculate = new CalculateProductionController();
            var resetTempData = new TemporaryData();

            PPPPCLastRound = Convert.ToDouble(pcProductionCalculate.ShowCurrentProductionCostsPC(PCCapacityLastRound.ToString(),
                FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound-2, 1, "Marketing").ToString(),
                SetupData.CurrentGameRound-1));

            resetTempData.ResetData();

            PPPPLTProductionLastRound = Convert.ToDouble(pltProductionCalculate.ShowCurrentProductionCostsPLT(PLTCapacityLastRound.ToString(), SetupData.CurrentGameRound-1));

            resetTempData.ResetData();
        }
    }
}
