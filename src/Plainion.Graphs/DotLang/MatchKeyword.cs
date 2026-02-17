namespace Plainion.Graphs.DotLang;

class MatchKeyword : MatcherBase
{
    private readonly TokenType myTokenType;
    private HashSet<char>? myDelimiterChars;

    public string Match { get; private set; }

    /// <summary>
    /// If true then matching on { in a string like "{test" will match the first character
    /// because it is not space delimited. If false it must be space or special character delimited
    /// </summary>
    public bool AllowAsSubString { get; set; }

    private List<MatchKeyword> mySpecialCharacters = [];

    public List<MatchKeyword> SpecialCharacters
    {
        get => mySpecialCharacters;
        set
        {
            mySpecialCharacters = value;
            myDelimiterChars = new HashSet<char>(
                value.Where(c => c.Match.Length == 1).Select(c => c.Match[0]));
        }
    }

    public MatchKeyword(TokenType type, string match)
    {
        Match = match;
        myTokenType = type;
        AllowAsSubString = true;
    }

    protected override Token? IsMatchImpl(Tokenizer tokenizer)
    {
        foreach (var character in Match)
        {
            if (tokenizer.Current == character)
            {
                tokenizer.Consume();
            }
            else
            {
                return null;
            }
        }

        bool found;

        if (!AllowAsSubString)
        {
            var next = tokenizer.Current;

            found = char.IsWhiteSpace(next) || (myDelimiterChars != null && myDelimiterChars.Contains(next));
        }
        else
        {
            found = true;
        }

        if (found)
        {
            return new Token(myTokenType, Match);
        }

        return null;
    }
}
