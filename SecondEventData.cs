using System.ComponentModel.DataAnnotations;

internal sealed record class SecondEventData
{
    [Required]
    public required int Second { get; init; }
}
