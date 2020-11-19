using Plotly.Blazor.Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Controller
{
    public class EstimateProductionPurchases
    {
        public EstimateProductionPurchases()
        {

        }

        private double[] CalculateCompetitionMinMaxCostForWorkers(int company)
        {
            double pcCapacity = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "CapacityPC");
            double pltCapacity = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "CapacityPLT");
            double efficiency = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "Efficiency");

            double minCosts = (((pcCapacity / 3000 * 20 + pltCapacity / 7500 * 10) / 80) * efficiency)*SetupData.CurrentWage;
            double maxCosts = (((pcCapacity / 3000 * 20 + pltCapacity / 7500 * 10) / 80) * 100)*SetupData.CurrentWage;

            return new double[] { minCosts, maxCosts };
        }

        public double[,] CalculateMinMaxCostsForAllCompanys()
        {
            double[,] returnDouble = new double[6, 2];

            for (int company = 1; company < 7; company++)
            {
                double pcSales = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "SalesPC")
                    * FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "PricePerPC");                
                double pltSales = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "SalesPLT")
                    * SetupHistoricalData.PPPPLTBuyLastRound;
                double marketingIncludingReport = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "Marketing") + 1000000;
                double costMachinesToRun = ((FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "CapacityPC") / 3000) * 500000)
                    + ((FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "CapacityPLT") / 7500) * 200000);
                double costBoughtMachines = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "CostMachinesBought");
                double saldo = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "Saldo");
                double account = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 2, company, "Account");
                double interestPos = 0;
                double interestNeg = 0;

                if (account > 0) interestPos = ((account / 100) * 4);
                else if (account < 0) interestNeg = (account / 100) * -12;

                double returnIntermediate = (pcSales + pltSales + interestPos) - (marketingIncludingReport + costMachinesToRun
                    + costBoughtMachines + interestNeg + CalculateProductionRessources(company));
                
                returnDouble[company - 1, 0] = returnIntermediate - CalculateCompetitionMinMaxCostForWorkers(company)[0];
                returnDouble[company - 1, 1] = returnIntermediate - CalculateCompetitionMinMaxCostForWorkers(company)[1];
            }
            return returnDouble;
        }

        public double CalculateProductionRessources(int company)
        {
            double pcProduced = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "OutputPC");
            double pltProduced = FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, company, "OutputPLT");
            double returnValue = pcProduced * (15 * SetupHistoricalData.PPPChip1LastRound + 9 * SetupHistoricalData.PPPChip2LastRound + 5 * SetupHistoricalData.PPPPLTBuyLastRound)
                + pltProduced * (9 * SetupHistoricalData.PPPChip1LastRound + 6 * SetupHistoricalData.PPPChip2LastRound);
            return returnValue;


        }
    }        
}
