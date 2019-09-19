using ICAN.SIC.Abstractions.IMessageVariants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAN.SIC.Plugin.SIMLHub.Plugin.ICANGEOLOCATE
{
    public class GeoLocationRequest : IGeoLocationRequest
    {
        public GeoLocationRequest()
        {

        }

        public GeoLocationRequest(string commandText)
        {
            this.commandText = commandText;
        }

        string commandText;

        public string CommandText { get => commandText; set => commandText = value; }
    }
}
