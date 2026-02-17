namespace Plainion.Graphs.DotLang;

// http://www.graphviz.org/doc/info/lang.html
public class DotLangDocument
{
    public DotLangDocument(Graph graph)
    {
        Contract.RequiresNotNull(graph, "graph");

        Graph = graph;
    }

    private IReadOnlyDictionary<string, string>? myLabels = null;

    public Graph Graph { get; }

    public static DotLangDocument Load(string path)
    {
        using var reader = new StreamReader(path);
        return Load(reader);
    }

    public static DotLangDocument Load(TextReader reader) =>
        Read(reader);

    private static DotLangDocument Read(TextReader reader)
    {
        var visitor = new DotAstVisitor();

        var lexer = new Lexer(reader.ReadToEnd());
        var parser = new Parser(lexer, visitor);
        parser.Parse();

        return new DotLangDocument(visitor.Graph) { myLabels = visitor.Labels };
    }

    public void Write(TextWriter writer)
    {
        Contract.RequiresNotNull(writer, "writer");
        Contract.RequiresNotNull(Graph, "Graph");

        var dotWriter = new DotWriter();
        dotWriter.Write(Graph, writer);
    }

    public string GetLabel(string id) =>
        myLabels != null && myLabels.TryGetValue(id, out var value) ? value : id;
}
