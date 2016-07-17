using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace CodeLightr.Server.Test
{
    internal class ObservableMiddleware : OwinMiddleware
    {
        public static bool WasInvoked { get; private set; }
        public static List<IOwinContext> ObservedContexts { get; private set; }

        static ObservableMiddleware()
        {
            Reset();
        }

        public static void Reset()
        {
            ObservedContexts = new List<IOwinContext>();
            WasInvoked = false;
        }

        public ObservableMiddleware(OwinMiddleware next) : base(next)
        { 
        }

        public override Task Invoke(IOwinContext context)
        {
            ObservedContexts.Add(context);
            WasInvoked = true;
            return base.Next.Invoke(context);
        }
    }
}