using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public interface IIterator<T>
	{
		public IIterator<T> Next();
		public T Value();
	}
}
