using System;

namespace Project
{
	public static class Algorithm<T>
    {
        public static T Find(ICollection<T> c,Predicate<T> pred)
        {
            IIterator<T> i = c.GetForwardIterator();

            while(i!=null)
            {
                if(pred(i.Value()))
                {
                    return i.Value();
                }
                i = i.Next();
                
            }
            return default(T);
        }
        public static void Print(ICollection<T> c, Predicate<T> pred)
        {
            IIterator<T> i = c.GetForwardIterator();
            while (i!=null)
            {
                if (pred(i.Value()))
                {
                    Console.WriteLine(i.Value());
                }
                i = i.Next();
            }
        }
        public static void Print2(ICollection<T> c,int it)
        {
            IIterator<T> i = it == 0 ? c.GetForwardIterator() : c.GetReverseIterator();
            while (i!=null)
            {
                Console.WriteLine(i.Value());
                i = i.Next();
            }
        }
        public static void ForEach(IIterator<T> it,Action<T> f)
        {
            while(it!=null)
            {
                f(it.Value());
                it = it.Next();
            }
        }
        public static int CountIf(IIterator<T> it , Predicate<T> pred)
        {
            int i = 0;
            while (it!=null)
            {
                if (pred(it.Value())) i++;
                it = it.Next();
            }

            return i;
        }
        public static T Find(IIterator<T> i, Predicate<T> pred)
        {
            while (i != null)
            {
                if (pred(i.Value()))
                {
                    return i.Value();
                }
                i = i.Next();

            }
            return default(T);
        }


    }
	
}
