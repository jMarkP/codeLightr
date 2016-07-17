using System;
using Owin;

namespace CodeLightr.Server
{
    public interface IWepAppWrapper
    {
        IDisposable Start(string url, Action<IAppBuilder> configure);
    }
}