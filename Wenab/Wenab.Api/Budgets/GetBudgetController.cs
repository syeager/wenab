using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Wenab.Ynab;
using Controller = LittleByte.Common.AspNet.Core.Controller;

namespace Wenab.Api.Budgets;

public sealed class GetBudgetController : Controller
{
    private readonly YnabOptions options;

    public GetBudgetController(IOptions<YnabOptions> options)
    {
        this.options = options.Value;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var client = new HttpClient
        {
            DefaultRequestHeaders = { { "Authorization", $"Bearer {options.ApiKey}" } }
        };

        var ynabClient = new Client(client);
        var budget = await ynabClient.GetBudgetByIdAsync(options.SharedBudgetId, null);

        return Ok(budget);
    }
}