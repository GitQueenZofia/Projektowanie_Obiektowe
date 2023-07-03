using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class AddCommand:ICommand
    {
        private Dictionary<string, ICommandFactory> AddDict;
        private ICommand add;
        
        public void Init(string[] args, University u, string[] args2 = null)
        {
            AddDict = new Dictionary<string, ICommandFactory>();
            AddDict.Add("STUDENT", new AddStudentFactory());
            AddDict.Add("TEACHER", new AddTeacherFactory());
            AddDict.Add("ROOM", new AddRoomFactory());
            AddDict.Add("CLASS", new AddClassFactory());

            if (AddDict.ContainsKey(args[0].ToUpper()) == false) throw new InvalidClass(args[0]);
            if (args.Length < 2) throw new NotEnough(args[0]);
            add = AddDict[args[0].ToUpper()].Create(args.Skip(1).ToArray(),u,args2);

        }
        public void Execute()
        {
            add.Execute();
        }
        public override string ToString()
        {
            return add.ToString();
        }
        public void Undo()
        {
            add.Undo();
        }
    }
    public class AddStudent : ICommand
    {
        System.Collections.Generic.ICollection<IStudent> students;
        Dictionary<string, StudentFactory> factories;
        Dictionary<string, string> fields;
        StudentFactory factory;
        IStudent student;
        string rep;
        bool done = true;
        public void Init(string[] args, University u, string[] args2 = null)
        {
            students = u.students;
            factories = new Dictionary<string, StudentFactory>();
            factories.Add("BASE", new StudentBase());
            factories.Add("SECONDARY", new StudentSecond());

            Console.ForegroundColor = ConsoleColor.Cyan;
            if (args2 == null) Console.WriteLine("FIELDS: SURNAME, SEMESTER, CODE");
            Console.ResetColor();

            fields = new Dictionary<string, string>();
            fields.Add("SURNAME", "");
            fields.Add("SEMESTER", "0");
            fields.Add("CODE", "");

            if (factories.ContainsKey(args[0].ToUpper()) == false) throw new InvalidArg(args[0]);
            factory = factories[args[0].ToUpper()];
            rep = args[0].ToUpper();
            int i = 0;

            while (true)
            {
                string s;
                if (args2 == null) s = Console.ReadLine();
                else s = args2[i];
                i++;

                string[] ss = s.Split("=");
                if (ss[0].ToUpper() == "EXIT")
                {
                    done = false;
                    break;
                }
                if (ss[0].ToUpper() == "DONE")
                {
                    break;
                }
                try
                {
                    if (fields.ContainsKey(ss[0].ToUpper()) == false || ss.Length != 2)
                        throw new InvalidArg(ss[0]);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"INVALID ARGUMENT {ss[0]}");
                    Console.ResetColor();
                    continue;
                }

                fields.Remove(ss[0].ToUpper());
                fields.Add(ss[0].ToUpper(), ss[1]);
            }
        }
        public void Execute()
        {
            if(done==true)
            {
                student = factory.Create(new List<string>(), fields["SURNAME"], int.Parse(fields["SEMESTER"]), fields["CODE"], new List<IMyClass>());
                students.Add(student);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("NEW STUDENT ADDED");
                Console.ResetColor();
            }

        }
        public override string ToString()
        {
            string done2 = done ? "DONE" : "EXIT";
            return $"ADD STUDENT {rep}\nSURNAME={fields["SURNAME"]}\nSEMESTER={fields["SEMESTER"]}\nCODE={fields["CODE"]}\n{done2}";
        }
        public void Undo()
        {
            if(done)
            students.Remove(student);
        }
    }
    public class AddTeacher : ICommand
    {
        System.Collections.Generic.ICollection<ITeacher> teachers;
        Dictionary<string, TeacherFactory> factories;
        Dictionary<string, string> fields;
        TeacherFactory factory;
        string rep;
        bool done = true;
        ITeacher teacher;

        public void Init(string[] args, University u, string[] args2=null)
        {
            teachers = u.teachers;
            factories = new Dictionary<string, TeacherFactory>();
            factories.Add("BASE", new TeacherBase());
            factories.Add("SECONDARY", new TeacherSecond());

            Console.ForegroundColor = ConsoleColor.Cyan;
            if(args2==null) Console.WriteLine("FIELDS: SURNAME, RANK, CODE");
            Console.ResetColor();

            fields = new Dictionary<string, string>();
            fields.Add("SURNAME", "");
            fields.Add("RANK", "");
            fields.Add("CODE", "");

            if (factories.ContainsKey(args[0].ToUpper()) == false) throw new InvalidArg(args[0]);
            factory = factories[args[0].ToUpper()];
            rep = args[0].ToUpper();
            int i = 0;
            while (true)
            {

                string s;
                if (args2 == null) s = Console.ReadLine();
                else s = args2[i];
                i++;

                string[] ss = s.Split("=");
                if (ss[0].ToUpper() == "EXIT")
                {
                    done = false;
                    break;
                }
                if (ss[0].ToUpper() == "DONE")
                {          
                    break;
                }
                try
                {
                    if (fields.ContainsKey(ss[0].ToUpper()) == false || ss.Length != 2)
                        throw new InvalidArg(ss[0]);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"INVALID ARGUMENT {ss[0]}");
                    Console.ResetColor();
                    continue;
                }
                fields.Remove(ss[0].ToUpper());
                fields.Add(ss[0].ToUpper(), ss[1]);
            }
        }
        public void Execute()
        {
            if (done)
            {
                teacher = factory.Create(new List<string>(), fields["SURNAME"], fields["RANK"], fields["CODE"], new List<IMyClass>());
                teachers.Add(teacher);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("NEW TEACHER ADDED");
                Console.ResetColor();
            }
        }
        public override string ToString()
        {
            string done2 = done ? "DONE" : "EXIT";
            return $"ADD TEACHER {rep}\nSURNAME={fields["SURNAME"]}\nRANK={fields["RANK"]}\nCODE={fields["CODE"]}\n{done2}";
        }
        public void Undo()
        {
            if(done)
            teachers.Remove(teacher);
        }
    }
    public class AddRoom : ICommand
    {
        System.Collections.Generic.ICollection<IRoom> rooms;
        Dictionary<string, RoomFactory> factories;
        Dictionary<string, string> fields;
        RoomFactory factory;
        string rep;
        bool done = true;
        IRoom room;
        public void Init(string[] args, University u, string[] args2)
        {
            rooms = u.rooms;
            factories = new Dictionary<string, RoomFactory>();
            factories.Add("BASE", new RoomBase());
            factories.Add("SECONDARY", new RoomSecond());

            if (factories.ContainsKey(args[0].ToUpper()) == false) throw new InvalidArg(args[0]);
            factory = factories[args[0].ToUpper()];
            rep = args[0].ToUpper();

            Console.ForegroundColor = ConsoleColor.Cyan;
            if (args2 == null) Console.WriteLine("FIELDS: TYPE, NUMBER");
            Console.ResetColor();

            fields = new Dictionary<string, string>();
            fields.Add("TYPE", "");
            fields.Add("NUMBER", "-1");

            factory=factories[args[0].ToUpper()];
            int i = 0;
            while (true)
            {
                string s;
                if (args2 == null) s = Console.ReadLine();
                else s = args2[i];
                i++;

                string[] ss = s.Split("=");
                if (ss[0].ToUpper() == "EXIT")
                {
                    done = false;
                    break;
                }
                if (ss[0].ToUpper() == "DONE")
                {
                    try { int dur = int.Parse(fields["NUMBER"]); }
                    catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("FAILED: NUMBER SHOULD BE TYPE OF INT"); Console.ResetColor(); continue; }
                    break;
                }

                try
                {
                    if (fields.ContainsKey(ss[0].ToUpper()) == false || ss.Length != 2)
                        throw new InvalidArg(ss[0]);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"INVALID ARGUMENT {ss[0]}");
                    Console.ResetColor();
                    continue;
                }
                fields.Remove(ss[0].ToUpper());
                fields.Add(ss[0].ToUpper(), ss[1]);
            }
        }
        public void Execute()
        {
            if (done)
            {
                room = factory.Create(int.Parse(fields["NUMBER"]), fields["TYPE"], new List<IMyClass>());
                rooms.Add(room);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("NEW ROOM ADDED");
                Console.ResetColor();
            }
        }
        public override string ToString()
        {
            string done2 = done ? "DONE" : "EXIT";
            return $"ADD ROOM {rep}\nNUMBER={fields["NUMBER"]}\nTYPE={fields["TYPE"]}\n{done2}";
        }
        public void Undo()
        {
            if(done)
            rooms.Remove(room);
        }
    }
    public class AddClass : ICommand
    {
        System.Collections.Generic.ICollection<IMyClass> classes;
        Dictionary<string, ClassFactory> factories;
        ClassFactory factory;
        string rep;
        Dictionary<string, string> fields;
        bool done = true;
        IMyClass c;
        public void Init(string[] args, University u, string[]args2)
        {
            classes = u.classes;
            factories = new Dictionary<string, ClassFactory>();
            factories.Add("BASE", new ClassBase());
            factories.Add("SECONDARY", new ClassSecond());

            if(factories.ContainsKey(args[0].ToUpper())==false) throw new InvalidArg(args[0]);
            factory = factories[args[0].ToUpper()];
            rep = args[0].ToUpper();

            Console.ForegroundColor = ConsoleColor.Cyan;
            if (args2 == null) Console.WriteLine("FIELDS: NAME, CODE, DURATION");
            Console.ResetColor();

            fields = new Dictionary<string, string>();
            fields.Add("NAME", "");
            fields.Add("CODE", "");
            fields.Add("DURATION", "0");
            int i = 0;

            while (true)
            {
                string s;
                if (args2 == null) s = Console.ReadLine();
                else s = args2[i];
                i++;

                string[] ss = s.Split("=");
                if (ss[0].ToUpper() == "EXIT")
                {
                    done = false;
                    break;
                }
                if (ss[0].ToUpper() == "DONE")
                {
                    try { int dur = int.Parse(fields["DURATION"]); }
                    catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("FAILED: DURATION SHOULD BE TYPE OF INT"); Console.ResetColor(); continue; }
                    break;
                }
                try
                {
                    if (fields.ContainsKey(ss[0].ToUpper()) == false || ss.Length != 2)
                        throw new InvalidArg(ss[0]);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"INVALID ARGUMENT {ss[0]}");
                    Console.ResetColor();
                    continue;
                }
                fields.Remove(ss[0].ToUpper());
                fields.Add(ss[0].ToUpper(), ss[1]);
            }
        }
        public void Execute()
        {
            if (done)
            {
                c = factory.Create(fields["NAME"], fields["CODE"], int.Parse(fields["DURATION"]), new List<ITeacher>(), new List<IStudent>());
                classes.Add(c);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("NEW CLASS ADDED");
                Console.ResetColor();
            }
        }
        public override string ToString()
        {
            string done2 = done ? "DONE" : "EXIT";
            return $"ADD CLASS {rep}\nNAME={fields["NAME"]}\nCODE={fields["CODE"]}\nDURATION={fields["DURATION"]}\n{done2}";
        }
        public void Undo()
        {
            if(done)
            classes.Remove(c);
        }
    }
}