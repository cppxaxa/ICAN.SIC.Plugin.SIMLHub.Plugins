using ICAN.SIC.Abstractions;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.ICANGEOLOCATE
{
    public class ICANGEOLOCATEAdapter : AbstractPlugin, IAdapter
    {
        public ICANGEOLOCATEAdapter() : base("SIMLHubPlugin.ICANGEOLOCATEAdapter")
        {

        }

        public bool IsRecursive => false;

        public XName TagName
        {
            get
            {
                XNamespace ns = "http://ican.sic/namespace#icangeolocate";
                return ns + "Parameter";
            }
        }

        public override void Dispose()
        {
            
        }

        public string Evaluate(Context simlContext)
        {
            string input = simlContext.Element.Value;

            string waitingMessage = simlContext.Element.PreviousNode.ToString();
            string[] breakWaitingMessage = waitingMessage.Split(',');

            List<string> paramsProvided = null;

            // May have params
            if (breakWaitingMessage.Length > 1)
            {
                // check if the first element ends with "params"
                if (breakWaitingMessage[0].Trim().ToLower().EndsWith("params"))
                {
                    paramsProvided = new List<string>();

                    for (int i = 1; i < breakWaitingMessage.Length; i++)
                        paramsProvided.Add(breakWaitingMessage[i].Trim());
                }
            }

            // Append if params provided
            if (paramsProvided != null)
            {
                foreach (var param in paramsProvided)
                {
                    input += "," + param;
                }
            }

            GeoLocationRequest request = new GeoLocationRequest(input);
            hub.Publish<GeoLocationRequest>(request);

            return string.Empty;
        }
    }
}
