using System.Collections.Generic;
using System.IO;

namespace CodingBot.DotLang
{
    // http://www.graphviz.org/doc/info/lang.html
    public class DotLangDocument
    {
        private IReadOnlyDictionary<string, string>? myLabels;

        public Graph.Graph? Graph { get; private set; }

        public static DotLangDocument Load(string path)
        {
            var doc = new DotLangDocument();

            using (var reader = new StreamReader(path))
            {
                doc.Read(reader);
            }

            return doc;
        }

        public void Read(TextReader reader)
        {
            var visitor = new DotAstVisitor();

            var lexer = new Lexer(reader.ReadToEnd());
            var parser = new Parser(lexer, visitor);
            parser.Parse();

            Graph = visitor.Graph;
            myLabels = visitor.Labels;
        }

        public string GetLabel(string id) =>
            myLabels != null && myLabels.TryGetValue(id, out var value) ? value : id;
    }
}
