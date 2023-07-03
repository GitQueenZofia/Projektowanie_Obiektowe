using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface IObject
    {
        public string ToString();
    }
	public interface IMyClass:IObject
    {
        public string GetName();
        public string GetCode();
        public int GetDuration();
        public List<ITeacher> GetTeachers();
        public List<IStudent> GetStudents();
        public void AddStudent(IStudent s);
        public void AddTeacher(ITeacher t);
        public void ChangeName(string n);
        public void ChangeCode(string c);
        public void ChangeDuration(int d);
        
        
    }
    public interface IRoom:IObject
    {
        public int GetNumber();
        public string GetRoomType();
        public List<IMyClass> GetClasses();
        public void AddClass(IMyClass cl);
        public void ChangeNumber(int n);
        public void ChangeRoomType(string t);
       
    }
    public interface IStudent:IObject
    {
        public List<string> GetNames();
        public string GetSurname();
        public int GetSemester();
        public string GetCode();
        public List<IMyClass> GetClasses();
        public void AddClass(IMyClass cl);
        public void ChangeSurname(string s);
        public void ChangeSemester(int s);
        public void ChangeCode(string c);
       
    }
    public interface ITeacher:IObject
    {
        public List<string> GetNames();
        public string GetSurname();
        public string GetRank();
        public string GetCode();
        public List<IMyClass> GetClasses();
        public void AddClass(IMyClass cl);
        public void ChangeSurname(string s);
        public void ChangeRank(string r);
        public void ChangeCode(string c);
    }
}
