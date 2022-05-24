using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Cores.Extension.Services.NServiceBus
{
    internal static class NServiceBusService
    {
        #region NServiceBus
        /// <summary>
        /// Setups the n service bus.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>A Task.</returns>
        private static void SetupNServiceBus(this IServiceCollection services)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

            }

            //var endpointConfiguration = new EndpointConfiguration("ClientUI");
            //endpointConfiguration.MakeInstanceUniquelyAddressable("Client");
            //var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

            //var connectionFactory = new ConnectionFactory()
            //{
            //    HostName = "127.0.0.1",
            //    Port = 15672,
            //    UserName = "guest",
            //    Password = "guest",
            //    VirtualHost = "/"
            //};
            //using (IConnection connection = connectionFactory.CreateConnection())
            //{
            //    //transport.ConnectionString("host=localhost:15672; username=diadiem;password =diadiem@2019").UseDirectRoutingTopology();



            //    var routing = transport.Routing();
            //    routing.RouteToEndpoint(typeof(RequestMessage), "ServerSide");
            //}
            //var _endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration)
            //    .ConfigureAwait(false);

            //services.AddSingleton(_endpointInstance);
        }
        #endregion
    }
}
