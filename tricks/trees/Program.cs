using System;
namespace queue.trees
{
    public class Program
    {
        public Program()
        {
        }

        public static void Main()
        {
            var x = Color.Black;

            //var array = new[] { 10, 9, 4, 2, 3, 6, 1, 5, 8, 7 };
            var array = new [] {10, 15, 5, 2, 7, 8, 6};
                // new[] { 1, 2, 3,// 4, 5 };
            var rb = new RedBlackTree();
            foreach (var item in array)
                rb.Add(item);

            rb.Print();
            rb.Test();
            rb.Print();
        }
    }
}
