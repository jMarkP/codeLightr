using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace CodeLightr.Server
{
    internal interface IConsoleControlHandler
    {
        void Subscribe(Action handler, params ConsoleControlHandler.CtrlTypes[] ctrlTypes);
    }

    internal class ConsoleControlHandler : IConsoleControlHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ConsoleControlHandler));
        private Dictionary<CtrlTypes, List<Action>> handlers = new Dictionary<CtrlTypes, List<Action>>();
         
        public ConsoleControlHandler()
        {
            SetConsoleCtrlHandler(ConsoleCtrlCheck, true);
        }

        public void Subscribe(Action handler, params CtrlTypes[] ctrlTypes)
        {
            if (ctrlTypes == null || !ctrlTypes.Any())
            {
                ctrlTypes = Enum.GetValues(typeof (CtrlTypes))
                    .Cast<CtrlTypes>()
                    .ToArray();
            }
            foreach (var ctrlType in ctrlTypes)
            {
                List<Action> handlersForCtrlType;
                if (!handlers.TryGetValue(ctrlType, out handlersForCtrlType))
                {
                    handlersForCtrlType = new List<Action>();
                    handlers[ctrlType] = handlersForCtrlType;
                }
                handlersForCtrlType.Add(handler);
            }
        }

        internal void _SimulateConsoleCtrl(CtrlTypes ctrlType)
        {
            ConsoleCtrlCheck(ctrlType);
        }

        // Declare the SetConsoleCtrlHandler function
        // as external and receiving a delegate.
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool dd);

        // A delegate type to be used as the handler routine 
        // for SetConsoleCtrlHandler.
        private delegate bool HandlerRoutine(CtrlTypes ctrlType);

        // An enumerated type for the control messages
        // sent to the handler routine.
        public enum CtrlTypes
        {
            CTRL_C = 0,
            CTRL_BREAK,
            CTRL_CLOSE,
            CTRL_LOGOFF = 5,
            CTRL_SHUTDOWN
        }

        private bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            Log.InfoFormat("Received {0} signal", ctrlType);

            List<Action> handlersForCtrlType;
            if (handlers.TryGetValue(ctrlType, out handlersForCtrlType))
            {
                foreach (var action in handlersForCtrlType)
                {
                    action();
                }
            }
            return true;
        }
    }
}
