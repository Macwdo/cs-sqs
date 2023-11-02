using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sqs.Publisher.Messages;

public class PaymentCreated: IMessage
{
    [JsonPropertyName("transaction_id")]
    public int TransactionId { get; init; }

    [JsonPropertyName("username")]
    public string Username { get; init; } = default!;

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonIgnore] public string MessageTypeName => nameof(PaymentCreated);
}