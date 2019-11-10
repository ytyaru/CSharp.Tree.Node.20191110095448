using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // ReadOnlyCollection

namespace Ytyaru.Collections
{
    public class Node<V>
    {
        public string Key { get; protected set; }
//        public virtual V Value { get; protected set; }
        public virtual V Value { get; set; }
//        public Node<V> Parent { get; protected set; }
        public Node<V> Parent { get; set; }
        protected IList<Node<V>> children { get; set; }
        public IReadOnlyList<Node<V>> Children { get; }
        /*
        public Node<V> Add(K key, V value) {
            if (Children.All(c => c.Key != key)) { // 既存なら新規作成しない（keyを一意にするため）
                children.Add(new Node<V>(key, value));
            }
            return this;
        }
        */
//        public Node<V> this[string path] {
        public V this[string path] {
            set {
                // 未存なら新規生成または例外またはnull
                // 既存なら指定値を代入する。インデクサで指定した位置にあるNodeに。
                FindAndNewNode(path, 0, this).Value = value;
//                FindAndNewNode(path, 0, this) = value;
            }
            get {
                // 未存なら新規生成または例外またはnull
                // 既存なら値を返す。インデクサで指定した位置にあるNodeの。
                return FindAndNewNode(path, 0, this).Value;
//                return FindAndNewNode(path, 0, this);
            }
        }
        protected Node(Node<V> parent, string key, V value)
        {
            Parent = parent; Key = key; Value = value;
            children = new List<Node<V>>();
            Children = new ReadOnlyCollection<Node<V>>(children);
        }
        /*
        public Node(V value)
        {
            children = new List<Node<T>>();
            Children = new ReadOnlyCollection<Node<T>>(children);
            Value = value;
        }
        public Node<V>() where T : new()
        {
            children = new List<Node<T>>();
            Children = new ReadOnlyCollection<Node<T>>(children);
            Value = new T();
        }
        */
        /*
        public Node()
        {
            children = new List<Node<T>>();
            Children = new ReadOnlyCollection<Node<T>>(children);
            Value = default;
        }
        */
//        public Node<V>? FindNode(in string delimiter, in string path, int depth, in Node<V> node)
        public Node<V>? FindNode(in string path, int depth, in Node<V> node)
        {
//            string keys = path.Split(Delimiter);
//            string keys = path.Split(delimiter);
            string keys = path.Split(Tree<V>.DELIMITER);
            if (depth == keys.Length -1) {
                if (node.Key == keys[depth]) { return node; }
//                else { throw new NotExist(); }
                else { return null; }
            }
            else {
                foreach (var child in node.Children) {
                    if (child.Key == keys[depth]) {
                        FindNode(path, ++depth, child);
                    }
                }
            }
//            throw new NotExist();
            return null;
        }
        public Node<V> FindAndNewNode(in string path, int depth, in Node<V> node)
//        public Node<V> FindAndNewNode(in string delimiter, in string path, int depth, in Node<V> node)
        {
//            string keys = path.Split(Delimiter);
            string keys = path.Split(Tree.DELIMITER);
            if (depth == keys.Length -1) {
                if (node.Key == keys[depth]) { return node; }
                else {
                    // 未存なら新規生成
                    var youngest_brother = new Node<T>(keys[depth]);
                    node.Parent.Add(youngest_brother);
                    return youngest_brother;
                }
            }
            else {
                depth++;
                var target = null;
                if (node.Key == keys[depth]) { target = FindNode(path, depth, node); }
                else {
                    foreach (var child in node.Children) {
                        if (child.Key == keys[depth]) {
                            target = FindNode(path, depth, child);
                            break;
                        }
                    }
                    // 未存なら新規生成
                    if (null == target) {
                        var child = new Node<T>(keys[depth]);
                        node.Children.Add(child);
                        target = FindNode(path, depth, child);
                    }
                }
                return target;
            }
        }
    }
}
