using System;

namespace queue.trees
{
    public enum Color : byte
    {
        Red = 0,
        Black = 0b_11111111
    }

    class NodeBase
    {
        public readonly bool IsLeaf;

        public NodeBase(bool isLeaf)
        {
            IsLeaf = isLeaf;
        }
    }

    class LeafNode : NodeBase
    {
        public LeafNode()
            : base(true)
        {}
    }

    class Node : NodeBase
    {
        public int Index;
        public Node Right { get; set; }
        public Node Left { get; set; }
        public Node Parent { get; set; }
        public Color Color { get; set; } = Color.Red;
        public readonly int Data;

        public Node(Node right, Node left, int data)
            : base(false)
        {
            Right = right;
            Left = left;
            Data = data;
        }

        public Node(Node right, Node left, Color color, int data)
            : this(right, left, data)
        {
            Color = color;
        }

        public void SwitchColor() => Color = ~Color;

        public static void SetRight(Node node, Node right)
        {
            node.Right = right;
            right.Parent = node;
        }

        public static void SetLeft(Node node, Node left)
        {
            node.Left = left;
            left.Parent = node;
        }

        public static (Node parent, Node current, bool isRight)
            BreakMutualLinksWithParent(Node node)
        {
            var parent = node.Parent;
            var isRight = true;
            if (node.Parent.Right != null && node.Data == node.Parent.Right.Data)
            {
                node.Parent.Right = null;
                node.Parent = null;
            }
            else if (node.Parent.Left != null && node.Data == node.Parent.Left.Data)
            {
                node.Parent.Left = null;
                node.Parent = null;
                isRight = false;
            }
            else
                throw new Exception("Cannot roatate with both children null");

            return (parent, node, isRight);
        }
    }
}
