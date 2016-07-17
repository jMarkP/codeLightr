using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Owin;

namespace CodeLightr.Server.Test.Unit
{
    [TestFixture]
    public class OwinServerTests
    {
        [SetUp]
        public void BeforeAll()
        {
            ObservableMiddleware.Reset();
        }

        [Test]
        public async Task CanRunAndStopWebServer()
        {
            var url = "http://+:80/";

            Action<IAppBuilder> configure = app => { };

            var webApp = new Mock<IWepAppWrapper>();
            webApp.Setup(x => x.Start(url, configure))
                .Returns(new Mock<IDisposable>().Object);

            var SUT = new OwinServer(webApp.Object);
            var runTask = Task.Run(() => SUT.Run(url, configure));

            // Let it start up
            await Task.Delay(TimeSpan.FromSeconds(1));

            // Trigger it to stop
            SUT.Stop();
            await runTask;
            webApp.Verify(x => x.Start(url, configure));
        }
    }
}
