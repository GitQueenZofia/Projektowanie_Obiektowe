using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public class Student: IStudent
	{
		List<string> names;
		string surname;
		int semester;
		string code;
		public List<IMyClass> classes;
		public List<string> GetNames()
		{
			return names; 
		}
		public string GetSurname()
		{
			return surname; 
		}
		public int GetSemester()
		{
			 return semester; 
		}
		public string GetCode()
		{
		     return code; 
		}
		public void ChangeSurname(string s)
		{
			surname = s;
		}
		public void ChangeSemester(int sem)
		{
			semester = sem;
		}
		public void ChangeCode(string c)
		{
			code = c;
		}
		public List<IMyClass> GetClasses()
        {
			return classes;
        }
		public void AddClass(IMyClass cl)
		{
			classes.Add(cl);
		}
		
		
		public Student(List<string> nam,string sur, int sem, string cod, List<IMyClass> cl)
        {
			names = nam;
			surname = sur;
			semester = sem;
			code = cod;
			classes = cl;
        }
        public override string ToString()
        {
			string s = "Student name: ";
			foreach(var n in names)
            {
				s = s + $"{n} ";
            }
			s = s + $"{surname} ";
			s = s + $"Semester: { semester} Code: {code}";
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
}