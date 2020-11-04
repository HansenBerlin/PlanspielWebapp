using Plotly.Blazor.Examples.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class PLT : IBaseRessouces
    {
        public double PricePerUnit { get; set; }
        public double CurrentStorage { get; set; }
        public double StorageCosts { get; set; }
        public double ProducedInLastRound { get; set; }



        public double CalculateCurrentCost()
        {
            var producedPLT = new CalculatePLTProductionController();
            double boughtInLastRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "PLTBought");
            double lastBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "PLTPrice")*
                (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));
            double priceWithDiscount = lastBasePrice - (lastBasePrice / 10);             
            if(ProducedInLastRound > 0)
            {
                if (boughtInLastRound >= 250000) PricePerUnit = priceWithDiscount;
                else PricePerUnit = lastBasePrice;                
                return (((producedPLT.CurrentPLTPrice() * ProducedInLastRound) + (PricePerUnit * boughtInLastRound)) / (ProducedInLastRound + boughtInLastRound))+10; 
            }
            else if (boughtInLastRound >= 250000) return PricePerUnit = priceWithDiscount + 10;
            else return PricePerUnit = lastBasePrice + 10;
        }

        public PLT()
        {
            CurrentStorage = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound, 1, "PLTStorage");
            ProducedInLastRound = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound, 1, "OutputPLT");
            PricePerUnit = CalculateCurrentCost();
        }
    }
}
