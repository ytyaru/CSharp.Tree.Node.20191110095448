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
        public Node<V> Add(string key, V value=default) {
            foreach (var child in Children) {
//                if (child.Key == key) { retturn child; } // 既存ならそれを返す
                if (child.Key == key) { return this; } // 既存なら何もしない
            }
            children.Add(new Node<V>(key, value, this)); // 未存なら新規生成する
            return this;
            /*
            if (children.All(c => c.Key != key)) { // 既存なら新規作成しない（keyを一意にするため）
                children.Add(new Node<V>(key, value));
            }
            return this;
            */
        }

//        public Node<V> this[string path] {
        public V this[string path] {
            set {
                if (String.IsNullOrEmpty(path)) { throw new Exception("インデクサには1字以上の文字列を指定してください。"); } 
                // 未存なら新規生成または例外またはnull
                // 既存なら指定値を代入する。インデクサで指定した位置にあるNodeに。
                FindAndNewNode(path, 0, this).Value = value;
//                FindAndNewNode(path, 0, this) = value;
            }
            get {
                if (String.IsNullOrEmpty(path)) { throw new Exception("インデクサには1字以上の文字列を指定してください。"); } 
                // 未存なら新規生成または例外またはnull
                // 既存なら値を返す。インデクサで指定した位置にあるNodeの。
                return FindAndNewNode(path, 0, this).Value;
//                return FindAndNewNode(path, 0, this);
            }
        }
// https://www.infoq.com/jp/articles/csharp-nullable-reference-case-study/
#nullable disable
//        public Node(Node<V> parent, string key, V value)
        public Node(string key, V value=default, Node<V> parent=default)
#nullable enable
        {
            Parent = parent; Key = key; Value = value;
            children = new List<Node<V>>();
            Children = new ReadOnlyCollection<Node<V>>(children);
        }
//        public Node<V>? FindNode(in string delimiter, in string path, int depth, in Node<V> node)
        public Node<V>? FindNode(in string path, int depth, in Node<V> node)
        {
            string[] keys = path.Split(Tree<V>.DELIMITER);
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
#nullable disable
        public Node<V> FindAndNewNode(in string path, int depth, in Node<V> node)
//        public Node<V> FindAndNewNode(in string delimiter, in string path, int depth, in Node<V> node)
        {
            string[] keys = path.Split(Tree<V>.DELIMITER);
            Console.WriteLine($"{path} {depth} {node?.Key} {keys[depth]}");
            if (depth == keys.Length -1) {
                if (node.Key == keys[depth]) { return node; }
                else {
                    // 未存なら新規生成
                    var youngest_brother = new Node<V>(keys[depth], default(V), (null == node.Parent) ? node : node.Parent);
                    if (null == node.Parent) { // nodeがルートなら
                        node.children.Add(youngest_brother);
                    } else {
                        node.Parent.children.Add(youngest_brother);
                    }
                    return youngest_brother;
                }
            }
            else {
//                (null == node.Parent) ? depth : ++depth;
//                if (null != node.Parent) { depth++; }
                depth++;
                Node<V> target = null;
                if (node.Key == keys[depth]) { target = FindNode(path, depth, node); }
                else {
                    foreach (var child in node.Children) {
                        if (child.Key == keys[depth]) {
                            target = FindAndNewNode(path, depth, child);
                            break;
                        }
                    }
                    // 未存なら新規生成
                    if (null == target) {
                        var child = new Node<V>(keys[depth]);
                        node.children.Add(child);
                        target = FindAndNewNode(path, depth, child);
                    }
                }
                return target;
            }
        }
#nullable enable

        /*
#nullable disable
        public Node<V> FindAndNewNode(in string path, int depth, in Node<V> node)
//        public Node<V> FindAndNewNode(in string delimiter, in string path, int depth, in Node<V> node)
        {
            string[] keys = path.Split(Tree<V>.DELIMITER);
            Console.WriteLine($"{path} {depth} {node?.Key} {keys[depth]}");
            if (depth == keys.Length -1) {
                if (node.Key == keys[depth]) { return node; }
                else {
                    // 未存なら新規生成
                    var youngest_brother = new Node<V>(keys[depth], default(V), (null == node.Parent) ? node : node.Parent);
                    if (null == node.Parent) { // nodeがルートなら
                        node.children.Add(youngest_brother);
                    } else {
                        node.Parent.children.Add(youngest_brother);
                    }
                    return youngest_brother;
                }
            }
            else {
//                (null == node.Parent) ? depth : ++depth;
//                if (null != node.Parent) { depth++; }
                depth++;
                Node<V> target = null;
                if (node.Key == keys[depth]) { target = FindNode(path, depth, node); }
                else {
                    foreach (var child in node.Children) {
                        if (child.Key == keys[depth]) {
                            target = FindAndNewNode(path, depth, child);
                            break;
                        }
                    }
                    // 未存なら新規生成
                    if (null == target) {
                        var child = new Node<V>(keys[depth]);
                        node.children.Add(child);
                        target = FindAndNewNode(path, depth, child);
                    }
                }
                return target;
            }
        }
#nullable enable
        */
    }
}
