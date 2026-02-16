namespace Plainion.Graphs.Specs;

public class BuildingGraphsSpecs
{
    [Test]
    public void AddingEdgesAddsNodesImplicitly()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddEdge("A", "B");

        var graph = builder.Graph;

        Assert.That(graph.Nodes.Select(x => x.Id), Is.EquivalentTo(["A", "B"]));
        Assert.That(graph.Edges.Select(x => x.Id), Is.EquivalentTo(["A -> B"]));
    }
}
