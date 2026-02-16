namespace Plainion.Graphs.DotLang;

interface IMatcher
{
    Token? IsMatch(Tokenizer tokenizer);
}
