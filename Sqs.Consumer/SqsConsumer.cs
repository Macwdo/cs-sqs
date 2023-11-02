using System.Net;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Sqs.Consumer;

public class SqsConsumer: BackgroundService
{
    private readonly IAmazonSQS _sqs;
    private const string QueueName = "payments";
    private readonly List<string> _messageAttributeNames = new() { "All" };


    public SqsConsumer(IAmazonSQS sqs)
    {
        _sqs = sqs;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrl = await _sqs.GetQueueUrlAsync(QueueName, stoppingToken);
        var receiveMessageRequest = new ReceiveMessageRequest
        {
            AttributeNames = _messageAttributeNames,
            MessageAttributeNames = _messageAttributeNames,
            QueueUrl = queueUrl.QueueUrl,
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var messageResponse = await _sqs.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);
            if (messageResponse.HttpStatusCode != HttpStatusCode.OK)
            {
                // Logging or Handle
                continue;
            }

            foreach (var message in messageResponse.Messages)
            {
                Console.WriteLine(message.Body);
                await _sqs.DeleteMessageAsync(queueUrl.QueueUrl, message.ReceiptHandle, stoppingToken);
            }

        }
    }
}