using System.ComponentModel.DataAnnotations;

namespace Sqs.Publisher.DTOs;

public record PaymentRequest(
    [Required] string Username,
    [Required] string PixKey,
    decimal Amount
);