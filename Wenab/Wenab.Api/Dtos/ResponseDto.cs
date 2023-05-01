using System.ComponentModel.DataAnnotations;

namespace Wenab.Api.Dtos;

public sealed class ResponseDto
{
    [Required]
    public SnapshotDto Snapshot { get; }
    [Required]
    public SpendingSummaryDto SpendingSummary { get; }

    public ResponseDto(SnapshotDto snapshot, SpendingSummaryDto spendingSummary)
    {
        Snapshot = snapshot;
        SpendingSummary = spendingSummary;
    }
}