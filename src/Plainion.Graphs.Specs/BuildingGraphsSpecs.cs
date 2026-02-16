namespace Plainion.Graphs.Specs;


public class RelaxedGraphBuilderSpecs
{
    [Test]
    public void AddingEdgeCreatesNodesImplicitly()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddEdge("A", "B");

        Assert.That(builder.Graph.Nodes.Select(x => x.Id), Is.EquivalentTo(["A", "B"]));
    }

    [Test]
    public void DuplicatesAreHandledGracefully()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddEdge("A", "B");

        Assert.That(builder.TryAddEdge("A", "B"), Is.Null);
        Assert.That(builder.TryAddNode("A"), Is.Null);
    }

    [Test]
    public void EdgeReusesExistingNodes()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddNode("A");
        builder.TryAddEdge("A", "B");

        Assert.That(builder.Graph.Nodes.Count(), Is.EqualTo(2));
    }

    [Test]
    public void ClusterCreatesNodesImplicitly()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddCluster("C1", ["X", "Y"]);

        Assert.That(builder.Graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["X", "Y"]));
        Assert.That(builder.Graph.Clusters.Single().Nodes.Select(n => n.Id), Is.EquivalentTo(["X", "Y"]));
    }

    [Test]
    public void FreezePreventsFurtherModification()
    {
        var builder = new RelaxedGraphBuilder();
        builder.Freeze();

        Assert.That(() => builder.TryAddNode("A"), Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void MultipleEdgesFromSameSource()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddEdge("A", "B");
        builder.TryAddEdge("A", "C");

        var source = builder.Graph.FindNode("A")!;

        Assert.That(source.Out, Has.Count.EqualTo(2));
        Assert.That(source.Out.Select(e => e.Target.Id), Is.EquivalentTo(["B", "C"]));
    }
}
