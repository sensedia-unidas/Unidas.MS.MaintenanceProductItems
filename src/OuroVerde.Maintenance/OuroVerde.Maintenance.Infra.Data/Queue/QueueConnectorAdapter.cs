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
            ServiceBusConnectionString = _configuration.GetSection("Service_BUS:PrimaryConnection").Value;
            QueueName = _configuration.GetSection("Service_BUS:QueueName").Value;
        }

        protected QueueConnectorAdapter()
        {

        }

        public async Task<List<T>> ConsumeMessageObject<T>(string sbConnectionString, 
                                                     string sbQueueName)
        {
            List<T> objSerialize = null;

            await using var client = new ServiceBusClient(sbConnectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(sbQueueName);

            var messages = await receiver.ReceiveMessageAsync();

            if (messages != null)
            {
                var body = Encoding.UTF8.GetString(messages.Body);
                objSerialize.Add(JsonConvert.DeserializeObject<T>(body));

                await receiver.CompleteMessageAsync(messages);
            }

            return objSerialize;
        }

        public async Task<List<T>> ConsumeMessageObject<T>()
        {
            List<T> objSerialize = null;
            await using var client = new ServiceBusClient(ServiceBusConnectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(QueueName);

            var messages = await receiver.ReceiveMessageAsync();

            if (messages != null)
            {
                var body = Encoding.UTF8.GetString(messages.Body);
                objSerialize.Add(JsonConvert.DeserializeObject<T>(body));

                await receiver.CompleteMessageAsync(messages);
            }            

            return objSerialize;
        }
    }
}
