﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class RListIterator<T> : IIterator<T>
	{
		Node<T> current;
		public RListIterator(Node<T> c)
		{
		    current = new Node<T>(c.value, c.prev, c.next);
		}
		public IIterator<T> Next()
		{
			current = current.prev;
			return this;
		}
		public T Value()
		{
			return current.value;
		}
	}
}
