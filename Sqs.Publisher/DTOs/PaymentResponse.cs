using System.ComponentModel.DataAnnotations;

namespace Sqs.Publisher.DTOs;

public record PaymentResponse(
    int TransactionId,
    [Required] string Username,
    decimal Amount,
    DateTime CreatedAt
);