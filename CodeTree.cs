using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // ReadOnlyCollection

namespace System.Collections
{
    class Tree
    {
        protected IList<Node> children;
        public IReadOnlyList<Node> Children { get; }
        public Tree()
        {
            children = new List<Node>();
            Children = new ReadOnlyCollection<Node>(children);
        }
        public Tree Add(Node node) { children.Add(node); return this; }
        public IReadOnlyList<Node> GetChildren() => Children;
    }
}
