using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class FindCommand : ICommand
    {
        Dictionary<string, ICommandFactory> Finds;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        
        ICommand find;
        public string Name { get; } = "FIND";
        public string Description { get; } = "Prints objects matching certain conditions";
        public void Init(string[]args,University u, string[]args2)
        {

            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            Finds = new Dictionary<string, ICommandFactory>();
            Finds.Add("STUDENTS", new FindStudentsFactory());
            Finds.Add("TEACHERS", new FindTeachersFactory());
            Finds.Add("ROOMS", new FindRoomsFactory());
            Finds.Add("CLASSES", new FindClassesFactory());

            if (Finds.ContainsKey(args[0].ToUpper()) == false) throw new InvalidClass(args[0]);
            find = Finds[args[0].ToUpper()].Create(args.Skip(1).ToArray(),u, args2);

        }
        public void Execute()
        {
            find.Execute();
        }
        public override string ToString()
        {
            return find.ToString();
        }
        public void Undo()
        {
            find.Undo();
        }
    }
    public class FindStudents : ICommand
    {
        System.Collections.Generic.ICollection<IStudent> students;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IStudent, int,bool>> fields;
        int p;
        int count = 0;
        string[] Args;
        public string Name { get; } = "FIND STUDENTS";
        public string Description { get; } = "Prints students matching certain conditions";
        public void Init(string[]args,University u,string[]args2=null)
        {
            Args = args;
            
            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[],IStudent,int,bool>>();
            fields.Add("SURNAME", PrintSurname);
            fields.Add("CODE", PrintCode);
            fields.Add("SEMESTER", PrintSemester);
            fields.Add("", Print);
            students = u.students;

            if (args.Length!=1&&args.Length%3!=0)
                throw new NotEnough(args[0]);

            for (int i = 0; i < args.Length-2; i += 3)
            {
                if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                if (args.Length > 1 && !preds.ContainsKey(args[i + 1])) throw new InvalidArg(args[i + 1]);  
            }
        }
        public void Execute()
        {
            count = 0;
            p = Console.GetCursorPosition().Top;
            foreach (var v in students)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good)
                {
                    Console.WriteLine(v);
                    count++;
                }
            }
        }
        private bool PrintSurname(string[] s, IStudent st,int i)
        {
            if (!preds[s[i+1].ToUpper()](st.GetSurname(), s[i+2])) return false;
            return true;
        }
        private bool PrintCode(string[] s, IStudent st,int i)
        {
            if (!preds[s[i+1].ToUpper()](st.GetCode(), s[i+2])) return false;
            return true;        
        }
        private bool PrintSemester(string[] s,IStudent st,int i)
        {
            if (!preds[s[i+1].ToUpper()](st.GetSemester(), int.Parse(s[i+2]))) return false;
            return true;               
        }
        private bool Print(string[] s,IStudent st,int i)
        {
            return true;
        }
        public override string ToString()
        {
            string s = $"FIND STUDENTS ";
            for (int i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            return s;
        }
        public void Undo()
        {
            int pp = (Console.GetCursorPosition().Top);
            Console.SetCursorPosition(0, p);
            for (int i = 0; i < count; i++)
                Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
            Console.SetCursorPosition(0, pp);
        }

    }
    public class FindTeachers : ICommand
    {
        System.Collections.Generic.ICollection<ITeacher> teachers;
        bool[] goodteachers;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], ITeacher, int, bool>> fields;
        string[] Args;
        int p;
        int count = 0;
        public string Name { get; } = "FIND TEACHERS";
        public string Description { get; } = "Prints teachers matching certain conditions";
        public void Init(string[] args,University u, string[]args2=null)
        {
            Args = args;
            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], ITeacher, int, bool>>();
            fields.Add("SURNAME", PrintSurname);
            fields.Add("CODE", PrintCode);
            fields.Add("RANK", PrintRank);
            fields.Add("", Print);
            teachers = u.teachers;
            goodteachers = new bool[teachers.Count];

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);

            foreach(var v in teachers)
            {
                for(int i=0;i<args.Length-2;i+=3)
                {
                    if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                    if (!preds.ContainsKey(args[i+1])) throw new InvalidArg(args[i+1]);
                }
            }
            

        }
        public void Execute()
        {
            count = 0;
            p = Console.GetCursorPosition().Top;
            foreach (var v in teachers)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;            
                }
                if (good)
                {
                    Console.WriteLine(v);
                    count++;
                }
            }
        }
        private bool PrintSurname(string[] s,ITeacher t,int i)
        {
            if (preds[s[i + 1].ToUpper()](t.GetSurname(), s[i + 2])) return false;
            return true;
        }
        private bool PrintCode(string[] s, ITeacher t, int i)
        {
            if (!preds[s[i + 1].ToUpper()](t.GetCode(), s[i + 2])) return false;
            return true;         
        }
        private bool PrintRank(string[] s, ITeacher t, int i)
        {
            if (!preds[s[i + 1].ToUpper()](t.GetRank(), s[i + 2])) return false;
            return true;       
        }
        private bool Print(string[] s, ITeacher t, int i)
        {
            return true;
        }
        public override string ToString()
        {
            string s = $"FIND TEACHERS ";
            for (int i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            return s;
        }
        public void Undo()
        {
            int pp = (Console.GetCursorPosition().Top);
            Console.SetCursorPosition(0, p);
            for (int i = 0; i < count; i++)
                Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
            Console.SetCursorPosition(0, pp);
        }
    }
    public class FindRooms : ICommand
    {
        System.Collections.Generic.ICollection<IRoom> rooms;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IRoom, int, bool>> fields;
        string[] Args;
        int p;
        int count = 0;
        public string Name { get; } = "FIND ROOMS";
        public string Description { get; } = "Prints rooms matching certain conditions";
        public void Init(string[]args,University u, string[] args2=null)
        {
            Args = args;
            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], IRoom, int, bool>>();
            fields.Add("NUMBER", PrintNumber);
            fields.Add("TYPE", PrintType);
            fields.Add("", Print);
            rooms = u.rooms;

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);

            
            foreach (var v in rooms)
            {
                for (int i = 0; i < args.Length - 2; i += 3)
                {
                    if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                    if (!preds.ContainsKey(args[i + 1])) throw new InvalidArg(args[i + 1]);
                }
            }
        }
        public void Execute()
        {
            count = 0;
            p = Console.GetCursorPosition().Top;
            foreach (var v in rooms)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good)
                {
                    Console.WriteLine(v);
                    count++;
                }
            }
        }
        private bool PrintNumber(string[] s,IRoom r,int i)
        {   
                if (!preds[s[i + 1].ToUpper()](r.GetNumber(), int.Parse(s[i + 2]))) return false;
                return true;    
        }
        private bool PrintType(string[] s, IRoom r, int i)
        {
                if (!preds[s[i + 1].ToUpper()](r.GetRoomType(), s[i + 2])) return false;
                return true;     
        }
        private bool Print(string[] s, IRoom r, int i)
        {
            return true;
        }
        public override string ToString()
        {
            string s = $"FIND ROOMS ";
            for (int i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            return s;
        }
        public void Undo()
        {
            int pp = (Console.GetCursorPosition().Top);
            Console.SetCursorPosition(0, p);
            for (int i = 0; i < count; i++)
                Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
            Console.SetCursorPosition(0, pp);
        }

    }
    public class FindClasses : ICommand
    {
        System.Collections.Generic.ICollection<IMyClass> classes;
        bool[] goodclasses;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IMyClass, int, bool>> fields;
        string[] Args;
        int count = 0;
        int p;
        public string Name { get; } = "FIND CLASSES";
        public string Description { get; } = "Prints classes matching certain conditions";
        public void Init(string[]args,University u,string[]args2=null)
        {
            Args = args;
            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], IMyClass, int, bool>>();
            fields.Add("NAME", PrintName);
            fields.Add("CODE", PrintCode);
            fields.Add("DURATION", PrintDuration);
            fields.Add("", Print);
            classes = u.classes;
            goodclasses = new bool[classes.Count];

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);

            int j = 0;
            foreach (var v in classes)
            {
                goodclasses[j] = true;
                for (int i = 0; i < args.Length - 2; i += 3)
                {
                    if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                    if (!preds.ContainsKey(args[i + 1])) throw new InvalidArg(args[i + 1]);

                    if (!fields[args[i].ToUpper()](args, v, i)) goodclasses[j] = false;
                }
                j++;
            }

        }
        public void Execute()
        {
            p = Console.GetCursorPosition().Top;
            count = 0;
            foreach (var v in classes)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good)
                {
                    Console.WriteLine(v);
                    count++;
                }
            }
        }
        private bool PrintName(string[] s, IMyClass c, int i)
        { 
                if (!preds[s[i + 1].ToUpper()](c.GetName(), s[i + 2])) return false;
                return true;
        }
        private bool PrintCode(string[] s, IMyClass c, int i)
        {

            if (!preds[s[i + 1].ToUpper()](c.GetCode(), s[i + 2])) return false;
            return true;
            
        }
        private bool PrintDuration(string[] s, IMyClass c, int i)
        {
            if (!preds[s[i + 1].ToUpper()](c.GetDuration(), int.Parse(s[i + 2]))) return false;
            return true;
            
        }
        private bool Print(string[] s,IMyClass c, int i)
        {
            return true;
        }
        public override string ToString()
        {
            string s= $"FIND CLASSES ";
            for (int i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            return s;
        }
        public void Undo()
        {
            int pp = (Console.GetCursorPosition().Top);
            Console.SetCursorPosition(0, p);
            for (int i = 0; i < count; i++)
                Console.Write(Enumerable.Repeat<char>(' ', Console.BufferWidth).ToArray());
            Console.SetCursorPosition(0, pp);
        }
    }
}