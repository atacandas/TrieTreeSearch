using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp
{
    public class Node
    {
        public char Value { get; set; }
        public string FullValue { get; set; }
        public List<Node> Children { get; set; }
        public Node Parent { get; set; }
        public int Depth { get; set; }

        public Node(char value, int depth, Node parent)
        {
            Value = value;
            Depth = depth;
            Parent = parent;
            Children = new List<Node>();
        }
        public bool IsLeaf()
        {
            return Children.Count == 0;
        }
        public Node FindChildNode(char c)
        {
            foreach (var child in Children)
            {
                if (child.Value==c)
                {
                    return child;
                }
            }
            return null;
        }

        public void DeleteChildNode(char c)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].Value==c)
                {
                    Children.RemoveAt(i);
                }
            }
        }
    }
}
