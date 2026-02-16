namespace Plainion.Graphs.DotLang;

class MatchWord : MatcherBase
{
    private readonly List<MatchKeyword> mySpecialCharacters;

    public MatchWord(IEnumerable<IMatcher> specialCharacters)
    {
        mySpecialCharacters = specialCharacters.OfType<MatchKeyword>().ToList();
    }

    protected override Token? IsMatchImpl(Tokenizer tokenizer)
    {
        string? current = null;

        while (!tokenizer.EndOfStream && !char.IsWhiteSpace(tokenizer.Current) && mySpecialCharacters.All(m => m.Match.Length > 1 || m.Match[0] != tokenizer.Current))
        {
            current += tokenizer.Current;
            tokenizer.Consume();
        }

        if (current == null)
        {
            return null;
        }

        // can't start a word with a special character
        if (mySpecialCharacters.Any(c => current.StartsWith(c.Match)))
        {
            throw new Exception(String.Format("Cannot start a word with a special character {0}", current));
        }

        return new Token(TokenType.Word, current);
    }
}
