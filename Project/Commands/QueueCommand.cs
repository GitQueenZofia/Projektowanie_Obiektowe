
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;



namespace Project
{
    public class QueueCommand : ICommand
    {
        Dictionary<string, ICommand> q;
        string[] Args;
        University university;
        public void Init(string[] args, University u,string[]args2)
        {
            Args = args;
            university = u;
            q = new Dictionary<string, ICommand>();
            q.Add("PRINT", new QueuePrint());
            q.Add("COMMIT", new QueueCommit());
            q.Add("EXPORT", new QueueExport());
            q.Add("DISMISS", new QueueDismiss());
            q.Add("IMPORT", new QueueImport());
            if (q.ContainsKey(Args[0].ToUpper()) == false) throw new InvalidArg(args[0]);
        }
        public void Execute()
        {
            q[Args[0].ToUpper()].Init(Args.Skip(1).ToArray(), university);
            q[Args[0].ToUpper()].Execute();
        }
        public override string ToString()
        {
            return "QUEUE";
        }
        public void Undo()
        {

        }
    }
    public class QueuePrint : ICommand
    {
        public void Init(string[] args, University u,string[]args2=null)
        {

        }
        public void Execute()
        {
            foreach (var v in MyConsole.queue)
                Console.WriteLine(v);
        }
        public override string ToString()
        {
            return "QUEUE PRINT";
        }
        public void Undo()
        {

        }
    }
    public class QueueCommit : ICommand
    {
        public void Init(string[] args, University u,string[]args2=null)
        {

        }
        public void Execute()
        {
            foreach (var v in MyConsole.queue)
                v.Execute();
            MyConsole.queue.Clear();
        }
        public override string ToString()
        {
            return "QUEUE COMMIT";
        }
        public void Undo()
        {

        }
    }

    public class QueueDismiss:ICommand
    {
        public void Init(string[] args, University u,string[]args2=null)
        {

        }
        public void Execute()
        {
            MyConsole.queue.Clear();
        }
        public override string ToString()
        {
            return "QUEUE DISMISS";
        }
        public void Undo()
        {

        }
    }
    public class QueueExport : ICommand
    {
        string[] Args;
        Dictionary<string, Action<string>> types;
        public void Init(string[] args, University u,string[]args2=null)
        {
            types = new Dictionary<string, Action<string>>();
            types.Add("XML", XML);
            types.Add("PLAINTEXT", PlainText);
            Args = args;
            if (types.ContainsKey(args[0].ToUpper()) == false) throw new InvalidArg(args[0]);
        }
        public void Execute()
        {
            types[Args[0].ToUpper()](Args[1]);
        }
        public void XML(string filename)
        {

            XmlDocument file = new XmlDocument();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;

            using (XmlWriter sw = XmlWriter.Create(filename,settings))
            {
                //StringBuilder sb = new StringBuilder();
                //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(string));
                //foreach (var cmd in MyConsole.queue)
                //    sb.Append($"{cmd.ToString()}$");
                //x.Serialize(sw, sb.ToString());
                sw.WriteStartDocument();
                sw.WriteStartElement("COMMANDS");
                foreach (var q in MyConsole.queue)
                {
                    XmlElement item = file.CreateElement("string");
                    sw.WriteStartElement("COMMAND");
                    sw.WriteValue(q.ToString());
                    sw.WriteEndElement();
                }
                sw.WriteEndElement();
                sw.WriteEndDocument();
                sw.Close();
            }
            
            
            MyConsole.queue.Clear();
        }
        public void PlainText(string s)
        {
            if (!File.Exists(s))
                File.Create(s);

            using (StreamWriter sw = new StreamWriter(s))
            {
                foreach (var cmd in MyConsole.queue)
                {
                    sw.Write($"{cmd.ToString()}$");
                }
                sw.Close();
            }
            MyConsole.queue.Clear();
        }
        public override string ToString()
        {
            return "QUEUE EXPORT";
        }
        public void Undo()
        {

        }
    }
    public class QueueImport : ICommand
    {
        string[] Args;
        Dictionary<string, Action<string>> types;
        University university;
        public void Init(string[] args, University u,string[]args2)
        {
            types = new Dictionary<string, Action<string>>();
            university = u;
            types.Add("XML", XML);
            types.Add("PLAINTEXT", PlainText);
            Args = args;
            if (types.ContainsKey(args[0].ToUpper()) == false) throw new InvalidArg(args[0]);
        }
        public void Execute()
        {
            types[Args[0].ToUpper()](Args[1]);
        }
        public void XML(string filename)
        {
            if (!File.Exists(filename))
                throw new InvalidFile("");

            using (StreamReader sr = new StreamReader(filename))
            {
                //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(string));
                //string input = (string)x.Deserialize(sr);
                //string[] commands = input.Split("$");
                //int i = 0;
                
                XmlReader reader = XmlReader.Create(filename);
                List<string> commands = new List<string>();
                reader.ReadStartElement("COMMANDS");
                while(reader.NodeType!=XmlNodeType.EndElement)
                {
                    if(reader.IsStartElement())
                    {
                        reader.ReadStartElement("COMMAND");
                        commands.Add(reader.ReadContentAsString());
                        reader.ReadEndElement();
                    }            
                }            
                foreach (var v in commands)
                {
                    string[] args = v.Split("\n");
                    string command = args[0].Split(" ")[0].ToUpper();
                    if (command == "") break;
                    try
                    {
                        ICommand c = MyConsole.available[command].Create(args[0].Split(" ").Skip(1).ToArray(), university, args.Skip(1).ToArray());
                        MyConsole.queue.Add(c);
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e);
                        Console.ResetColor();
                    }
                }
                reader.Close();
                sr.Close();
            }
            
        }
        public void PlainText(string filename)
        {
            if (!File.Exists(filename))
                throw new InvalidFile("");
            List<ICommand> queue = MyConsole.queue;
            using (StreamReader sr = new StreamReader(filename))
            {
                string input = sr.ReadToEnd();
                string[] commands = input.Split("$");
                int i = 0;
                foreach(var v in commands)
                {         
                    string[] args = v.Split("\n");
                    string command = args[0].Split(" ")[0].ToUpper();
                    if (command == "") break;
                    try
                    {
                        ICommand c = MyConsole.available[command].Create(args[0].Split(" ").Skip(1).ToArray(), university,args.Skip(1).ToArray());
                        MyConsole.queue.Add(c);
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e);
                        Console.ResetColor();
                    }
                }
                sr.Close();
            }
        }
        public override string ToString()
        {
            return "QUEUE IMPORT";
        }
        public void Undo()
        {

        }
    }
}
