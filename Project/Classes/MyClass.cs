using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class MyClass : IMyClass
	{
		string name;
		string code;
		int duration;
		public List<ITeacher> teachers;
		public List<IStudent> students;
		public string GetName()
		{
			return name;
		}
		public string GetCode()
		{
			return code;
		}
		public int GetDuration()
		{
			return duration;
		}
		public void ChangeName(string n)
        {
			name = n;
        }
		public void ChangeCode(string c)
        {
			code = c;
        }
		public void ChangeDuration(int d)
        {
			duration = d;
        }


		public List<ITeacher> GetTeachers()
		{
			return teachers;
		}
		public List<IStudent> GetStudents()
		{
			return students;
		}
		public void AddStudent(IStudent s)
		{
			students.Add(s);
		}
		public void AddTeacher(ITeacher t)
		{
			teachers.Add(t);
		}
		public MyClass(string nam, string cod,int dur, List<ITeacher> t,List<IStudent>s)
        {
			name = nam;
			code = cod;
			duration = dur;
			teachers = t;
			students = s;
        }
        public override string ToString()
        {
			string s = $"Class name: {name} Code: {code} Duration: {duration}";
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
}