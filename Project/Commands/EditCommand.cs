using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class EditCommand:ICommand
    {
        Dictionary<string, ICommandFactory> Edits;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;

        ICommand edit;
        public void Init(string[] args, University u,string[]args2=null)
        {

            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            Edits = new Dictionary<string, ICommandFactory>();
            Edits.Add("STUDENTS", new EditStudentsFactory());
            Edits.Add("TEACHERS", new EditTeachersFactory());
            Edits.Add("ROOMS", new EditRoomsFactory());
            Edits.Add("CLASSES", new EditClassesFactory());

            if (Edits.ContainsKey(args[0].ToUpper()) == false) throw new InvalidClass(args[0]);
            edit = Edits[args[0].ToUpper()].Create(args.Skip(1).ToArray(), u,args2);

        }
        public void Execute()
        {
            edit.Execute();
        }
        public override string ToString()
        {
            return edit.ToString();
        }
        public void Undo()
        {
            edit.Undo();
        }
    }
    public class EditStudents : ICommand
    {
        System.Collections.Generic.ICollection<IStudent> students;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IStudent, int, bool>> fields;
        Dictionary<string, Action<IStudent,string>> editfields;
        List<(string, Action<IStudent,string>)> edits;
        List<string> names;
        List<IStudent> edited;
        List<(string surname, string code, int semester)> editedfields;
        bool done = true;
        string[] Args;
        public void Init(string[] args, University u,string[]args2)
        {
            Args = args;
            edited = new List<IStudent>();
            editedfields = new List<(string surname, string code, int semester)>();
            names = new List<string>();
            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], IStudent, int, bool>>();
            fields.Add("SURNAME", PrintSurname);
            fields.Add("CODE", PrintCode);
            fields.Add("SEMESTER", PrintSemester);
            fields.Add("", Print);

            editfields = new Dictionary<string, Action<IStudent,string>>();
            editfields.Add("SURNAME", EditSurname);
            editfields.Add("CODE", EditCode);
            editfields.Add("SEMESTER", EditSemester);

            edits = new List<(string, Action<IStudent,string>)>();

            students = u.students;

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);

            int i;
            for (i = 0; i < args.Length - 2; i += 3)
            {
                if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                if (args.Length > 1 && !preds.ContainsKey(args[i + 1])) throw new InvalidArg(args[i + 1]);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            if (args2 == null) Console.WriteLine("FIELDS: SURNAME, SEMESTER, CODE");
            Console.ResetColor();

            i = 0;

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
                    if (editfields.ContainsKey(ss[0].ToUpper()) == false || ss.Length != 2)
                        throw new InvalidArg(ss[0]);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"INVALID ARGUMENT {ss[0]}");
                    Console.ResetColor();
                    continue;
                }
                edits.Add((ss[1], editfields[ss[0].ToUpper()]));
                names.Add(ss[0].ToUpper());
            }
        }
        public void Execute()
        {
            if (!done) return;
            foreach (var v in students)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if(good)
                {
                    edited.Add(v);
                    editedfields.Add((v.GetSurname(), v.GetCode(), v.GetSemester()));
                    foreach(var e in edits)
                    {
                        e.Item2(v, e.Item1);
                    }
                }
                
            }
        }
        private bool PrintSurname(string[] s, IStudent st, int i)
        {

            if (!preds[s[i + 1].ToUpper()](st.GetSurname(), s[i + 2])) return false;
            return true;
        }
        private bool PrintCode(string[] s, IStudent st, int i)
        {

            if (!preds[s[i + 1].ToUpper()](st.GetCode(), s[i + 2])) return false;
            return true;

        }
        private bool PrintSemester(string[] s, IStudent st, int i)
        {

            if (!preds[s[i + 1].ToUpper()](st.GetSemester(), int.Parse(s[i + 2]))) return false;
            return true;

        }
        private bool Print(string[] s, IStudent st, int i)
        {
            return true;
        }
        private void EditSurname(IStudent st,string s)
        {
            st.ChangeSurname(s);
        }
        private void EditCode(IStudent st,string s)
        {
            st.ChangeCode(s);

        }
        private void EditSemester(IStudent st,string s)
        {
            st.ChangeCode(s);
        }
        public override string ToString()
        {
            int i;
            string s = $"EDIT STUDENTS ";
            for (i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            string[] names2 = names.ToArray();
            i = 0;
            foreach (var e in edits)
            {
                s = s + $"\n{names2[i]}={e.Item1}";
                i++;
            }
            string done2 = done ? "DONE" : "EXIT";
            s = s + "\n"+done2;
            return s;
        }
        public void Undo()
        {
            for(int i=0;i<edited.Count;i++)
            {
                edited[i].ChangeSurname(editedfields[i].surname);
                edited[i].ChangeCode(editedfields[i].code);
                edited[i].ChangeSemester(editedfields[i].semester);
            }
        }
    }
    public class EditTeachers : ICommand
    {
        System.Collections.Generic.ICollection<ITeacher> teachers;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], ITeacher, int, bool>> fields;
        Dictionary<string, Action<ITeacher, string>> editfields;
        List<(string, Action<ITeacher, string>)> edits;
        List<string> names;
        bool done = true;
        string[] Args;
        List<ITeacher> edited;
        List<(string surname, string code, string rank)> editedfields;
        public void Init(string[] args, University u, string[]args2)
        {
            Args = args;
            edited = new List<ITeacher>();
            editedfields = new List<(string surname, string code, string rank)>();
            names = new List<string>();
            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], ITeacher, int, bool>>();
            fields.Add("SURNAME", PrintSurname);
            fields.Add("CODE", PrintCode);
            fields.Add("RANK", PrintRank);
            fields.Add("", Print);

            editfields = new Dictionary<string, Action<ITeacher, string>>();
            editfields.Add("SURNAME", EditSurname);
            editfields.Add("CODE", EditCode);
            editfields.Add("RANK", EditRank);

            teachers = u.teachers;
            edits = new List<(string, Action<ITeacher, string>)>();

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);
            int i;
            for (i = 0; i < args.Length - 2; i += 3)
            {
                if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                if (!preds.ContainsKey(args[i + 1])) throw new InvalidArg(args[i + 1]);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            if (args2 == null) Console.WriteLine("FIELDS: SURNAME, RANK, CODE");
            Console.ResetColor();
            i = 0;
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
                    if (editfields.ContainsKey(ss[0].ToUpper()) == false || ss.Length != 2)
                        throw new InvalidArg(ss[0]);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"INVALID ARGUMENT {ss[0]}");
                    Console.ResetColor();
                    continue;
                }
                edits.Add((ss[1], editfields[ss[0].ToUpper()]));
                names.Add(ss[0].ToUpper());
            }
        }
        public void Execute()
        {
            if (!done) return;
            foreach (var v in teachers)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good)
                {
                    edited.Add(v);
                    editedfields.Add((v.GetSurname(), v.GetCode(), v.GetRank()));
                    foreach (var e in edits)
                    {
                        e.Item2(v, e.Item1);
                    }
                }
            }
        }
        private bool PrintSurname(string[] s, ITeacher t, int i)
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
        private void EditSurname(ITeacher st, string s)
        {
            st.ChangeSurname(s);
        }
        private void EditCode(ITeacher st, string s)
        {
            st.ChangeCode(s);

        }
        private void EditRank(ITeacher st, string s)
        {
            st.ChangeRank(s);
        }
        public override string ToString()
        {
            int i;
            string s = $"EDIT TEACHERS ";
            for (i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            string[] names2 = names.ToArray();
            i = 0;
            foreach (var e in edits)
            {
                s = s + $"\n{names2[i]}={e.Item1}";
                i++;
            }
            string done2 = done ? "DONE" : "EXIT";
            s = s + "\n" + done2;
            return s;
        }
        public void Undo()
        {
            for (int i = 0; i < edited.Count; i++)
            {
                edited[i].ChangeSurname(editedfields[i].surname);
                edited[i].ChangeCode(editedfields[i].code);
                edited[i].ChangeRank(editedfields[i].rank);
            }
        }
    }
    public class EditRooms : ICommand
    {
        System.Collections.Generic.ICollection<IRoom> rooms;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IRoom, int, bool>> fields;
        Dictionary<string, Action<IRoom, string>> editfields;
        List<(string, Action<IRoom, string>)> edits;
        List<string> names;
        string[] Args;
        bool done = true;
        List<IRoom> edited;
        List<(int number, string type)> editedfields;
        public void Init(string[] args, University u,string[]args2)
        {
            Args = args;
            edited = new List<IRoom>();
            editedfields = new List<(int number, string type)>();
            names = new List<string>();
            preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
            preds.Add("=", (a, b) => a.Equals(b));
            preds.Add(">", (a, b) => a.CompareTo(b) > 0);
            preds.Add("<", (a, b) => a.CompareTo(b) < 0);

            fields = new Dictionary<string, Func<string[], IRoom, int, bool>>();
            fields.Add("NUMBER", PrintNumber);
            fields.Add("TYPE", PrintType);
            fields.Add("", Print);
            rooms = u.rooms;

            editfields = new Dictionary<string, Action<IRoom, string>>();
            editfields.Add("NUMBER", EditNumber);
            editfields.Add("TYPE", EditType);

            rooms = u.rooms;
            edits = new List<(string, Action<IRoom, string>)>();

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);
            int i;
            for (i = 0; i < args.Length - 2; i += 3)
            {
                if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                if (!preds.ContainsKey(args[i + 1])) throw new InvalidArg(args[i + 1]);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            if(args2==null) Console.WriteLine("FIELDS: TYPE, NUMBER");
            Console.ResetColor();
            i = 0;
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
                    if (editfields.ContainsKey(ss[0].ToUpper()) == false || ss.Length != 2)
                        throw new InvalidArg(ss[0]);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"INVALID ARGUMENT {ss[0]}");
                    Console.ResetColor();
                    continue;
                }
                edits.Add((ss[1], editfields[ss[0].ToUpper()]));
                names.Add(ss[0].ToUpper());

            }
        }
        public void Execute()
        {
            if (!done) return;
            foreach (var v in rooms)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good)
                {
                    edited.Add(v);
                    editedfields.Add((v.GetNumber(), v.GetRoomType()));
                    foreach (var e in edits)
                    {
                        e.Item2(v, e.Item1);
                    }
                }
            }
        }
        private bool PrintNumber(string[] s, IRoom r, int i)
        {
            try
            {
                if (!preds[s[i + 1].ToUpper()](r.GetNumber(), int.Parse(s[i + 2]))) return false;
                return true;
            }
            catch
            {
                throw new InvalidArg(s[i + 2]);
            }
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
        public void EditNumber(IRoom r,string s)
        {
            r.ChangeNumber(int.Parse(s));
        }
        public void EditType(IRoom r,string s)
        {
            r.ChangeRoomType(s);
        }
        public override string ToString()
        {
            int i = 0;
            string s = $"EDIT ROOMS ";
            for (i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            string[] names2 = names.ToArray();
            i = 0;
            foreach (var e in edits)
            {
                s = s + $"\n{names2[i]}={e.Item1}";
                i++;
            }
            string done2 = done ? "DONE" : "EXIT";
            s = s + "\n" + done2;
            return s;
        }
        public void Undo()
        {
            for (int i = 0; i < edited.Count; i++)
            {
                edited[i].ChangeNumber(editedfields[i].number);
                edited[i].ChangeRoomType(editedfields[i].type);
            }
        }

    }
    public class EditClasses : ICommand
    {
        System.Collections.Generic.ICollection<IMyClass> classes;
        Dictionary<string, Func<IComparable, IComparable, bool>> preds;
        Dictionary<string, Func<string[], IMyClass, int, bool>> fields;
        Dictionary<string, Action<IMyClass, string>> editfields;
        List<(string, Action<IMyClass, string>)> edits;
        List<string> names;
        string[] Args;
        List<IMyClass> edited;
        bool done = true;
        List<(string name, string code, int duration)> editedfields;
        public void Init(string[] args, University u,string[]args2)
        {
            Args = args;
            edited = new List<IMyClass>();
            editedfields = new List<(string name, string code, int duration)>();
            names = new List<string>();
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

            editfields = new Dictionary<string, Action<IMyClass, string>>();
            editfields.Add("NAME", EditName);
            editfields.Add("CODE", EditCode);
            editfields.Add("DURATION", EditDuration);

            edits = new List<(string, Action<IMyClass, string>)>();

            if (args.Length != 1 && args.Length % 3 != 0)
                throw new NotEnough(args[0]);
            int i;
            for (i = 0; i < args.Length - 2; i += 3)
            {
                if (!fields.ContainsKey(args[i].ToUpper())) throw new InvalidArg(args[i]);
                if (!preds.ContainsKey(args[i + 1])) throw new InvalidArg(args[i + 1]);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            if(args2==null) Console.WriteLine("FIELDS: NAME, CODE, DURATION");
            Console.ResetColor();
            i= 0;
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
                    if (editfields.ContainsKey(ss[0].ToUpper()) == false || ss.Length != 2)
                        throw new InvalidArg(ss[0]);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"INVALID ARGUMENT {ss[0]}");
                    Console.ResetColor();
                    continue;
                }
                edits.Add((ss[1], editfields[ss[0].ToUpper()]));
                names.Add(ss[0].ToUpper());
            }
        }
        public void Execute()
        {
            if (!done) return;
            foreach (var v in classes)
            {
                bool good = true;
                for (int i = 0; i < Args.Length - 2; i += 3)
                {
                    if (!fields[Args[i].ToUpper()](Args, v, i)) good = false;
                }
                if (good)
                {
                    edited.Add(v);
                    editedfields.Add((v.GetName(), v.GetCode(), v.GetDuration()));
                    foreach (var e in edits)
                    {
                        e.Item2(v, e.Item1);
                    }
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
            try
            {
                if (!preds[s[i + 1].ToUpper()](c.GetDuration(), int.Parse(s[i + 2]))) return false;
                return true;
            }
            catch
            {
                throw new InvalidArg(s[i + 2]);
            }
        }
        public void EditName(IMyClass c,string s)
        {
            c.ChangeName(s);
        }
        public void EditCode(IMyClass c,string s)
        {
            c.ChangeCode(s);
        }
        public void EditDuration(IMyClass c, string s)
        {
            c.ChangeDuration(int.Parse(s));
        }

        private bool Print(string[] s, IMyClass c, int i)
        {
            return true;
        }
        public override string ToString()
        {
            int i;
            string s = $"EDIT CLASSES ";
            for (i = 0; i < Args.Length - 2; i += 3)
                s = s + Args[i] + " " + Args[i + 1] + " " + Args[i + 2];
            string[] names2 = names.ToArray();
            i = 0;
            foreach(var e in edits)
            {
                s = s + $"\n{names2[i]}={e.Item1}";
                i++;
            }
            string done2 = done ? "DONE" : "EXIT";
            s = s + "\n" + done2;
            return s;
        }
        public void Undo()
        {
            for (int i = 0; i < edited.Count; i++)
            {
                edited[i].ChangeName(editedfields[i].name);
                edited[i].ChangeCode(editedfields[i].code);
                edited[i].ChangeDuration(editedfields[i].duration);
            }
        }
    }
}
