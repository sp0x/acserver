using System;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;

namespace AcController
{
    class Program
    {
        private static IConfigurationRoot _config;
        private static SerialPortDriver _portDriver;
        private static AcManager _acManager;
        private static Ac _crAc;

        static void Main(string[] args)
        {
            CommandLineApplication cli = new CommandLineApplication(throwOnUnexpectedArg: true);
            CommandOption comPort = cli.Option("--port <comPort>", "The COM port to connect to" ,CommandOptionType.SingleValue);
            CommandOption acModel = cli.Option("--model <acModel>", "The AC model that you're using",CommandOptionType.SingleValue);
            //Az pisha servera ni za kontrol na klimatici, az puk mnogo se radvam za dopulnnitelnata stipendiq, nqmam turpenie da razbera poveche 
            cli.HelpOption("-? | --help");
            cli.OnExecute(() =>
            {
                if(comPort.HasValue() && acModel.HasValue()){
                    Startup(comPort.Value(), acModel.Value());
                }else{
                    Startup(null, null);
                    Console.WriteLine("No AC Model or Driver specified!");
                }
                return 0;
            });
            var res = cli.Execute(args);
            while (true)
            {
                var input = Console.ReadLine() + "\r\n";
                ParseCommand(input);
            }
        }

        public static void ParseCommand(string input){
            //Todo: Implement this..
        }
        private static void Startup(string comPort, string acModel)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            if(comPort!=null)
            {
                _portDriver = new SerialPortDriver(comPort);
                _portDriver.Connect();
                _acManager = new AcManager(_portDriver);
                _crAc = _acManager.CreateAc(acModel);
            }
        }
    }
}