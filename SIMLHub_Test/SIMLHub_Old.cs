using ICAN.SIC.Abstractions;
using ICAN.SIC.Abstractions.IMessageVariants;
using ICAN.SIC.PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syn.Bot.Siml;
using System.Reflection;
using Syn.Bot.Siml.Interfaces;
using ICAN.SIC.Plugin.SIMLHub.DataTypes;

namespace ICAN.SIC.Plugin.SIMLHub
{
    public class SIMLHub_Old : AbstractPlugin, ISIMLHub
    {
        SIMLHubHelper_Old helper = new SIMLHubHelper_Old();
        SIMLHubUtility_Old utility = new SIMLHubUtility_Old();

        SimlBot bot;
        BotUser currentUser = null;

        // Siml Bot info
        int adapterCount = 0;
        Dictionary<string, List<string>> pluginAdapterPathAndTypes = null;

        public int AdapterCount { get { return adapterCount; } }
        public List<string> LoadedDLLPath
        {
            get
            {
                List<string> loadedDllPath = new List<string>();
                foreach (var adapterPair in pluginAdapterPathAndTypes)
                {
                    loadedDllPath.Add(adapterPair.Key);
                }
                return loadedDllPath;
            }
        }

        public SIMLHub_Old() : base("SIMLHub")
        {
            bot = new SimlBot();

            // Soon it will be substracted
            this.adapterCount = bot.Adapters.Count;

            // Add all adapters
            pluginAdapterPathAndTypes = helper.GetAllSIMLHubPluginIndexAdapterPathAndTypes();
            Console.Write("[SIMLHub] Adapters loading : ");
            foreach (var adapterPair in pluginAdapterPathAndTypes)
            {
                Assembly assembly = Assembly.LoadFrom(adapterPair.Key);

                foreach (var typename in adapterPair.Value)
                {
                    try
                    {
                        IAdapter adapter = (IAdapter)assembly.CreateInstance(typename);

                        AbstractPlugin simlHubPlugin = (AbstractPlugin)adapter;
                        this.Hub.PassThrough(simlHubPlugin.Hub);

                        // Add to bot in not null
                        if (adapter != null)
                        {
                            bot.Adapters.Add(adapter);
                        }
                    }
                    catch { /*Ignore*/ }
                }
            }

            Console.WriteLine("success");

            // Now final value is set
            this.adapterCount = bot.Adapters.Count - this.adapterCount;

            // Add all index.siml files
            Console.Write("[SIMLHub] Index siml merge : ");
            string mergedIndexSimlPackage = helper.GetAllIndexSimlPackage();
            Console.WriteLine("success");


            Console.Write("[SIMLHub] Merged index siml : ");
            bot.PackageManager.LoadFromString(mergedIndexSimlPackage);
            Console.WriteLine("success");

            // Subscribe to IUserResponse for input
            hub.Subscribe<IUserResponse>(this.GetBotReponse);
        }

        private void GetBotReponse(IUserResponse message)
        {
            ChatResult result;

            if (currentUser == null)
                result = bot.Chat(message.Text);
            else
                result = bot.Chat(new ChatRequest(message.Text, currentUser));

            Console.WriteLine("PrintMessage: " + result.BotMessage);

            IBotResult botResponse = new BotResult(result);

            hub.Publish<IBotResult>(botResponse);
        }
    }
}
