using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class Student_Adapter2 : IStudent
	{
		Student_Adaptee2 student;
		public Student_Adapter2(Student_Adaptee2 s)
		{
			student = s;
		}
		public List<string> GetNames()
		{
			List<string> l = new List<string>();
			string[] s = student.identity.Split(",");
			for (int i = 1; i < s.Length; i++)
				l.Add(s[i]);
			return l;
		}
		public string GetSurname()
		{
			string[] s = student.identity.Split(",");
			return s[0];
		}
		public int GetSemester()
		{
			return student.semester;
		}
		public string GetCode()
		{
			return student.code;
		}
		public void ChangeSurname(string s)
		{
			string[] ss = student.identity.Split(",");
			ss[0] = s;
			student.identity = string.Join(" ", ss);
		}
		public void ChangeSemester(int sem)
		{
			student.semester = sem;
		}
		public void ChangeCode(string c)
		{
			student.code = c;
		}
		public List<IMyClass> GetClasses()
		{
			List<IMyClass> cl = new List<IMyClass>();
			string[] s = student.classes.Split(",");
			foreach (var v in s)
				cl.Add((IMyClass)HashMap2.hashmap[v]);
			return cl;
		}

		public void AddClass(IMyClass cl)
		{
			if (student.classes.CompareTo("")==0)
				student.classes = cl.GetCode();
			else
				student.classes = student.classes + "," + cl.GetCode();
		}
		

		public override string ToString()
		{
			string s = "Student name: ";
			foreach (var n in GetNames())
			{
				s = s + $"{n} ";
			}
			s = s + $"{GetSurname()} ";
			s = s + $"Semester: { GetSemester()} Code: {GetCode()}";
			return s;
		}
		public IComparable GetField(string name)
		{
			name = name.ToUpper();
			switch (name)
			{
				//case "NAMES":
				//return GetNames();
				case "SURNAME":
					return GetSurname();
				case "SEMESTER":
					return GetSemester();
				case "CODE":
					return GetCode();
				default:
					return -1;
			}
			}
		}
	public class Student_Adaptee2
	{
		public string identity;
		public int semester;
		public string code;
		public string classes;
		public Student_Adaptee2(List<string> nam, string sur, int sem, string cod, List<IMyClass> cl)
		{
			identity = sur + "," + string.Join(",", nam);
			semester = sem;
			code = cod;
			//classes = string.Join(",", cl.Select(v => v.GetCode()));
			classes = new string("");
		}

	}
}
