
namespace CodingBot.DotLang
{
    interface IMatcher
    {
        Token? IsMatch(Tokenizer tokenizer);
    }
}
