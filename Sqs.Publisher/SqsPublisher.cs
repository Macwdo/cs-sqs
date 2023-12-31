using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Sqs.Publisher.Messages;

namespace Sqs.Publisher;

public class SqsPublisher
{
    private readonly IAmazonSQS _sqs;

    public SqsPublisher(IAmazonSQS sqs)
    {
        _sqs = sqs;
    }

    public async Task PublishAsync<TMessage>(string queueName, TMessage message)
        where TMessage : IMessage
    {
        var queueUrl = await _sqs.GetQueueUrlAsync(queueName);
        var request = new SendMessageRequest
        {
            MessageBody = JsonSerializer.Serialize(message),
            QueueUrl = queueUrl.QueueUrl,
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {nameof(IMessage.MessageTypeName), new MessageAttributeValue
                {
                    StringValue = message.MessageTypeName,
                    DataType = "String"
                }}
            }
        };

        await _sqs.SendMessageAsync(request);

    }
}