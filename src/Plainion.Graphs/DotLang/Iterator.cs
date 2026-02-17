namespace Plainion.Graphs.DotLang;

class Iterator
{
    private readonly IEnumerator<Token> myEnumerator;
    private Token? myCurrent;
    private Token? myNext;

    public Iterator(Lexer lexer)
    {
        myEnumerator = lexer.Lex().GetEnumerator();

        // Pre-fetch the first token into myNext so that IsNext works before the first MoveNext
        if (myEnumerator.MoveNext())
        {
            myNext = myEnumerator.Current;
        }
    }

    public Token Current => myCurrent!;

    public Token? Next => myNext;

    public bool IsNext(TokenType tokenType) =>
        myNext != null && myNext.Type == tokenType;

    public bool MoveNext()
    {
        if (myNext == null)
        {
            return false;
        }

        myCurrent = myNext;

        if (myEnumerator.MoveNext())
        {
            myNext = myEnumerator.Current;
        }
        else
        {
            myNext = null;
        }

        return true;
    }
}
