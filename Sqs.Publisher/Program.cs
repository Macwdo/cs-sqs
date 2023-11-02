using Amazon;
using Amazon.SQS;
using Sqs.Publisher;
using Sqs.Publisher.DTOs;
using Sqs.Publisher.Messages;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapPost("/create", async (PaymentRequest paymentRequest) =>
{

    var paymentResponse = new PaymentResponse(
        new Random().Next(1, 10000),
        paymentRequest.Username,
        paymentRequest.Amount,
        DateTime.Now
    );

    var paymentCreated = new PaymentCreated
    {
        TransactionId = paymentResponse.TransactionId,
        CreatedAt = paymentResponse.CreatedAt,
        Username = paymentResponse.Username,
        Amount = paymentResponse.Amount
    };

    var sqsClient = new AmazonSQSClient(RegionEndpoint.USEast1);
    var publisher = new SqsPublisher(sqsClient);

    await publisher.PublishAsync("payments", paymentCreated);

    return Results.Ok(paymentResponse);
});

app.Urls.Add("http://localhost:5001");

app.Run();