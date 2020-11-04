using Plotly.Blazor.Examples.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class ChipTypeOne : IBaseRessouces
    {
        public double PricePerUnit { get; set; }
        public double CurrentStorage { get; set; }
        public double StorageCosts { get; set; }


        public double CalculateCurrentCost()
        {
            double boughtInLastRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "Chip1Bought");
            double lastBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound-1, 1, "Chip1Price")*
                (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
            double priceWithDiscount = lastBasePrice-(lastBasePrice/10);
            if (boughtInLastRound >= 1500000) return PricePerUnit = priceWithDiscount + 0.1;
            else return PricePerUnit = lastBasePrice + 0.1;
        }

        public ChipTypeOne()
        {
            CurrentStorage = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "Chip1Storage");
            PricePerUnit = CalculateCurrentCost();
        }
    }
}
