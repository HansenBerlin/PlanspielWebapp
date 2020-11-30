using Plotly.Blazor.Examples.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Models
{
    public class BilanzData
    {
        public static int GameRoundToCheckFor { get; set; } = SetupData.CurrentGameRound-1;
        public static double PCMachinesValue => FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "ValueLeftForAllPCMachines");
        public static double PLTMachinesValue => FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "ValueLeftForAllPLTMachines");
        public static double Chips1Value => FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "Chip1Price")
            * FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "Chip1Storage");
        public static double Chips2Value => FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "Chip2Price")
            * FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "Chip2Storage");
        public static double PLTValue => FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "PLTPrice")
            * FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "PLTStorage");
        public static double PCValue => CalculatePCPerPiece()
            * FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "PCStorage");
        public static double AccountPositive => CalculateAccount();
        public static double AccountNegative => CalculateDebt();
        public static double Bilanzsum => PCMachinesValue + PLTMachinesValue + Chips1Value + Chips2Value + PLTValue + PCValue + AccountPositive;
        public static double WinSumThisRound => Bilanzsum - AccountNegative;

        public BilanzData()
        {

        }

        private static double CalculatePCPerPiece()
        {
            if (FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "PCStorage") == 0 ||
                FetchTableDataController.ReadValueFromXML("marketData.xml", GameRoundToCheckFor - 1, 1, "OutputPC") == 0) return 0;
            double costAllPCs = ((FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "PCMachinesAvailable") * 2250000)
                + (FetchTableDataController.ReadValueFromXML("generalData.xml", GameRoundToCheckFor - 1, 1, "CurrentWage")
                * FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "PCMachinesAvailable") * 20)
                + (FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "Chip1Storage") * 0.1)
                + (FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "Chip2Storage") * 0.5)
                + (FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "PLTStorage") * 10)
                + (FetchTableDataController.ReadValueFromXML("marketData.xml", GameRoundToCheckFor, 1, "Marketing") + 1000000))
                / FetchTableDataController.ReadValueFromXML("marketData.xml", GameRoundToCheckFor, 1, "OutputPC");

            double costPerPC = FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "Chip1Price") * 15
                + FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "Chip2Price") * 9
                + FetchTableDataController.ReadValueFromXML("companyProductionData.xml", GameRoundToCheckFor, 1, "PLTPrice") * 5;

            return costAllPCs + costPerPC;
        }

        private static double CalculateAccount()
        {
            double currentAccount = FetchTableDataController.ReadValueFromXML("marketData.xml", GameRoundToCheckFor, 1, "Account");
            if (currentAccount > 0) return currentAccount;
            else return 0;
        }

        private static double CalculateDebt()
        {
            double currentAccount = FetchTableDataController.ReadValueFromXML("marketData.xml", GameRoundToCheckFor, 1, "Account");
            if (currentAccount < 0) return currentAccount*-1;
            else return 0;
        }
    }
}
