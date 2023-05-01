using LittleByte.Common.Domain;

namespace Wenab.Api.Models;

public enum Owner
{
    None,
    Rachel,
    Steve,
}




//public readonly record struct Transaction(Id<Transaction> Id, DateTimeOffset Date, long Amount, string? Memo, Account Account,
//    Category Category);



/*
 * "name": "Shared expenses on Amazon card",
 * "paidFrom": 123, // amazon card
 * "payTo": 987, // chase checking shared
 * "split": {
 *      "rachel": .5,
 *      "steve": .5,
 * },
 * "categoryGroups": [
 *      234, // Food
 *      345, // Health
 * ]
 */



// -- data
// account
// spent in each category group
// how much we each owe
