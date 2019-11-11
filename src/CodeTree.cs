using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // ReadOnlyCollection

namespace Ytyaru.Collections
{
    public class Tree<V>
    {
        public static readonly char DELIMITER = '/';
        public char Delimiter { get; protected set; }
        protected IList<Node<V>> children { get; set; }
        public IReadOnlyList<Node<V>> Children { get; }
        public Node<V> Root { get; protected set; }
//        public Node<V> this[string path] {
        public V this[string path] {
            set {
                // 未存なら新規生成または例外
                // 既存なら指定値を代入する。インデクサで指定した位置にあるNodeに。
//                Root.FindNode(path, 0, Root).Value = value;
                Root.FindAndNewNode(path, 0, Root).Value = value;
            }
            get {
                // 未存なら新規生成または例外
                // 既存なら値を返す。インデクサで指定した位置にあるNodeの。
//                return Root.FindNode(path, 0, Root);
                return Root.FindAndNewNode(path, 0, Root).Value;
            }
        }
        public Tree(char delimiter='/')
        {
            Delimiter = delimiter;
            children = new List<Node<V>>();
            Children = new ReadOnlyCollection<Node<V>>(children);
            Root = new Node<V>("Root");
        }
    }
}
