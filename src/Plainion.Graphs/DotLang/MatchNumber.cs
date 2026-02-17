using System.Text;

namespace Plainion.Graphs.DotLang;

class MatchNumber : MatcherBase
{
    protected override Token? IsMatchImpl(Tokenizer tokenizer)
    {
        var leftOperand = GetIntegers(tokenizer);

        if (leftOperand != null)
        {
            if (tokenizer.Current == '.')
            {
                tokenizer.Consume();

                var rightOperand = GetIntegers(tokenizer);

                // found a float
                if (rightOperand != null)
                {
                    return new Token(TokenType.Number, leftOperand + "." + rightOperand);
                }
            }

            return new Token(TokenType.Number, leftOperand);
        }

        return null;
    }

    private string? GetIntegers(Tokenizer tokenizer)
    {
        if (tokenizer.EndOfStream || !char.IsDigit(tokenizer.Current))
        {
            return null;
        }

        var sb = new StringBuilder();

        while (!tokenizer.EndOfStream && char.IsDigit(tokenizer.Current))
        {
            sb.Append(tokenizer.Current);
            tokenizer.Consume();
        }

        return sb.ToString();
    }
}
