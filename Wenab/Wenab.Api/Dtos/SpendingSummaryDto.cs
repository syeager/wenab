using System.ComponentModel.DataAnnotations;
using Wenab.Api.Models;

namespace Wenab.Api.Dtos;

public sealed class SpendingSummaryDto
{
    [Required]
    public DateTime FromDate { get; }
    [Required]
    public DateTime ToDate { get; }
    [Required]
    public OwnerSummaryDto[] OwnerSummaries { get; }

    public SpendingSummaryDto(DateTime fromDate, DateTime toDate, OwnerSummaryDto[] ownerSummaries)
    {
        FromDate = fromDate;
        ToDate = toDate;
        OwnerSummaries = ownerSummaries;
    }

    public static SpendingSummaryDto Create(SpendingSummary spendingSummary)
    {
        var dto = new SpendingSummaryDto(spendingSummary.FromDate, spendingSummary.ToDate, spendingSummary.OwnerSummaries.Select(OwnerSummaryDto.Create).ToArray());

        return dto;
    }
}