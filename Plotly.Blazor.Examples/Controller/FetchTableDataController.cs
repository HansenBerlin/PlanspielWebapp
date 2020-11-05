using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Plotly.Blazor.Examples.Controller
{
    public class FetchTableDataController
    {      
        public static double ReadValueFromXML(string tableName, int gameRound, int companyID, string searchForKey)
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
                            return XmlConvert.ToDouble(element.Value);
                        }
                    }
                } 
            }
            return 0;
        }
    }
}
