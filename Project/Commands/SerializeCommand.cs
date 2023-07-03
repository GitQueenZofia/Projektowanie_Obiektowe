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
    public class ExportCommand:ICommand
    {
        string[] Args;
        Dictionary<string, Action<string>> types;
        public void Init(string[] args, University u, string[] args2 = null)
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
            MyConsole.lines++;
        }
        public void XML(string filename)
        {
            XmlDocument file = new XmlDocument();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;

            using (XmlWriter sw = XmlWriter.Create(filename, settings))
            {
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
            return "EXPORT";
        }
        public void Undo()
        {

        }
    }
    public class ImportCommand:ICommand
    {
        string[] Args;
        List<ICommand> newcommands;
        Dictionary<string, Action<string>> types;
        University university;
        public void Init(string[] args, University u, string[] args2)
        {
            newcommands = new List<ICommand>();
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
            MyConsole.lines++;
        }
        public void XML(string filename)
        {
            if (!File.Exists(filename))
                throw new InvalidFile("");

            using (StreamReader sr = new StreamReader(filename))
            {
                XmlReader reader = XmlReader.Create(filename);
                List<string> commands = new List<string>();
                reader.ReadStartElement("COMMANDS");
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    if (reader.IsStartElement())
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
                        newcommands.Add(c);
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
            foreach (var v in newcommands)
                v.Execute();

        }
        public void PlainText(string filename)
        {
            if (!File.Exists(filename))
                throw new InvalidFile("");
            newcommands = new List<ICommand>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string input = sr.ReadToEnd();
                string[] commands = input.Split("$");
                int i = 0;
                foreach (var v in commands)
                {
                    string[] args = v.Split("\n");
                    string command = args[0].Split(" ")[0].ToUpper();
                    if (command == "") break;
                    try
                    {
                        ICommand c = MyConsole.available[command].Create(args[0].Split(" ").Skip(1).ToArray(), university, args.Skip(1).ToArray());
                        newcommands.Add(c);
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
            foreach (var v in newcommands)
                v.Execute();
        }
        public override string ToString()
        {
            return "IMPORT";
        }
        public void Undo()
        {

        }
    }

}
