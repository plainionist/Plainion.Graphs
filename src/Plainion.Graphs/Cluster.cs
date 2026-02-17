namespace Plainion.Graphs;

public class Cluster : IGraphItem, IEquatable<Cluster>
{
    public Cluster(string id, IEnumerable<Node> nodes)
    {
        Contract.RequiresNotNullNotEmpty(id, nameof(id));

        Id = id;

        Nodes = nodes.ToList();
    }

    public string Id { get; }

    public IReadOnlyCollection<Node> Nodes { get; }

    public bool Equals(Cluster other) =>
        other != null && Id == other.Id;

    public override bool Equals(object obj) =>
        obj is Cluster cluster && Equals(cluster);

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => Id;
}
