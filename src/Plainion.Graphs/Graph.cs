namespace Plainion.Graphs;

public class Graph
{
    private readonly IDictionary<string, Node> myNodes;
    private readonly IDictionary<string, Edge> myEdges;
    private readonly IDictionary<string, Cluster> myClusters;

    public Graph()
    {
        myNodes = new Dictionary<string, Node>();
        myEdges = new Dictionary<string, Edge>();
        myClusters = new Dictionary<string, Cluster>();
    }

    public IEnumerable<Node> Nodes => myNodes.Values;
    public IEnumerable<Edge> Edges => myEdges.Values;
    public IEnumerable<Cluster> Clusters => myClusters.Values;

    public bool TryAdd(Node node) => TryAdd(node, myNodes);
    public bool TryAdd(Edge edge) => TryAdd(edge, myEdges);
    public bool TryAdd(Cluster cluster) => TryAdd(cluster, myClusters);

    public void Add(Node node) => Add(node, myNodes);
    public void Add(Edge edge) => Add(edge, myEdges);
    public void Add(Cluster cluster) => Add(cluster, myClusters);

    private bool TryAdd<T>(T item, IDictionary<string, T> store) where T : IGraphItem
    {
        Contract.RequiresNotNull(item, nameof(item));
        Contract.Invariant(!IsFrozen, "Graph is frozen and cannot be modified");

        if (store.ContainsKey(item.Id))
        {
            return false;
        }

        store.Add(item.Id, item);
        return true;
    }

    private void Add<T>(T item, IDictionary<string, T> store) where T : IGraphItem
    {
        if (!TryAdd(item, store))
        {
            throw new ArgumentException($"{typeof(T).Name} already exists: {item.Id}");
        }
    }

    public Node? FindNode(string nodeId) =>
        myNodes.TryGetValue(nodeId, out var node) ? node : null;

    public Node GetNode(string nodeId)
    {
        var node = FindNode(nodeId);
        Contract.Requires(node != null, "Node not found: " + nodeId);
        return node!;
    }

    public bool IsFrozen { get; private set; }

    public void Freeze()
    {
        IsFrozen = true;
    }
}
