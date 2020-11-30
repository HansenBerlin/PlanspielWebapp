using Plotly.Blazor.Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Plotly.Blazor.Examples.Controller
{
    public class CalculateRankingsController
    {
        
        public static double CheckPosition(string type)
        {
            bool[] checkForRound = new bool[] { false, false, false, false, false, false};
            checkForRound[SetupData.CurrentGameRound-2] = true;
            //for (int i = SetupData.CurrentGameRound-1; i < 6; i++)
            //{
            //    checkForRound[i] = true;
            //}

            double currentPosition = 1;
            if (type == "production")
            {
                type = "OutputPC";
                var listPCs = MergedDataController.GetDataSetFromCompanyTable(checkForRound, new bool[] { true, true, true, true, true, true }, type);
                type = "OutputPLT";
                var listPLT = MergedDataController.GetDataSetFromCompanyTable(checkForRound, new bool[] { true, true, true, true, true, true }, type);

                for (int i = 1; i < 6; i++)
                {
                    if ((Convert.ToDouble(listPCs[i])*20 + Convert.ToDouble(listPLT[i])) 
                        > (Convert.ToDouble(listPCs[0])*20 + Convert.ToDouble(listPLT[0])))
                    {
                        currentPosition++;
                    }
                }
            }
            else if(type == "valueTotal")
            {
                type = "CapacityPC";
                var listPCs = MergedDataController.GetDataSetFromCompanyTable(checkForRound, new bool[] { true, true, true, true, true, true }, type);
                type = "CapacityPLT";
                var listPLT = MergedDataController.GetDataSetFromCompanyTable(checkForRound, new bool[] { true, true, true, true, true, true }, type);

                for (int i = 1; i < 6; i++)
                {        
                    if (Convert.ToDouble(listPCs[i]) *1166 + Convert.ToDouble(listPLT[i])*134 + FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, i, "Account")
                        > Convert.ToDouble(listPCs[0]) * 1166 + Convert.ToDouble(listPLT[0])*134 + FetchTableDataController.ReadValueFromXML("marketData.xml", SetupData.CurrentGameRound - 1, 1, "Account"))
                    {
                        currentPosition++;
                    }
                }

            }
            else
            {
                var listAllValues = MergedDataController.GetDataSetFromCompanyTable(checkForRound, new bool[] { true, true, true, true, true, true }, type);
                for (int i = 1; i < 6; i++)
                {
                    if (Convert.ToDouble(listAllValues[i]) > Convert.ToDouble(listAllValues[0]))
                    {
                        currentPosition++;
                    }
                }
            }            
            return currentPosition;
        }
    }
}
