﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Microsoft.Owin.Hosting;
using Owin;

namespace CodeLightr.Server
{
    internal class OwinServer
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (OwinServer));
        private readonly ManualResetEvent stopEvent = new ManualResetEvent(false);

        public void Run(string url, Action<IAppBuilder> configure)
        {
            using (WebApp.Start(url, configure))
            {
                Log.InfoFormat("OWIN Server running at {0}", url);
                stopEvent.WaitOne();
            }
            Log.InfoFormat("OWIN server at {0} stopped", url);
        }

        public void Stop()
        {
            stopEvent.Set();
        }
    }
}
