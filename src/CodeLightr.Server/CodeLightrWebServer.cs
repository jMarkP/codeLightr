using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace CodeLightr.Server
{
    internal class CodeLightrWebServer
    {
        private readonly IOwinServer server;
        private readonly IConsoleControlHandler ctrlHandler;

        public CodeLightrWebServer(IOwinServer server, IConsoleControlHandler ctrlHandler)
        {
            this.server = server;
            this.ctrlHandler = ctrlHandler;
        }

        public void Run(string url)
        {
            ctrlHandler.Subscribe(Stop);
            server.Run(url, Configure);
        }

        private void Stop()
        {
            server.Stop();
        }

        private void Configure(IAppBuilder appBuilder)
        {
        }
    }
}
