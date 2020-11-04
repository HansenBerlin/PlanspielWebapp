using Plotly.Blazor.Examples.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class ChipTypeTwo : IBaseRessouces
    {
        public double PricePerUnit { get; set; }
        public double CurrentStorage { get; set; }
        public double StorageCosts { get; set; }



        public double CalculateCurrentCost()
        {
            double boughtInLastRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "Chip2Bought");
            double lastBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Chip2Price")*
                (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
            StorageCosts = lastBasePrice * 0.5;
            double priceWithDiscount = lastBasePrice - (lastBasePrice / 10);
            if (boughtInLastRound >= 1000000) return PricePerUnit = priceWithDiscount + 0.5;
            else return PricePerUnit = lastBasePrice + 0.5;
        }

        public ChipTypeTwo()
        {
            CurrentStorage = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "Chip2Storage");            
            PricePerUnit = CalculateCurrentCost();
        }
    }
}
