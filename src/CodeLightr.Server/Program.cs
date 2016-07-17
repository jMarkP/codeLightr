using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLightr.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.BasicConfigurator.Configure();
            var server = new CodeLightrWebServer(new OwinServer(new WepAppWrapper()), new ConsoleControlHandler());
            server.Run("http://+:12345/");
        }
    }
}
