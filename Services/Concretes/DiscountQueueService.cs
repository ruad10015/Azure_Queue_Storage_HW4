using Azure.Storage.Queues;
using Azure_Queue_Storage_HW4.Services.Abstracts;

namespace Azure_Queue_Storage_HW4.Services.Concretes
{
    public class DiscountQueueService : IDiscountQueueService
    {
        private readonly QueueClient _queueClient;
        public DiscountQueueService(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName);
            _queueClient.CreateIfNotExists();
        }
        public async Task<string> ReceiveMessageAsync()
        {
            var messageResponse = await _queueClient.ReceiveMessageAsync();
            if (messageResponse.Value != null)
            {
                var message = messageResponse.Value;
                //await _queueClient.DeleteMessageAsync(messageResponse.Value.MessageId, messageResponse.Value.PopReceipt);
                return message.Body.ToString();
            }
            return null;
        }

        public async Task SendMessageAsync(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                await _queueClient.SendMessageAsync(message, TimeSpan.FromSeconds(30),TimeSpan.FromMinutes(50));
            }
        }
    }
}
