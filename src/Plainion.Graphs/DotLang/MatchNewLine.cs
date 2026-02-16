using System;
using System.Text;

namespace Plainion.Graphs.DotLang;

class MatchNewLine : MatcherBase
{
    protected override Token? IsMatchImpl(Tokenizer tokenizer)
    {
        var str = new StringBuilder();

        while (tokenizer.Current == '\r' || tokenizer.Current == '\n')
        {
            str.Append(tokenizer.Current);

            tokenizer.Consume();
        }

        if (str.ToString() == Environment.NewLine)
        {
            return new Token(TokenType.NewLine);
        }

        return null;
    }
}
