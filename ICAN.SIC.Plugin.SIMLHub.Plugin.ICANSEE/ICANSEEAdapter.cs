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

        public override void Dispose()
        {
            
        }

        public string Evaluate(Context simlContext)
        {
            string userFormattedParam = simlContext.Element.Value;

            List<string> functionalParameters = new List<string>();

            int count = 0;
            var textNode = simlContext.Element.FirstNode;
            string fullString = string.Empty;
            while (textNode != null)
            {
                string line = textNode.ToString();
                fullString += line;

                textNode = textNode.NextNode;

                count++;
                if (count > 100)
                    break;
            }

            var stringTokens = fullString.Split(',');
            int index = 0;

            // State 1 - If you don't encounter [Python] string, just treat tokens as param
            while (index < stringTokens.Length)
            {
                string unit = stringTokens[index];

                if (unit.Trim().StartsWith("[Python]"))
                    break;

                functionalParameters.Add(unit);
                index++;
            }

            // State 2 - If you encounter [Python], remove [Python] and treat subsequent units as one
            string subsequentUnit = string.Empty;
            if (index < stringTokens.Length)
            {
                string unit = stringTokens[index];
                subsequentUnit = unit.Trim().Substring("[Python]".Length);
                index++;
            }
            while (index < stringTokens.Length)
            {
                string unit = stringTokens[index];
                subsequentUnit += "," + unit;
                index++;
            }

            if (subsequentUnit.Trim() != string.Empty)
                functionalParameters.Add(subsequentUnit);
            // Done parsing inputs from siml


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
