using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface StudentFactory
    {
        public IStudent Create(List<string> nam, string sur, int sem, string cod, List<IMyClass> cl);
    }
    public class StudentBase:StudentFactory
    {
        public IStudent Create(List<string> nam, string sur, int sem, string cod, List<IMyClass> cl)
        {
            return new Student(nam, sur, sem, cod,cl);
        }
    }
    public class StudentSecond : StudentFactory
    {
        public IStudent Create(List<string> nam, string sur, int sem, string cod, List<IMyClass> cl)
        {
            return new Student_Adapter2(new Student_Adaptee2(nam,sur,sem,cod,cl));
        }
    }

    public interface TeacherFactory
    {
        public ITeacher Create(List<string> nam, string sur, string r, string cod, List<IMyClass> cl);
    }
    public class TeacherBase : TeacherFactory
    {
        public ITeacher Create(List<string> nam, string sur, string r, string cod, List<IMyClass> cl)
        {
            return new Teacher(nam, sur, r, cod, cl);
        }
    }
    public class TeacherSecond : TeacherFactory
    {
        public ITeacher Create(List<string> nam, string sur, string r, string cod, List<IMyClass> cl)
        {
            return new Teacher_Adapter2(new Teacher_Adaptee2(nam, sur, r, cod, cl));
        }
    }

    public interface RoomFactory
    {
        public IRoom Create(int n, string t, List<IMyClass> c);
    }
    public class RoomBase : RoomFactory
    {
        public IRoom Create(int n, string t, List<IMyClass> c)
        {
            return new Room(n,t,c);
        }
    }
    public class RoomSecond : RoomFactory
    {
        public IRoom Create(int n, string t, List<IMyClass> c)
        {
            return new Room_Adapter2(new Room_Adaptee2(n,t,c));
        }
    }

    public interface ClassFactory
    {
        public IMyClass Create(string nam, string cod, int dur, List<ITeacher> t, List<IStudent> s);
    }
    public class ClassBase : ClassFactory
    {
        public IMyClass Create(string nam, string cod, int dur, List<ITeacher> t, List<IStudent> s)
        {
            return new MyClass(nam,cod,dur,t,s);
        }
    }
    public class ClassSecond : ClassFactory
    {
        public IMyClass Create(string nam, string cod, int dur, List<ITeacher> t, List<IStudent> s)
        {
            return new MyClass_Adapter2(new MyClass_Adaptee2(nam,cod,dur,t,s));
        }
    }






}
