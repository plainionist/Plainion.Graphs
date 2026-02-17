using Plainion.Graphs.DotLang;

namespace Plainion.Graphs.Specs;

public class ParsingDotFilesSpecs
{
    [Test]
    public void EdgesOnly()
    {
        var graph = ParseDot("""
            digraph {
                A -> B
                B -> C
            }
            """);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> B", "B -> C"]));
    }

    private static Graph ParseDot(string dot)
    {
        var doc = DotLangDocument.Load(new StringReader(dot));
        return doc.Graph!;
    }

    [Test]
    public void EdgesAndExplicitNodes()
    {
        var graph = ParseDot("""
            digraph {
                A
                B
                C
                A -> B
                B -> C
            }
            """);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> B", "B -> C"]));
    }

    [Test]
    public void Subgraphs()
    {
        var graph = ParseDot("""
            digraph {
                subgraph cluster_ui {
                    A
                    B
                }
                subgraph cluster_core {
                    C
                    D
                }
                A -> C
            }
            """);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C", "D"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> C"]));
        Assert.That(graph.Clusters.Select(c => c.Id), Is.EquivalentTo(["cluster_ui", "cluster_core"]));
        Assert.That(graph.Clusters.Single(c => c.Id == "cluster_ui").Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B"]));
        Assert.That(graph.Clusters.Single(c => c.Id == "cluster_core").Nodes.Select(n => n.Id), Is.EquivalentTo(["C", "D"]));
    }

    [Test]
    public void UnixLineEndings()
    {
        var dot = "digraph {\n  A -> B\n  B -> C\n}\n";
        var graph = ParseDot(dot);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> B", "B -> C"]));
    }

    [Test]
    public void WindowsLineEndings()
    {
        var dot = "digraph {\r\n  A -> B\r\n  B -> C\r\n}\r\n";
        var graph = ParseDot(dot);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> B", "B -> C"]));
    }

    [Test]
    public void ConsecutiveBlankLines()
    {
        var dot = "digraph {\n\n  A -> B\n\n\n  B -> C\n\n}\n";
        var graph = ParseDot(dot);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B", "C"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> B", "B -> C"]));
    }

    [Test]
    public void DuplicateEdgeWithAttributes()
    {
        var graph = ParseDot("""
            digraph {
                A -> B [label="first"]
                A -> B [label="duplicate"]
            }
            """);

        Assert.That(graph.Edges.Count(), Is.EqualTo(1));
        Assert.That(graph.Edges.Single().Id, Is.EqualTo("A -> B"));
    }

    [Test]
    public void SingleLineCommentWithUnixLineEndings()
    {
        var dot = "digraph {\n  // this is a comment\n  A -> B\n}\n";
        var graph = ParseDot(dot);

        Assert.That(graph.Nodes.Select(n => n.Id), Is.EquivalentTo(["A", "B"]));
        Assert.That(graph.Edges.Select(e => e.Id), Is.EquivalentTo(["A -> B"]));
    }
}
