using System;
using System.Collections.Generic;

namespace queue.trees
{
    /*
    Every node is colored either red or black.
    Root of the tree is black.
    All leaves are black.
    Both children of a red node are black i.e., there can't be consecutive red nodes.
    All the simple paths from a node to descendant leaves contain the same number of black nodes.
     */

    public enum Color : byte
    {
        Red,
        Black
    }

    public class Node
    {
        public int Index;
        public Node Right { get; set; }
        public Node Left { get; set; }
        public Node Parent { get; set; }
        public Color Color { get; set; } = Color.Red;

        public readonly int Data;

        public Node(Node right, Node left, int data)
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
    }

    public class RedBlackTree
    {
        public Node _root;

        public RedBlackTree()
        {

        }

        public void Add(int data)
        {
            if (_root == null)
            {
                _root = new Node(null, null, Color.Black, data);
                return;
            }

            Add(new Node(null, null, data), _root);
        }

        public void Print()
        {
            var arr = new List<int>();
            Flatten(_root, arr);
            Console.WriteLine(string.Join(",", arr));
        }

        private void Flatten(Node n, List<int> list)
        {
            if (n == null)
                return;

            list.Add(n.Data);
            Flatten(n.Right, list);
            Flatten(n.Left, list);
        }

        private void Add(Node curNode, Node parent)
        {
            if (parent.Data < curNode.Data)
                if (parent.Right == null)
                {
                    parent.Right = curNode;
                    // check
                }
                else
                    Add(curNode, parent.Right);

            else
                if (parent.Left == null)
                {
                    parent.Left = curNode;
                    // check
                }
                else
                    Add(curNode, parent.Left);
        }

        private (Node parent, Node current, bool isRight) BreakMutualLinksWithParent(Node node)
        {
            var parent = node.Parent;
            var isRight = true;
            if (node.Data == node.Parent.Right.Data)
            {
                node.Parent.Right = null;
                node.Parent = null;
            }
            else
            {
                node.Parent.Left = null;
                node.Parent = null;
                isRight = false;
            }

            return (parent, node, isRight);
        }

        public void RotateLeft(Node x, Node xParent)
        {
            if (x == null || xParent == null)
                throw new ArgumentNullException();

            var y = x.Right;
            x.Right = y.Left;
            y.Left = x;

            if (xParent.Left.Data == x.Data)
                xParent.Left = y;
            else
                xParent.Right = y;
        }

        public void RotateRight(Node x, Node xParent)
        {
            if (x == null || xParent == null)
                throw new ArgumentNullException();

            var y = x.Left;
            x.Left = y.Right;
            y.Right = x;

            if (xParent.Left.Data == x.Data)
                xParent.Left = y;
            else
                xParent.Right = y;
        }
    }
}
