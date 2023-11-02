using Amazon;
using Amazon.SQS;
using Sqs.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<SqsConsumer>();

builder.Services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(RegionEndpoint.USEast1));

var app = builder.Build();
app.Run();