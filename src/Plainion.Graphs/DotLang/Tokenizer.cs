namespace Plainion.Graphs.DotLang;

class Tokenizer
{
    private readonly char[] myChars;
    private readonly Stack<int> mySnapshotIndexes = new();
    private int myIndex;

    public Tokenizer(string source)
    {
        myChars = source.ToCharArray();
    }

    public char Current => myIndex < myChars.Length ? myChars[myIndex] : default;

    public bool EndOfStream => myIndex >= myChars.Length;

    public void Consume() => myIndex++;

    public void TakeSnapshot() => mySnapshotIndexes.Push(myIndex);

    public void RollbackSnapshot() => myIndex = mySnapshotIndexes.Pop();

    public void CommitSnapshot() => mySnapshotIndexes.Pop();
}
