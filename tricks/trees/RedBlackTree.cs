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
                _root = new Node(null, null, Color.Black, data);
                return;
            }

            Add(new Node(null, null, data), _root);
        }

        private void Add(Node curNode, Node parent)
        {
            if (parent.Data < curNode.Data)
                if (parent.Right == null)
                {
                    Node.SetRight(parent, curNode);
                    // check
                }
                else
                    Add(curNode, parent.Right);

            else
                if (parent.Left == null)
                {
                    Node.SetLeft(parent, curNode);
                    // check
                }
                else
                    Add(curNode, parent.Left);
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
            if (n == null)
                return;

            list.Add(n.Data);
            Flatten(n.Right, list);
            Flatten(n.Left, list);
        }

        #endregion Printing
    }
}
