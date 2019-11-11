using System;

namespace Ytyaru.Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Tree<string>();
            tree["A"] = "A";
            tree["A/A1"] = "A1";
            tree["A/A1/A11"] = "A11";
            tree["A/A2"] = "A2";
            tree["B"] = "B";
            tree["B/B1"] = "B1";
            Console.WriteLine($"tree.Root.Children.Count: {tree.Root.Children.Count}");
            ShowTree(tree);
        }
        static void ShowTree(Tree<string> tree)
        {
            foreach (var node in tree.Root.Children) { ShowNode(node); }
        }
        static void ShowNode(Node<string> node, int index=0)
        {
            if (null != node) { Console.WriteLine($"{(new string('*', index)).Replace("*", "    ")}{node.Value}"); }
            if (null != node?.Children) {
                index++;
                foreach (var n in node.Children) { ShowNode(n, index); }
            }
        }
    }
}
