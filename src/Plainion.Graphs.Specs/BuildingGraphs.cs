using Plainion.Graphs;

namespace Plainion.Graphs.Specs;

public class Tests
{
    [Test]
    public void AddingEdgesAddsNodesImplicitly()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddEdge("A", "B");

        var graph = builder.Graph;

        Assert.That(graph.Nodes, Is.EquivalentTo(["A", "B"]));
        Assert.That(graph.Edges, Is.EquivalentTo(["A -> B"]));
    }
}
