using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{ 
    public class RTreeIterator<T> : IIterator<T>
    {
        Stack<TreeNode<T>> stack;
        TreeNode<T> root;
        public RTreeIterator(TreeNode<T> root)
        {
            this.root = root;
            stack = new Stack<TreeNode<T>>();
            Traverse(root);
        }
        public IIterator<T> Next()
        {
            TreeNode<T> node = stack.Pop();
            if (stack.Count == 0) return null;
            return this;

        }
        public T Value()
        {
            return stack.Peek().value;
        }
        private void Traverse(TreeNode<T> node)
        {
            if (node != null)
            {
                stack.Push(node);
                Traverse(node.left);
                Traverse(node.right);
            }
        }
    }
}
