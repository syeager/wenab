using System.ComponentModel.DataAnnotations;

namespace Wenab.Api.Requests;

public record MonthSummaryRequest([Required] int Month, [Required] int Year);