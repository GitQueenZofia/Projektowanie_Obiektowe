using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class VectorIterator<T> : IIterator<T>
	{
		MyVector<T> vector;
		public int index;
		public VectorIterator(MyVector<T> v)
		{
			this.vector = v;
			index = 0;
		}
		public IIterator<T> Next()
		{
			index++;
			if (index >= vector.array.Length) return null;
			return this;
		}
		public T Value()
		{
			return vector.array[index];
		}
		public bool HasNext()
        {
			return index + 1 < vector.array.Length;
        }
	}
}

