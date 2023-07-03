using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class Teacher_Adapter2 : ITeacher
	{
		Teacher_Adaptee2 teacher;
		public Teacher_Adapter2(Teacher_Adaptee2 t)
		{
			teacher = t;
		}
		public List<string> GetNames()
		{
			List<string> l = new List<string>();
			string[] s = teacher.identity.Split(",");
			for (int i = 1; i < s.Length; i++)
				l.Add(s[i]);
			return l;
		}
		public string GetSurname()
        {
			string[] s = teacher.identity.Split(",");
			return s[0];
		}
		public string GetRank()
		{
			return teacher.rank;
		}
		public string GetCode()
		{
			return teacher.code;
		}
		public void ChangeSurname(string s)
		{
			string[] ss = teacher.identity.Split(",");
			ss[0] = s;
			teacher.identity = string.Join(" ", ss);
		}
		public void ChangeRank(string r)
		{
			teacher.rank = r;
		}
		public void ChangeCode(string c)
		{
			teacher.code = c;
		}
		public List<IMyClass> GetClasses()
		{
			List<IMyClass> cl = new List<IMyClass>();
			string[] s = teacher.classes.Split(",");
			foreach (var v in s)
				cl.Add((IMyClass)HashMap2.hashmap[v]);
			return cl;
		}

		public void AddClass(IMyClass cl)
		{
			if (teacher.classes.CompareTo("")==0)
				teacher.classes = cl.GetCode();
			else
				teacher.classes = teacher.classes + "," + cl.GetCode();

		}
		public override string ToString()
		{
			string s = "Teacher name: ";
			foreach (var n in GetNames())
				s = s + $"{n} ";
			s = s + $"{GetSurname()} Rank: {GetRank()} Code: {GetCode()}";
			return s;

		}
		public IComparable GetField(string name)
		{
			name = name.ToUpper();
			switch (name)
			{
				// case "NAME":
				// return GetName();
				case "SURNAME":
					return GetSurname();
				case "CODE":
					return GetCode();
				case "RANK":
					return GetRank();
				default:
					return -1;

			}
		}
	}
	public class Teacher_Adaptee2
	{
		public string identity;
		public string rank;
		public string code;
		public string classes;
		public Teacher_Adaptee2(List<string> nam, string sur, string r, string cod, List<IMyClass> cl)
		{
			identity = sur + "," + string.Join(",", nam);
			rank = r;
			code = cod;
			//classes = string.Join(",", cl.Select(v => v.GetCode()));
			classes = new string("");
		}
	}
}
