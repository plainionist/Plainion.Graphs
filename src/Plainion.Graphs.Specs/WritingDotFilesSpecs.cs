using Plainion.Graphs.DotLang;

namespace Plainion.Graphs.Specs;

public class WritingDotFilesSpecs
{
    [Test]
    public void EdgesOnly()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddEdge("A", "B");
        builder.TryAddEdge("B", "C");

        var dot = WriteDot(builder.Graph);
        var graph = ParseDot(dot);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> B", "B -> C"]));
    }

    [Test]
    public void EdgesAndExplicitNodes()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddNode("A");
        builder.TryAddNode("B");
        builder.TryAddNode("C");
        builder.TryAddEdge("A", "B");
        builder.TryAddEdge("B", "C");

        var dot = WriteDot(builder.Graph);
        var graph = ParseDot(dot);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> B", "B -> C"]));
    }

    [Test]
    public void Subgraphs()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddCluster("cluster_ui", ["A", "B"]);
        builder.TryAddCluster("cluster_core", ["C", "D"]);
        builder.TryAddEdge("A", "C");

        var dot = WriteDot(builder.Graph);
        var graph = ParseDot(dot);
        
        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C", "D"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> C"]));
        Assert.That(graph.Clusters.Select(c => c.Id), Is.EquivalentTo(["cluster_ui", "cluster_core"]));
        Assert.That(graph.Clusters.Single(c => c.Id == "cluster_ui").Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B"]));
        Assert.That(graph.Clusters.Single(c => c.Id == "cluster_core").Nodes.Select(n => n.Id), Is.EquivalentTo(["C", "D"]));
    }

    [Test]
    public void EdgesAndSubgraphs()
    {
        var builder = new RelaxedGraphBuilder();
        builder.TryAddCluster("ui", ["A", "B"]);
        builder.TryAddCluster("core", ["C", "D"]);
        builder.TryAddEdge("A", "C");

        var dot = WriteDot(builder.Graph);

        Assert.That(dot, Is.EqualTo("""
            digraph {
              subgraph "ui" {
                "A"
                "B"
              }
              subgraph "core" {
                "C"
                "D"
              }
              "A"
              "B"
              "C"
              "D"
              "A" -> "C"
            }
            
            """.ReplaceLineEndings()));
    }

    private static string WriteDot(Graph graph)
    {
        var doc = new DotLangDocument(graph);
        using var writer = new StringWriter();
        doc.Write(writer);
        return writer.ToString();
    }

    private static Graph ParseDot(string dot)
    {
        var doc = DotLangDocument.Load(new StringReader(dot));
        return doc.Graph;
    }
}
