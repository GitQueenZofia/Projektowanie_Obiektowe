using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class ListCommand:ICommand
    {
        public string[] Args;
        University university;
        Dictionary<string, ICommandFactory> Lists;
        ICommand list;
        int p;
        public void Init(string[] args, University u, string[]args2=null)
        {
            Args = args;
            university = u;
            Lists = new Dictionary<string, ICommandFactory>();
            Lists.Add("STUDENTS", new ListStudentsFactory());
            Lists.Add("TEACHERS", new ListTeachersFactory());
            Lists.Add("CLASSES", new ListClassesFactory());
            Lists.Add("ROOMS", new ListRoomsFactory());
            if (Lists.ContainsKey(Args[0].ToUpper()) == false) throw new InvalidClass(args[0]);
            list = Lists[Args[0].ToUpper()].Create(Args.Skip(1).ToArray(), university);
        }
        public void Execute()
        {
            p = (Console.GetCursorPosition().Top);
            list.Execute();
        }
        public override string ToString()
        {
            return list.ToString();
        }
        public void Undo()
        {
            list.Undo();
        }
    }
    public class ListStudents:ICommand
    {
        public University university;
        int p;
        public void Init(string[] args, University u, string[]args2)
        {
            university = u;
        }
        public void Execute()
        {
            p = (Console.GetCursorPosition().Top);
            foreach (var v in university.students)
                Console.WriteLine(v);
        }
        public override string ToString()
        {
            return "LIST STUDENTS";
        }
        public void Undo()
        {
            int pp= (Console.GetCursorPosition().Top);
            Console.SetCursorPosition(0, p);
            for(int i=0;i<university.students.Count;i++)
                Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
            Console.SetCursorPosition(0, pp);

        }
    }
    public class ListTeachers : ICommand
    {
        public University university;
        int p;
        public void Init(string[] args, University u,string[]args2)
        {
            university = u;
        }
        public void Execute()
        {
            p = (Console.GetCursorPosition().Top);
            foreach (var v in university.teachers)
                Console.WriteLine(v);
        }
        public override string ToString()
        {
            return "LIST TEACHERS";
        }
        public void Undo()
        {
            int pp = (Console.GetCursorPosition().Top);
            Console.SetCursorPosition(0, p);
            for (int i = 0; i < university.teachers.Count; i++)
                Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
            Console.SetCursorPosition(0, pp);
        }
    }
    public class ListRooms : ICommand
    {
        public University university;
        int p;
        public void Init(string[] args, University u,string[]args2)
        {
            university = u;
        }
        public void Execute()
        {
            p = (Console.GetCursorPosition().Top);
            foreach (var v in university.rooms)
                Console.WriteLine(v);
        }
        public override string ToString()
        {
            return "LIST ROOMS";
        }
        public void Undo()
        {
            int pp = (Console.GetCursorPosition().Top);
            Console.SetCursorPosition(0, p);
            for (int i = 0; i < university.rooms.Count; i++)
                Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
            Console.SetCursorPosition(0, pp);
        }
    }
    public class ListClasses : ICommand
    {
        public University university;
        int p;
        public void Init(string[] args, University u,string[]args2)
        {
            university = u;
        }
        public void Execute()
        {
            p = (Console.GetCursorPosition().Top);
            foreach (var v in university.classes)
                Console.WriteLine(v);
        }
        public override string ToString()
        {
            return "LIST CLASSES";
        }
        public void Undo()
        {
            int pp = (Console.GetCursorPosition().Top);
            Console.SetCursorPosition(0, p);
            for (int i = 0; i < university.classes.Count; i++)
                Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
            Console.SetCursorPosition(0, pp);
        }
    }
}
