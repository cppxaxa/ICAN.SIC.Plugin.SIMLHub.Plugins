using ICAN.SIC.Abstractions.IMessageVariants.ICANSEE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.ICANSEE
{
    public class InputMessage : IInputMessage
    {
        public ControlFunction ControlFunction { get; set; }
        public List<string> Parameters { get; set; }

        public InputMessage()
        {

        }

        public InputMessage(ControlFunction controlFunction, List<string> parameters)
        {
            this.ControlFunction = controlFunction;
            this.Parameters = parameters;
        }
    }
}
