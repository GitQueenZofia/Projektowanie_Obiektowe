using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class MyTree<T>: ICollection<T>
	{
        TreeNode<T> root;
        int length = 0;
		public void AddObject(T obj)
        {
            if (length == 0)
            {
                length++;
                root = new TreeNode<T>(obj, null, null, null);
                return;
            }
            Random rnd = new Random();
            length++;    
            if (root.left==null)
            {
                root.left = new TreeNode<T>(obj, null, null, root);
                return;
            }
            if (root.right == null)
            {
                root.right = new TreeNode<T>(obj, null, null, root);
                return;
            }
            TreeNode<T> p = root;
            while (p.left != null && p.right != null)
            {
                int a = rnd.Next(2);
                p = a == 0 ? p.left : p.right;
            }
            if (p.left == null)
                p.left = new TreeNode<T>(obj, null, null, p);
            else p.right = new TreeNode<T>(obj, null, null, p);

        }
		public bool DeleteObject(T obj)
        {
            if (root == null) return false;
            length--;
            if (root.right == null && root.left == null)
            {
                root = null;
                return true;
            }
            TreeNode<T> p = root;
            int last = 0;
            while (p.left != null || p.right != null)
            {
                if (p.left != null)
                {
                    p = p.left;
                    last = 0;
                }
                else
                {
                    p = p.right;
                    last = 1;
                }
            }
            if (last == 0)
            {
                p.parent.left = null;
                return true;
            }
            else
            {
                p.parent.right = null;
                return true;
            }
        }
		public int GetLength()
        {
            return length;
        }
		public IIterator<T> GetForwardIterator()
        {
            return new TreeIterator<T>(root);
        }
		public IIterator<T> GetReverseIterator()
        {
            return new RTreeIterator<T>(root);
        }
	}
    public class TreeNode<T>
    {
        public T value;
        public TreeNode<T> right;
        public TreeNode<T> left;
        public TreeNode<T> parent;
        public TreeNode(T val, TreeNode<T> l, TreeNode<T> r,TreeNode<T> p)
        {
            value = val;
            left = l;
            right = r;
            parent = p;
        }
    }
}
