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

            GeoLocationRequest request = new GeoLocationRequest(input);
            hub.Publish<GeoLocationRequest>(request);

            return string.Empty;
        }
    }
}
