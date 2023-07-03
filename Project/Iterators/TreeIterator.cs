using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public enum Direction { Left, Right, None, Current }
	public class TreeIterator<T>:IIterator<T>
	{
        Stack<TreeNode<T>> stack;
        TreeNode<T> root;
        public TreeIterator(TreeNode<T> root)
        {
            this.root = root;
            stack = new Stack<TreeNode<T>>();
            stack.Push(root);
        }
        public IIterator<T> Next()
        {
            TreeNode<T> node = stack.Pop();
                if (node.right != null)
                {
                    stack.Push(node.right);
                }
                if (node.left != null)
                {
                    stack.Push(node.left);
                }
            if (stack.Count == 0) return null;
            return this;
        }
        public T Value()
        {
            return stack.Peek().value;
        }
	}
}
