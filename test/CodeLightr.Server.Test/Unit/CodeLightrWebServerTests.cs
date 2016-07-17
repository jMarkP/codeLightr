using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Owin;

namespace CodeLightr.Server.Test.Unit
{
    [TestFixture]
    class CodeLightrWebServerTests
    {
        [Test]
        public void StartsWebServerWithPassedInUrl()
        {
            var server = new Mock<IOwinServer>();
            var ctrlHandler = new Mock<IConsoleControlHandler>();
            var SUT = new CodeLightrWebServer(server.Object, ctrlHandler.Object);

            var url = "http://+:999/TEST";

            SUT.Run(url);

            server.Verify(x => x.Run(url, It.IsAny<Action<IAppBuilder>>()), Times.Once);
        }

        [Test]
        public void StopsWhenAControlSignalIsRaised()
        {
            var server = new Mock<IOwinServer>();
            var ctrlHandler = new Mock<IConsoleControlHandler>();
            Action action = null;
            ctrlHandler.Setup(x => x.Subscribe(It.IsAny<Action>(), It.IsAny<ConsoleControlHandler.CtrlTypes[]>()))
                .Callback((Action a, ConsoleControlHandler.CtrlTypes[] _) => action = a);

            var SUT = new CodeLightrWebServer(server.Object, ctrlHandler.Object);
            var url = "http://+:999/TEST";
            SUT.Run(url);
            ctrlHandler.Verify(x => x.Subscribe(It.IsAny<Action>(), new ConsoleControlHandler.CtrlTypes[0]), Times.Once);

            Assert.IsNotNull(action, "Server should have subscribed to console control handler");

            // Trigger the action - which we hope should trigger the IOwinServer to be stopped
            action();
            server.Verify(x => x.Stop(), Times.Once);
        }
    }
}
