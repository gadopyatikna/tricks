using System;
namespace queue.trees
{
    static class NodeBuilder
    {
        public static Node GetLeafNode(Node parent)
            => new Node(true, -1) { Parent = parent, Color = Color.Black };
    }
}
