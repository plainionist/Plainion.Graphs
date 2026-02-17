namespace Plainion.Graphs.DotLang;

// Hint: also for rendering we pass label to trigger dot.exe to create proper size of node bounding box
public class DotWriter
{
    public void Write(Graph graph, string path)
    {
        Contract.RequiresNotNull(graph, "graph");
        Contract.RequiresNotNull(path, "path");

        using var writer = new StreamWriter(path);
        Write(graph, writer);
    }

    public void Write(Graph graph, TextWriter writer)
    {
        Contract.RequiresNotNull(graph, "graph");
        Contract.RequiresNotNull(writer, "writer");

        writer.WriteLine("digraph {");

        foreach (var cluster in graph.Clusters)
        {
            writer.WriteLine("  subgraph \"" + cluster.Id + "\" {");

            foreach (var node in cluster.Nodes.OrderBy(n => n.Id))
            {
                Write(writer, node, "    ");
            }

            writer.WriteLine("  }");
        }

        var clusteredNodeIds = new HashSet<string>(
            graph.Clusters.SelectMany(c => c.Nodes).Select(n => n.Id));

        foreach (var node in graph.Nodes.OrderBy(x => x.Id))
        {
            if (!clusteredNodeIds.Contains(node.Id))
            {
                Write(writer, node, "  ");
            }
        }

        foreach (var edge in graph.Edges.OrderBy(x => x.Id))
        {
            Write(writer, edge, "  ");
        }

        writer.WriteLine("}");
    }

    private void Write(TextWriter writer, Node node, string indent)
    {
        writer.Write(indent);
        writer.WriteLine("\"{0}\"", node.Id);
    }

    private void Write(TextWriter writer, Edge edge, string indent)
    {
        writer.Write(indent);

        writer.Write("\"{0}\" -> \"{1}\"", edge.Source.Id, edge.Target.Id);

        writer.WriteLine();
    }
}

