using System;
using Microsoft.Owin.Hosting;
using Owin;

namespace CodeLightr.Server
{
    internal class WepAppWrapper : IWepAppWrapper
    {
        public IDisposable Start(string url, Action<IAppBuilder> configure)
        {
            return WebApp.Start(url, configure);
        }
    }
}