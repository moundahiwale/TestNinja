using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace TestNinja.IntegrationTests
{
    public class TestClientProvider : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client {get; private set; }
        public TestClientProvider()
        {
            // https://github.com/aspnet/Hosting/issues/1191#issuecomment-326119911
            _server = new TestServer(new WebHostBuilder()
                        .UseConfiguration(new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build())
                        .UseStartup<Startup>());

            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}