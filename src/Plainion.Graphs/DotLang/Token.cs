namespace Plainion.Graphs.DotLang;

class Token
{
    public Token(TokenType tokenType)
        : this(tokenType, null)
    {
    }

    public Token(TokenType tokenType, string? token)
    {
        Type = tokenType;
        Value = token;
    }

    public TokenType Type { get; }

    public string? Value { get; }

    public override string ToString() => Type + ": " + Value;
}

enum TokenType
{
    Edge,
    Graph,
    WhiteSpace,
    GraphBegin,
    GraphEnd,
    QuotedString,
    Word,
    Comma,
    Assignment,
    CommentBegin,
    CommentEnd,
    EdgeDef,
    Strict,
    SemiColon,
    Node,
    Subgraph,
    EndOfStream,
    DirectedGraph,
    AttributeBegin,
    AttributeEnd,
    Number,
    SingleLineComment,
    NewLine
}
