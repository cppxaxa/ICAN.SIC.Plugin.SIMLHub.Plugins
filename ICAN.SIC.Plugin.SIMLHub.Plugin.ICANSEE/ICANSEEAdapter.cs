using ICAN.SIC.Abstractions;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
            Thread.Sleep(2000);
            string userFormattedParam = simlContext.Element.Value;

            List<string> functionalParameters = new List<string>();

            int count = 0;
            var textNode = simlContext.Element.FirstNode;
            while (textNode != null)
            {
                string line = textNode.ToString();
                foreach (var chunk in line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    functionalParameters.Add(chunk);
                }

                textNode = textNode.NextNode;

                count++;
                if (count > 100)
                    break;
            }

            if (functionalParameters.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ICANSEEAdapter] No parameters provided. Kindly check <icansee:Parameter> tag under xpath //Siml/Concept/Model/Response. e.g. ControlFunction.ExecutePreset Preset1 2");
                Console.ResetColor();
                return string.Empty;
            }
            else
            {
                Console.Write("[ICANSEEAdapter] Requesting: ");
                foreach (var item in functionalParameters)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }

            bool success = Enum.TryParse(functionalParameters[0].Substring(functionalParameters[0].LastIndexOf('.') + 1), out Abstractions.IMessageVariants.ICANSEE.ControlFunction controlFunction);

            if (!success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ICANSEEAdapter] Unrecognized ControlFunction. Kindly check <icansee:Parameter> tag under xpath //Siml/Concept/Model/Response to match with 'ControlFunction' enum value. e.g. ControlFunction.ExecutePreset Preset1 2");
                Console.ResetColor();
                return string.Empty;
            }

            // InputMessage message = new InputMessage(Abstractions.IMessageVariants.ICANSEE.ControlFunction.ExecutePreset, new List<string> { "Preset1", "2" });

            functionalParameters = functionalParameters.GetRange(1, functionalParameters.Count - 1);
            InputMessage message = new InputMessage(controlFunction, functionalParameters);
            Hub.Publish<InputMessage>(message);

            return string.Empty;
        }
    }
}
