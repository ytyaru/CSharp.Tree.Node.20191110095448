using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // ReadOnlyCollection

namespace System.Collections
{
    class Node<T>
    {
//        protected string Value { get; }
        protected IList<Node> children { get; set; }
        protected IReadOnlyList<Node> Children { get; }
        public Node(T value)
        {
            children = new List<Node>();
            Children = new ReadOnlyCollection<Node>(children);
            Value = value;
        }
        public IReadOnlyList<Node> GetChildren() => Children;
        public Node Add(Node node) { children.Add(node); return this; }
        public virtual T Value { get; protected set; }
//        public virtual string Generate() => Value;
    }
}
