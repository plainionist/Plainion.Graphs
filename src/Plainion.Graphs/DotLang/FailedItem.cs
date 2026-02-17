namespace Plainion.Graphs.DotLang;

public class FailedItem(string item, string reason)
{
    public string Item { get; } = item;

    public string FailureReason { get; } = reason;
}
