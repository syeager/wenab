using System.Net;
using LittleByte.Common.AspNet.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Wenab.Api.Dtos;
using Wenab.Api.Models;
using Wenab.Api.Requests;
using Wenab.Api.Services;
using Wenab.Api.Ynab;
using Wenab.Ynab;
using Controller = LittleByte.Common.AspNet.Core.Controller;

namespace Wenab.Api.Controllers;

public sealed class GetSpendingSummaryController : Controller
{
    private readonly BudgetConfig budgetConfig;
    private readonly GenerateSpendingSummary generateSpendingSummary = new();
    private readonly LoadSnapshot loadSnapshot = new();
    private readonly YnabOptions options;

    public GetSpendingSummaryController(IOptions<YnabOptions> options, IOptions<BudgetConfig> budgetConfig)
    {
        this.options = options.Value;
        this.budgetConfig = budgetConfig.Value;
    }

    [HttpGet]
    [ResponseType(HttpStatusCode.OK, typeof(ResponseDto))]
    public async Task<ApiResponse<ResponseDto>> Get([FromQuery] MonthSummaryRequest request)
    {
        var (month, year) = request;

        var client = new HttpClient
        {
            DefaultRequestHeaders = { { "Authorization", $"Bearer {options.ApiKey}" } }
        };

        var ynabClient = new Client(client);
        //var budgetData = await ynabClient.GetBudgetByIdAsync(options.SharedBudgetId, null);
        //var budgetData = await ynabClient.GetBudgetMonthAsync(options.SharedBudgetId, new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero));

        var sinceDate = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);

        var day = DateTime.DaysInMonth(year, month);
        var snapshot = await loadSnapshot.LoadAsync(ynabClient, options.SharedBudgetId, budgetConfig, sinceDate);

        //var (budget, ledger) = new LoadDomainModels().Load(budgetData.Data.Budget, budgetConfig);
        var spendingSummary = generateSpendingSummary.Generate(budgetConfig, snapshot,
            DateTime.Parse($"{month}/1/{year}"), DateTime.Parse($"{month}/{day}/{year}"));
        var dto = new ResponseDto(new SnapshotDto(
                snapshot.Accounts
                    .Select(a => new AccountDto(a.Key, a.Value.Name, a.Value.Owner, a.Value.BackingAccount))
                    .ToArray(),
                snapshot.CategoryGroups.Select(cg => new CategoryGroupDto(cg.Key, cg.Value.Name,
                    cg.Value.Categories.Select(c => new CategoryDto(c.Id, c.Name)).ToArray())).ToArray()),
            SpendingSummaryDto.Create(spendingSummary));

        return new OkResponse<ResponseDto>(dto);
    }
}