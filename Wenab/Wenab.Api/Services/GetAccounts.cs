using LittleByte.Common.Domain;
using Wenab.Api.Models;
using Wenab.Ynab;
using Account = Wenab.Api.Models.Account;
using Category = Wenab.Api.Models.Category;
using CategoryGroup = Wenab.Api.Models.CategoryGroup;

namespace Wenab.Api.Services;

public sealed class GetAccounts
{
    public async ValueTask<IReadOnlyDictionary<Guid, Account>> LoadAsync(Client ynabClient, string budgetId, BudgetConfig config)
    {
        var accountData = await ynabClient.GetAccountsAsync(budgetId, null);
        var accounts = accountData.Data.Accounts
            .Select(account =>
            {
                var accountConfig = config.Accounts.FirstOrDefault(a => a.Id == account.Id);
                var id = new Id<Account>(account.Id);
                var backingId = accountConfig is null || accountConfig.BackingAccount == Guid.Empty
                    ? id
                    : new Id<Account>(accountConfig.BackingAccount);
                return new Account(id, account.Name, accountConfig?.Owner ?? Owner.None, backingId);
            })
            .ToDictionary(account => account.Id.Value, account => account);
        return accounts;
    }
}

public sealed class GetCategoryGroups
{
    public async ValueTask<IReadOnlyDictionary<Guid, CategoryGroup>> LoadAsync(Client ynabClient, string budgetId)
    {
        var categoryGroupData = await ynabClient.GetCategoriesAsync(budgetId, null);
        var categoryGroups = categoryGroupData.Data.Category_groups
            .Select(cg => new CategoryGroup(new Id<CategoryGroup>(cg.Id), cg.Name,
                cg.Categories.Select(c => new Category(new Id<Category>(c.Id), c.Name)).ToArray()))
            .ToDictionary(cg => cg.Id.Value, cg => cg);
        return categoryGroups;
    }
}

public sealed class GetTransactions
{
    public async ValueTask<IReadOnlyDictionary<Guid, Transaction>> LoadAsync(Client ynabClient, string budgetId,
        DateTimeOffset sinceDate, IReadOnlyDictionary<Guid, Account> accounts,
        IReadOnlyDictionary<Guid, CategoryGroup> categoryGroups)
    {
        var categories = categoryGroups.SelectMany(cg => cg.Value.Categories).ToDictionary(c => c.Id.Value, c => c);

        var transactionData = await ynabClient.GetTransactionsAsync(budgetId, sinceDate, null, null);
        var transactions = transactionData.Data.Transactions.SelectMany(transaction =>
        {
            var account = accounts[transaction.Account_id];

            if (transaction.Subtransactions.Any())
            {
                return transaction.Subtransactions.Select(st =>
                {
                    var category = GetCategory(categories, st.Category_id);

                    return new Transaction
                    {
                        Id = new Id<Transaction>(Guid.Parse(st.Id)),
                        Date = transaction.Date.UtcDateTime,
                        Amount = st.Amount,
                        Account = account,
                        Category = category,
                        Memo = st.Memo,
                        PayeeName = transaction.Import_payee_name
                    };
                });
            }

            var category = GetCategory(categories, transaction.Category_id);

            return new Transaction[]
            {
                new()
                {
                    Id = new Id<Transaction>(Guid.Parse(transaction.Id)),
                    Date = transaction.Date.UtcDateTime,
                    Amount = transaction.Amount,
                    Account = account,
                    Category = category,
                    Memo = transaction.Memo,
                    PayeeName = transaction.Import_payee_name
                }
            };
        }).ToDictionary(t => t.Id.Value, t => t);
        return transactions;
    }

    private static Category? GetCategory(IReadOnlyDictionary<Guid, Category> categories, Guid? categoryId)
    {
        categories.TryGetValue(categoryId ?? Guid.Empty, out var category);
        return category;
    }
}

public sealed class Bag<T>
{
    private readonly IReadOnlyDictionary<Guid, T> items;

    public T this[Guid id] => items[id];

    public Bag(IReadOnlyDictionary<Guid, T> items)
    {
        this.items = items;
    }
}

public sealed class LoadSnapshot
{
    private readonly GetAccounts getAccounts = new();
    private readonly GetCategoryGroups getCategoryGroups = new();
    private readonly GetTransactions getTransactions = new();

    public async ValueTask<Snapshot> LoadAsync(Client ynabClient, string budgetId, BudgetConfig config,
        DateTimeOffset sinceDate)
    {
        var accounts = await getAccounts.LoadAsync(ynabClient, budgetId, config);
        var categoryGroups = await getCategoryGroups.LoadAsync(ynabClient, budgetId);
        var transactions = await getTransactions.LoadAsync(ynabClient, budgetId, sinceDate, accounts, categoryGroups);

        var snapshot = new Snapshot(accounts, categoryGroups, transactions);
        return snapshot;
    }
}