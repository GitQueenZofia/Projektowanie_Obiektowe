using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	public enum Ranks { MiB, GiB, TiB, KiB };
	public class Teacher: ITeacher
	{
		List<string> names;
		string surname;
		string rank;
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
		public string GetRank()
		{
			 return rank;
		}
		public string GetCode()
        {
            return code; 
        }
		public void ChangeSurname(string s)
        {
			surname = s;
        }
		public void ChangeRank(string r)
        {
			rank = r;
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
		public Teacher(List<string> nam,string sur, string r,string cod, List<IMyClass>cl)
        {
			names = nam;
			surname = sur;
			rank = r;
			code = cod;
			classes = cl;
        }
        public override string ToString()
        {
			string s = "Teacher name: ";
			foreach (var n in names)
				s = s + $"{n} ";
			s = s + $"{surname} Rank: {rank} Code: {code}";
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

}