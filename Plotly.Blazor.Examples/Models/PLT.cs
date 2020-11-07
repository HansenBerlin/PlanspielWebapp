﻿using Plotly.Blazor.Examples.Controller;
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



        public double CalculateLastRoundProductionCosts(int calculateForGameRound)
        {
            var producedPLT = new CalculateProductionController();
            double boughtInLastRound = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound-1, 1, "PLTBought");
            
            double lastBasePrice = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "PLTPrice") *
                (100 / FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound - 1, 1, "Quality"));            
            
            double priceWithDiscount = lastBasePrice - (lastBasePrice / 10);    
            
            if(ProducedInLastRound > 0)
            {
                if (boughtInLastRound >= 250000) PricePerUnit = priceWithDiscount;
                else PricePerUnit = lastBasePrice;                
                return (((producedPLT.LastPLTPrice(calculateForGameRound) * ProducedInLastRound) + (PricePerUnit * boughtInLastRound)) / (ProducedInLastRound + boughtInLastRound))+10; 
            }
            else if (boughtInLastRound >= 250000) return PricePerUnit = priceWithDiscount + 10;
            else return PricePerUnit = lastBasePrice + 10;
        }

        public PLT(int calculateForGameRound)
        {
            CurrentStorage = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", SetupData.CurrentGameRound-1, 1, "PLTStorage");
            ProducedInLastRound = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound-1, 1, "OutputPLT");
            PricePerUnit = CalculateLastRoundProductionCosts(calculateForGameRound);
        }
    }
}
