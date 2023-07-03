using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class DeleteCommand : ICommand
    {
        private Dictionary<string, ICommandFactory> DelDict;
        private ICommand del;
        public void Init(string[] args, University u,string[]args2=null)
        {
            DelDict = new Dictionary<string, ICommandFactory>();
            DelDict.Add("STUDENT", new DeleteStudentFactory());
            DelDict.Add("TEACHER", new DeleteTeacherFactory());
            DelDict.Add("ROOM", new DeleteRoomFactory());
            DelDict.Add("CLASS", new DeleteClassFactory());

            if (DelDict.ContainsKey(args[0].ToUpper()) == false) throw new InvalidClass(args[0]);
            del = DelDict[args[0].ToUpper()].Create(args.Skip(1).ToArray(), u);
        }
        public void Execute()
        {
            del.Execute();
        }
        public override string ToString()
        {
            return del.ToString();
        }
        public void Undo()
        {
            del.Undo();
        }
    }
    public class DeleteStudent : ICommand
    {
        System.Collections.Generic.ICollection<IStudent> students;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IStudent, int, bool>> fields;
        string[] Args;
        University university;
        IStudent student;
        List<IMyClass> classes;
        public string Name { get; } = "DELETE STUDENT";
        public void Init(string[] args, University u,string[]args2)
        {
            Args = args;
            university = u;

            classes = new List<IMyClass>();

            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], IStudent, int, bool>>();
            fields.Add("SURNAME", Surname);
            fields.Add("CODE", Code);
            fields.Add("SEMESTER", Semester);
            fields.Add("", Print);
            students = u.students;

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);

            for (int i = 0; i < args.Length - 2; i += 3)
            {
                if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                if (args.Length > 1 && !preds.ContainsKey(args[i + 1])) throw new InvalidArg(args[i + 1]);
            }
        }
        public void Execute()
        {
            IStudent student = null;
            foreach (var v in students)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good)
                {
                    student = v;
                }
            }
            if (student != null)
            {
                this.student = student;
                students.Remove(student);
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("STUDENT DELETED");
                Console.ResetColor();
                foreach (var v in university.classes)
                {
                    if(v.GetStudents().Contains(student))
                    {
                        classes.Add(v);
                        v.GetStudents().Remove(student);
                    }
                }
            }
        }
        private bool Surname(string[] s, IStudent st, int i)
        {
            if (!preds[s[i + 1].ToUpper()](st.GetSurname(), s[i + 2])) return false;
            return true;
        }
        private bool Code(string[] s, IStudent st, int i)
        {
            if (!preds[s[i + 1].ToUpper()](st.GetCode(), s[i + 2])) return false;
            return true;
        }
        private bool Semester(string[] s, IStudent st, int i)
        {
            if (!preds[s[i + 1].ToUpper()](st.GetSemester(), int.Parse(s[i + 2]))) return false;
            return true;
        }
        private bool Print(string[] s, IStudent st, int i)
        {
            return true;
        }
        public override string ToString()
        {
            string s = $"DELETE STUDENT ";
            for (int i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            return s;
        }
        public void Undo()
        {
            if(student!=null)
            {
                university.students.Add(student);
                foreach (var v in classes)
                    v.AddStudent(student);
            }
        }
    }
    public class DeleteTeacher : ICommand
    {
        System.Collections.Generic.ICollection<ITeacher> teachers;
        bool[] goodteachers;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], ITeacher, int, bool>> fields;
        string[] Args;
        University university;
        List<IMyClass> classes;
        ITeacher teacher;
        public string Name { get; } = "DELETE TEACHER";
        public void Init(string[] args, University u,string[]arsg2=null)
        {
            Args = args;
            university = u;

            classes = new List<IMyClass>();

            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], ITeacher, int, bool>>();
            fields.Add("SURNAME", Surname);
            fields.Add("CODE", Code);
            fields.Add("RANK", Rank);
            fields.Add("", Print);
            teachers = u.teachers;
            goodteachers = new bool[teachers.Count];

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);

            foreach (var v in teachers)
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
            ITeacher teacher = null;
            foreach (var v in teachers)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good) teacher = v;
            }
            if (teacher != null)
            {
                this.teacher = teacher;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("TEACHER DELETED");
                Console.ResetColor();
                teachers.Remove(teacher);
                foreach (var v in university.classes)
                {
                    if(v.GetTeachers().Contains(teacher))
                    {
                        classes.Add(v);
                        v.GetTeachers().Remove(teacher);
                    }
                }
            }
        }
        private bool Surname(string[] s, ITeacher t, int i)
        {
            if (preds[s[i + 1].ToUpper()](t.GetSurname(), s[i + 2])) return false;
            return true;
        }
        private bool Code(string[] s, ITeacher t, int i)
        {
            if (!preds[s[i + 1].ToUpper()](t.GetCode(), s[i + 2])) return false;
            return true;
        }
        private bool Rank(string[] s, ITeacher t, int i)
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
            string s = $"DELETE TEACHER ";
            for (int i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            return s;
        }
        public void Undo()
        {
            if(teacher!=null)
            {
                university.teachers.Add(teacher);
                foreach(var v in classes)
                {
                    v.AddTeacher(teacher);
                }
            }
        }
    }
    public class DeleteRoom : ICommand
    {
        System.Collections.Generic.ICollection<IRoom> rooms;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IRoom, int, bool>> fields;
        string[] Args;
        University university;
        IRoom room;
        public void Init(string[] args, University u,string[]args2)
        {
            Args = args;
            university = u;

            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], IRoom, int, bool>>();
            fields.Add("NUMBER", Number);
            fields.Add("TYPE", Type);
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
            IRoom room = null;
            foreach (var v in rooms)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good) room = v;
            }
            if(room!=null)
            {
                this.room = room;
                rooms.Remove(room);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ROOM DELETED");
                Console.ResetColor();
            }
            
        }
        private bool Number(string[] s, IRoom r, int i)
        {
            if (!preds[s[i + 1].ToUpper()](r.GetNumber(), int.Parse(s[i + 2]))) return false;
            return true;
        }
        private bool Type(string[] s, IRoom r, int i)
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
            string s = $"DELETE ROOM ";
            for (int i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            return s;
        }
        public void Undo()
        {
            if(room!=null)
            {

                university.rooms.Add(room);
            }
        }

    }
    public class DeleteClass : ICommand
    {
        System.Collections.Generic.ICollection<IMyClass> classes;
        bool[] goodclasses;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IMyClass, int, bool>> fields;
        string[] Args;
        University university;
        IMyClass c;
        List<IRoom> rooms;
        List<ITeacher> teachers;
        List<IStudent> students;
        public void Init(string[] args, University u,string[]args2)
        {
            Args = args;
            university = u;

            rooms = new List<IRoom>();
            teachers = new List<ITeacher>();
            students = new List<IStudent>();

            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], IMyClass, int, bool>>();
            fields.Add("NAME", Name);
            fields.Add("CODE", Code);
            fields.Add("DURATION", Duration);
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
            IMyClass c=null;
            foreach (var v in classes)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good) c = v;
            }
            if (c != null)
            {
                this.c = c;
                classes.Remove(c);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("CLASS DELETED");
                Console.ResetColor();

                foreach (var v in university.students)
                {
                    if(v.GetClasses().Contains(c))
                    {
                        students.Add(v);
                        v.GetClasses().Remove(c);
                    }
                }
                foreach(var v in university.teachers)
                {
                    if (v.GetClasses().Contains(c))
                    {
                        teachers.Add(v);
                        v.GetClasses().Remove(c);
                    }
                }
                foreach(var v in university.rooms)
                {
                    if (v.GetClasses().Contains(c))
                    {
                        rooms.Add(v);
                        v.GetClasses().Remove(c);
                    }
                }
            } 
            
        }
        private bool Name(string[] s, IMyClass c, int i)
        {
            if (!preds[s[i + 1].ToUpper()](c.GetName(), s[i + 2])) return false;
            return true;
        }
        private bool Code(string[] s, IMyClass c, int i)
        {

            if (!preds[s[i + 1].ToUpper()](c.GetCode(), s[i + 2])) return false;
            return true;

        }
        private bool Duration(string[] s, IMyClass c, int i)
        {
            if (!preds[s[i + 1].ToUpper()](c.GetDuration(), int.Parse(s[i + 2]))) return false;
            return true;

        }
        private bool Print(string[] s, IMyClass c, int i)
        {
            return true;
        }
        public override string ToString()
        {
            string s = $"DELETE CLASS ";
            for (int i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            return s;
        }
        public void Undo()
        {
            if(c!=null)
            {
                university.classes.Add(c);
                foreach (var v in students)
                    v.AddClass(c);
                foreach (var v in teachers)
                    v.AddClass(c);
                foreach (var v in rooms)
                    v.AddClass(c);
            }
        }
    }
}