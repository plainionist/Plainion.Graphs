namespace Plainion.Graphs;

[Serializable]
public class Edge : IGraphItem, IEquatable<Edge>
{
    public Edge(Node source, Node target)
    {
        Contract.RequiresNotNull(source, nameof(source));
        Contract.RequiresNotNull(target, nameof(target));

        Source = source;
        Target = target;

        Id = CreateId(source.Id, target.Id);
    }

    public string Id { get; }

    public Node Source { get; }
    public Node Target { get; }

    public static string CreateId(string sourceId, string targetId) =>
        $"{sourceId} -> {targetId}";

    public bool Equals(Edge other) =>
        other != null && Id == other.Id;

    public override bool Equals(object obj) =>
        obj is Edge edge && Equals(edge);

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => Id;
}
