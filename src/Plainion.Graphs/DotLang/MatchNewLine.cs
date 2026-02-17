namespace Plainion.Graphs.DotLang;

class MatchNewLine : MatcherBase
{
    protected override Token? IsMatchImpl(Tokenizer tokenizer)
    {
        if (tokenizer.Current == '\r')
        {
            tokenizer.Consume();
        }

        if (tokenizer.Current == '\n')
        {
            tokenizer.Consume();
            return new Token(TokenType.NewLine);
        }

        return null;
    }
}
