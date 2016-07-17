using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Owin.Hosting;
using Owin;

namespace CodeLightr.Server
{
    [ExcludeFromCodeCoverage]
    internal class WepAppWrapper : IWepAppWrapper
    {
        public IDisposable Start(string url, Action<IAppBuilder> configure)
        {
            return WebApp.Start(url, configure);
        }
    }
}