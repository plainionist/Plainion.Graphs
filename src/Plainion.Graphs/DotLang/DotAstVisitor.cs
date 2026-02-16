namespace Plainion.Graphs.DotLang;

public class DotAstVisitor : IDotAstVisitor
{
    private readonly RelaxedGraphBuilder myGraphBuilder;
    private readonly IList<FailedItem> myFailedItems;
    private readonly Dictionary<string, string> myLabels;

    public DotAstVisitor()
    {
        myGraphBuilder = new RelaxedGraphBuilder();
        myFailedItems = new List<FailedItem>();
        myLabels = new Dictionary<string, string>();
    }

    public Graph Graph
    {
        get { return myGraphBuilder.Graph; }
    }

    public IReadOnlyDictionary<string, string> Labels => myLabels;

    public Node? VisitNode(string id)
    {
        var node = myGraphBuilder.TryAddNode(id);
        if (node == null)
        {
            myFailedItems.Add(new FailedItem(id, "Node already exists"));
            return null;
        }

        return node;
    }

    public Edge? VisitEdge(string sourceNodeId, string targetNodeId)
    {
        var edge = myGraphBuilder.TryAddEdge(sourceNodeId, targetNodeId);

        if (edge == null)
        {
            myFailedItems.Add(new FailedItem(Edge.CreateId(sourceNodeId, targetNodeId), "Edge already exists"));
            return null;
        }

        return edge;
    }

    public Cluster? VisitCluster(string clusterId, IEnumerable<string> nodes)
    {
        var cluster = myGraphBuilder.TryAddCluster(clusterId, nodes);
        if (cluster == null)
        {
            myFailedItems.Add(new FailedItem(clusterId, "Cluster already exists"));
            return null;
        }

        return cluster;
    }

    public IEnumerable<FailedItem> FailedItems
    {
        get { return myFailedItems; }
    }

    public void VisitLabel(string id, string value)
    {
        myLabels.Add(id, value);
    }
}

