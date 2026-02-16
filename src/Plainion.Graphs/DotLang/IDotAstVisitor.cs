using System.Collections.Generic;
using CodingBot.DotLang.Graph;

namespace CodingBot.DotLang
{
    public interface IDotAstVisitor
    {
        Node VisitNode(string nodeId);
        Edge VisitEdge(string sourceNodeId, string targetNodeId);
        Cluster VisitCluster(string clusterId, IEnumerable<string> nodes);
        void VisitLabel(string id, string value);
    }
}