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

    public class RedBlackTree
    {
        private Node _root;

        public void Add(int data)
        {
            if (_root == null)
            {
                _root = new Node(false, data)
                {
                    Color = Color.Black
                };
                _root.Left = NodeBuilder.GetLeafNode(_root);
                _root.Right = NodeBuilder.GetLeafNode(_root);

                return;
            }

            var newNode = new Node(false, data)
            {
                Color = Color.Red
            };
            newNode.Left = NodeBuilder.GetLeafNode(newNode);
            newNode.Right = NodeBuilder.GetLeafNode(newNode);

            Add(newNode, _root);
        }

        private void Add(Node curNode, Node parent)
        {
            if (parent.Data < curNode.Data)
                if (parent.Right.IsLeaf)
                {
                    Node.SetRight(parent, curNode);
                    ValidateRedBlacknessPreserved(curNode);
                }
                else
                    Add(curNode, parent.Right);

            else
                if (parent.Left.IsLeaf)
                {
                    Node.SetLeft(parent, curNode);
                    ValidateRedBlacknessPreserved(curNode);
                }
                else
                    Add(curNode, parent.Left);
        }

        private void ValidateRedBlacknessPreserved(Node node)
        {
            while (node.Parent != null // if we are red root, we re fine with that cause we have no parents
                && node.Parent.Color == Color.Red)
            {
                if (node.Parent.Data == _root.Data)
                    _root.Color = Color.Black;

                // my parent is left child cases
                if (node.Parent.Data == node.Parent.Parent.Left.Data)
                {
                    if (node.Parent.Parent.Right.Color == Color.Red)
                    {
                        node.Parent.Color = Color.Black; // p was Red
                        node.Parent.Parent.Color = Color.Red; // gp was Black
                        node.Parent.Parent.Right.Color = Color.Black; // u was Red

                        node = node.Parent.Parent; // we recolored GP to Red, let's check he is ok
                    }

                    else //if (node.Parent.Parent.Right.Color == Color.Black)
                    {
                        // line case, z left child of p
                        if (node.Data == node.Parent.Left.Data)
                        {
                            RotateRight(node.Parent.Parent);

                            node.Parent.Color = Color.Black; // p was red
                            node.Parent.Parent.Color = Color.Red; // gp was black

                            node = node.Parent;
                        }
                        else // triangle case, z right child of p
                        {
                            RotateLeft(node.Parent);
                        }
                    }
                }
            }
        }

        private void RotateLeft(Node x)
        {
            var (xParent_, x_, isRight) = Node.BreakMutualLinksWithParent(x);
            var (_, y_, _) = Node.BreakMutualLinksWithParent(x_.Right);
            var (_, yLeft_, _) = Node.BreakMutualLinksWithParent(y_.Left);

            Node.SetRight(x_, yLeft_);
            Node.SetLeft(y_, x_);

            if (isRight)
                Node.SetRight(xParent_, y_);
            else
                Node.SetLeft(xParent_, y_);
        }

        private void RotateRight(Node x)
        {
            var (xParent_, x_, isRight) = Node.BreakMutualLinksWithParent(x);
            var (_, y_, _) = Node.BreakMutualLinksWithParent(x_.Left);
            var (_, yRight_, _) = Node.BreakMutualLinksWithParent(y_.Right);

            Node.SetLeft(x_, yRight_);
            Node.SetRight(y_, x_);

            if (isRight)
                Node.SetRight(xParent_, y_);
            else
                Node.SetLeft(xParent_, y_);
        }

        public void Test()
        {
            RotateLeft(_root.Left);
            RotateRight(_root.Left);
        }

        #region Printing
        public void Print()
        {
            var arr = new List<int>();
            Flatten(_root, arr);
            Console.WriteLine(string.Join(",", arr));
        }

        private void Flatten(Node n, List<int> list)
        {
            if (n.IsLeaf)
                return;

            list.Add(n.Data);
            Flatten(n.Right, list);
            Flatten(n.Left, list);
        }

        #endregion Printing
    }
}
