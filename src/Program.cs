using System;

namespace Ytyaru.Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Tree<string>();
            tree["A"] = "A";
            tree["A","A1"] = "A1";
            tree["A","A","A11"] = "A11";
            tree["A","A2"] = "A2";
            tree["B"] = "B";
            tree["B","B1"] = "B1";
//            var gen = new Generator();
/*
            var tree = new Tree<string>()
                .Add(new Node("using System;"))
                .Add(new Node(""))
                .Add(new Node("MyNamespace")
                    .Add(new Node("MyClass")
                        .Add(new Node("static void Main(string[] args)")
                            .Add(new Node(@"Console.WriteLine(""Hello world"");")))));
*/
/*
            var tree = new Tree<string>()
                .Add("using System;")
                .Add("")
                .AddChild(new Node<string>("MyNamespace")
                    .Add("MyClass")
                        .Add("static void Main(string[] args)")
                            .Add(@"Console.WriteLine(""Hello world"");")
                );
                */
//            Console.WriteLine($"{tree}");
//            ShowTree(tree);
        }
        static void ShowTree(Tree<string> tree)
        {
            foreach (var node in tree.Children) { ShowNode(node); }
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
