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
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            SIMLHub simlHub = new SIMLHub();
            simlHub.Hub.Subscribe<IBotResult>(PrintBotResult);
            simlHub.Hub.Subscribe<IUserResponse>(PrintUserResponse);

            //simlHub.Hub.Publish<IUserResponse>(new UserResponse("Run preset Preset1 with camera 2"));
            simlHub.Hub.Publish<IUserResponse>(new UserResponse("Unload camera 2 used with preset Preset1"));
            // simlHub.Hub.Publish<IUserResponse>(new UserResponse("Run process 0"));

            Console.WriteLine("Done");
            Console.Read();
        }

        static void PrintUserResponse(IUserResponse userResponse)
        {
            Console.WriteLine("UserResponse: " + userResponse.Text);
        }

        static void PrintBotResult(IBotResult botResult)
        {
            Console.WriteLine("BotResult: " + botResult.Text);
        }
    }
}
