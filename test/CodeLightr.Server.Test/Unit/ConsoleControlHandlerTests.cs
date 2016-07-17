using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeLightr.Server.Test.Unit
{
    [TestFixture]
    class ConsoleControlHandlerTests
    {
        [Test]
        public void CanSubscriveToSpecificEvent()
        {
            var handler = new Handler();
            var SUT = new ConsoleControlHandler();
            SUT.Subscribe(handler.Handle, ConsoleControlHandler.CtrlTypes.CTRL_BREAK);

            // First simulate a different ctrl type
            SUT._SimulateConsoleCtrl(ConsoleControlHandler.CtrlTypes.CTRL_CLOSE);

            // Should not have fired
            Assert.IsFalse(handler.WasCalled, "Handler should not have been called");

            // Now simulate the targetted ctrl type
            SUT._SimulateConsoleCtrl(ConsoleControlHandler.CtrlTypes.CTRL_BREAK);
            Assert.IsTrue(handler.WasCalled, "Handler should have been called");
        }

        [Test]
        public void ByDefaultSubscribesToAllEvents()
        {
            var handler = new Handler();
            var SUT = new ConsoleControlHandler();
            SUT.Subscribe(handler.Handle);

            // First simulate a random ctrl type
            SUT._SimulateConsoleCtrl(ConsoleControlHandler.CtrlTypes.CTRL_CLOSE);

            // Should have fired
            Assert.IsTrue(handler.WasCalled, "Handler should have been called");
            handler.Reset();
            
            // Now simulate a different ctrl type
            SUT._SimulateConsoleCtrl(ConsoleControlHandler.CtrlTypes.CTRL_BREAK);
            Assert.IsTrue(handler.WasCalled, "Handler should have been called");
        }

        private class Handler
        {
            public bool WasCalled { get; private set; }

            public void Reset()
            {
                WasCalled = false;
            }

            public void Handle()
            {
                WasCalled = true;
            }
        }
    }
}
