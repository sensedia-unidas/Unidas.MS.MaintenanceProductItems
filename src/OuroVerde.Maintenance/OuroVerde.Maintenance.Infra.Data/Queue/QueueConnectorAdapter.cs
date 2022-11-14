using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OuroVerde.Maintenance.Domain.Core.Messaging;
using System.Text;
using Unidas.MS.Maintenance.Domain.Adapters.Repository.Queue;

namespace Unidas.MS.Maintenance.Infra.Data.Queue
{
    public class QueueConnectorAdapter: Message, IQueueConnectorAdapter
    {
        private readonly IConfiguration _configuration;
        string ServiceBusConnectionString = "";
        string QueueName = "";

        public QueueConnectorAdapter(IConfiguration configuration)
        {
            _configuration = configuration;
            ServiceBusConnectionString = _configuration.GetConnectionString("");
            QueueName = _configuration.GetConnectionString("");
        }

        protected QueueConnectorAdapter()
        {

        }

        public async Task<List<T>> ConsumeMessageObject<T>(string sbConnectionString, 
                                                     string sbQueueName)
        {
            await using var client = new ServiceBusClient(sbConnectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(sbQueueName);

            var messages = await receiver.ReceiveMessageAsync();
            var body = Encoding.UTF8.GetString(messages.Body);

            List<T> objSerialize = null;
            objSerialize.Add(JsonConvert.DeserializeObject<T>(body));

            return objSerialize;
        }

        public async Task<List<T>> ConsumeMessageObject<T>()
        {
            await using var client = new ServiceBusClient(ServiceBusConnectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(QueueName);

            var messages = await receiver.ReceiveMessageAsync();
            var body = Encoding.UTF8.GetString(messages.Body);

            List<T> objSerialize = null;
            objSerialize.Add(JsonConvert.DeserializeObject<T>(body));

            return objSerialize;
        }
    }
}
