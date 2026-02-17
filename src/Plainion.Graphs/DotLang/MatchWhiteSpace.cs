namespace Plainion.Graphs.DotLang;

class MatchWhiteSpace : MatcherBase
{
    protected override Token? IsMatchImpl(Tokenizer tokenizer)
    {
        var matched = false;

        while (!tokenizer.EndOfStream && char.IsWhiteSpace(tokenizer.Current)
            && tokenizer.Current != '\r' && tokenizer.Current != '\n')
        {
            matched = true;
            tokenizer.Consume();
        }

        if (matched)
        {
            return new Token(TokenType.WhiteSpace);
        }

        return null;
    }
}
