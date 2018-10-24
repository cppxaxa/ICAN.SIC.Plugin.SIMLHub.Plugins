using ICAN.SIC.Abstractions.IMessageVariants;
using ICAN.SIC.Plugin.SIMLHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIMLHub_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SIMLHub simlHub = new SIMLHub();
            simlHub.Hub.Subscribe<IBotResult>(PrintBotResult);

            simlHub.Hub.Publish<IUserResponse>(new UserResponse("Connect camera 0"));
            simlHub.Hub.Publish<IUserResponse>(new UserResponse("Run process 0"));

            Console.WriteLine("Done");
            Console.Read();
        }

        static void PrintBotResult(IBotResult botResult)
        {
            Console.WriteLine("BotResult: " + botResult.Text);
        }
    }
}
