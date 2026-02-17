using System.Text;

namespace Plainion.Graphs.DotLang;

class MatchWord : MatcherBase
{
    private readonly HashSet<char> mySingleCharDelimiters;
    private readonly List<MatchKeyword> mySpecialCharacters;

    public MatchWord(IEnumerable<IMatcher> specialCharacters)
    {
        mySpecialCharacters = specialCharacters.OfType<MatchKeyword>().ToList();
        mySingleCharDelimiters = new HashSet<char>(
            mySpecialCharacters.Where(m => m.Match.Length == 1).Select(m => m.Match[0]));
    }

    protected override Token? IsMatchImpl(Tokenizer tokenizer)
    {
        if (tokenizer.EndOfStream || char.IsWhiteSpace(tokenizer.Current) || mySingleCharDelimiters.Contains(tokenizer.Current))
        {
            return null;
        }

        var sb = new StringBuilder();

        while (!tokenizer.EndOfStream && !char.IsWhiteSpace(tokenizer.Current) && !mySingleCharDelimiters.Contains(tokenizer.Current))
        {
            sb.Append(tokenizer.Current);
            tokenizer.Consume();
        }

        var current = sb.ToString();

        // can't start a word with a special character
        if (mySpecialCharacters.Any(c => current.StartsWith(c.Match)))
        {
            throw new Exception(String.Format("Cannot start a word with a special character {0}", current));
        }

        return new Token(TokenType.Word, current);
    }
}
