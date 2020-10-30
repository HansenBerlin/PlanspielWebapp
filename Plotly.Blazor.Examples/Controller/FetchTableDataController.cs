using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlanspielWebApp.Controller
{
    public class FetchTableDataController
    {
        public FetchTableDataController()
        {

        }

        public static int ReadValueFromXML(string tableName, int gameRound, int companyID, string searchForKey)
        {
            XDocument doc = XDocument.Load("Tables\\" + tableName);

            foreach (XElement el in doc.Root.Elements())
            {
                if (el.Attribute("round").Value == gameRound.ToString() && el.Attribute("companyID").Value == companyID.ToString())
                {
                    foreach (XElement element in el.Elements())
                    {
                        if(element.Name == searchForKey)
                        {
                            return int.Parse(element.Value);                            
                        }
                    }
                } 
            }
            return 0;
        }
    }
}
