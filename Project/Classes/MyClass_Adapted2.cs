using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class MyClass_Adapter2 : IMyClass
	{
		MyClass_Adaptee2 myclass;
		public MyClass_Adapter2(MyClass_Adaptee2 mc)
		{
			myclass = mc;
		}
		public string GetName()
		{
			return myclass.name;
		}
		public string GetCode()
		{
			return myclass.code;
		}
		public int GetDuration()
		{
			return myclass.duration;
		}
		public void ChangeName(string n)
		{
			myclass.name = n;
		}
		public void ChangeCode(string c)
		{
			myclass.code = c;
		}
		public void ChangeDuration(int d)
		{
			myclass.duration = d;
		}
		public List<ITeacher> GetTeachers()
		{
			List<ITeacher> l = new List<ITeacher>();
			string[] s = myclass.people.Split("$");
			string[] ss = s[0].Split(",");
			foreach (var v in ss)
				l.Add((ITeacher)HashMap2.hashmap[v]);
			return l;

		}
		public List<IStudent> GetStudents()
		{
			List<IStudent> l = new List<IStudent>();
			string[] s = myclass.people.Split("$");
			string[] ss = s[1].Split(",");
			foreach (var v in ss)
				l.Add((IStudent)HashMap2.hashmap[v]);
			return l;
		}

		public void AddStudent(IStudent s)
		{
			if(myclass.people.EndsWith("$"))
				myclass.people = myclass.people +s.GetCode();
			else
				myclass.people = myclass.people +"," +s.GetCode();
		}

		public void AddTeacher(ITeacher t)
		{
			if (myclass.people.StartsWith("$"))
				myclass.people = t.GetCode()+myclass.people;
			else
				myclass.people = t.GetCode() +"," +myclass.people;
		}
		public override string ToString()
		{
			string s = $"Class name: {GetName()} Code: {GetCode()} Duration: {GetDuration()}";
			return s;
		}
		public IComparable GetField(string name)
		{
			name = name.ToUpper();
			switch (name)
			{
				case "NAME":
					return GetName();
				case "CODE":
					return GetCode();
				case "DURATION":
					return GetDuration();
				default:
					return -1;

			}
		}
	}
	public class MyClass_Adaptee2
	{
		public string name;
		public string code;
		public int duration;
		public string people;
		public MyClass_Adaptee2(string nam, string cod, int dur, List<ITeacher> t, List<IStudent> s)
		{
			name = nam;
			code = cod;
			duration = dur;
			people = new string("$");

			//people=string.Join(",", t.Select(v => v.GetCode()));
			//people = people + "$";
			//people = people+string.Join(",", s.Select(v => v.GetCode()));

		}
	}
}
