using ICAN.SIC.Abstractions;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.ICANSEE
{
    public class ICANSEEAdapter : AbstractPlugin, IAdapter
    {
        public ICANSEEAdapter() : base("SIMLHubPlugin.ICANSEEAdapter")
        {

        }

        public bool IsRecursive { get { return true; } }

        public XName TagName
        {
            get
            {
                XNamespace ns = "http://ican.sic/namespace#icansee";
                return ns + "Parameter";
            }
        }

        public string Evaluate(Context simlContext)
        {
            string userFormattedParam = simlContext.Element.Value;

            List<string> functionalParameters = new List<string>();

            foreach (var param in userFormattedParam.Split(new string[] { " with camera " }, StringSplitOptions.None))
            {
                functionalParameters.Add(param);
            }

            Console.Write("[ICANSEEAdapter] Requesting to execute preset for ");
            foreach (var item in functionalParameters)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            // InputMessage message = new InputMessage(Abstractions.IMessageVariants.ICANSEE.ControlFunction.ExecutePreset, new List<string> { "Preset1", "2" });

            InputMessage message = new InputMessage(Abstractions.IMessageVariants.ICANSEE.ControlFunction.ExecutePreset, functionalParameters);
            Hub.Publish<InputMessage>(message);

            return string.Empty;
        }
    }
}
