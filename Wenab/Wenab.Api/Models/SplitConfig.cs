namespace Wenab.Api.Models;

public sealed class SplitConfig
{
    public string Name { get; init; }
    public Split Split { get; init; }
    public Guid[] CategoryGroups { get; init; }

    public SplitPortion? this[Owner owner] => Split.Portions.FirstOrDefault(portion => portion.Owner == owner);

    public override string ToString() => Name;
}