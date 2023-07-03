using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class RVectorIterator<T> : IIterator<T>
	{
		public MyVector<T> vector;
		public int index;
		public RVectorIterator(MyVector<T> v)
		{
			this.vector = v;
			index = vector.array.Length-1;

		}
		public IIterator<T> Next()
		{
			index--;
			if (index < 0) return null;
			return this;
		}
		public T Value()
		{
			return vector.array[index];
		}
	}
}

