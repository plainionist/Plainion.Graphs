namespace Plainion.Graphs.Specs;

public class GraphSpecs
{
    [Test]
    public void AddingDuplicateItemsThrows()
    {
        var graph = new Graph();
        graph.Add(new Edge(new Node("A"), new Node("B")));
        graph.Add(new Node("C"));

        Assert.That(() => graph.Add(new Node("C")), Throws.ArgumentException);
        Assert.That(() => graph.Add(new Edge(new Node("A"), new Node("B"))), Throws.ArgumentException);
    }

    [Test]
    public void TryAddingDuplicateItemsReturnsFalse()
    {
        var graph = new Graph();
        graph.Add(new Edge(new Node("A"), new Node("B")));
        graph.Add(new Node("C"));

        Assert.That(graph.TryAdd(new Node("C")), Is.False);
        Assert.That(graph.TryAdd(new Edge(new Node("A"), new Node("B"))), Is.False);
    }

    [Test]
    public void FindingNodes()
    {
        var graph = new Graph();

        Assert.That(graph.FindNode("X"), Is.Null);

        var node = new Node("A");
        graph.Add(node);

        Assert.That(graph.FindNode("A"), Is.SameAs(node));
    }

    [Test]
    public void FrozenGraphRejectsNewItems()
    {
        var graph = new Graph();
        graph.Freeze();

        Assert.That(() => graph.Add(new Node("A")), Throws.InstanceOf<InvalidOperationException>());
        Assert.That(() => graph.Add(new Edge(new Node("A"), new Node("B"))), Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void ItemsEqualById()
    {
        var a = new Node("X");
        var b = new Node("X");

        Assert.That(a, Is.EqualTo(b));
        Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));

        Assert.That(new Node("A"), Is.Not.EqualTo(new Node("B")));

        var e1 = new Edge(new Node("X"), new Node("Y"));
        var e2 = new Edge(new Node("X"), new Node("Y"));

        Assert.That(e1, Is.EqualTo(e2));
        Assert.That(e1.GetHashCode(), Is.EqualTo(e2.GetHashCode()));

        Assert.That(new Edge(new Node("A"), new Node("B")), Is.Not.EqualTo(new Edge(new Node("A"), new Node("C"))));
        Assert.That(new Edge(new Node("A"), new Node("B")), Is.Not.EqualTo(new Edge(new Node("B"), new Node("A"))));

        var c1 = new Cluster("C1", [new Node("N")]);
        var c2 = new Cluster("C1", [new Node("M")]);

        Assert.That(c1, Is.EqualTo(c2));
        Assert.That(c1.GetHashCode(), Is.EqualTo(c2.GetHashCode()));

        Assert.That(new Cluster("C1", [new Node("N")]), Is.Not.EqualTo(new Cluster("C2", [new Node("N")])));
    }
}
