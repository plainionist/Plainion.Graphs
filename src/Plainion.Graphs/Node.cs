using System.Diagnostics;

namespace Plainion.Graphs;

[DebuggerDisplay("{Id}")]
public class Node : IGraphItem, IEquatable<Node>
{
    public Node(string id)
    {
        Contract.RequiresNotNullNotEmpty(id, nameof(id));

        Id = id;

        In = [];
        Out = [];
    }

    public string Id { get; }

    public IList<Edge> In { get; }
    public IList<Edge> Out { get; }

    public bool Equals(Node other) =>
        other != null && Id == other.Id;

    public override bool Equals(object obj) =>
        obj is Node node && Equals(node);

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => Id;
}
