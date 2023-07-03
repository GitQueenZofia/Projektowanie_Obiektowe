using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class MyConsole
    {
        public static List<ICommand> queue;
        public static List<ICommand> history;

        public static bool flag = true;
        University university;
        public static Dictionary<string, ICommandFactory> available;
        public static int lines=0;
        public MyConsole(University u)
        {
            university = u;
            Run();
        }
        public void Run()
        {
            Console.WriteLine("AVAILABLE COMMANDS:");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("LIST");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t <COLLECTION>\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("FIND");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t <COLLECTION> <FIELD = VALUE>\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("EDIT");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t <COLLECTION> <FIELD = VALUE>\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("ADD");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t <CLASS> <BASE|SECONDARY>\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("DELETE");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t <COLLECTION> <FIELD = VALUE>\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("UNDO\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("REDO\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("HISTORY\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("EXIT");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.ResetColor();
            Console.WriteLine();

            queue = new List<ICommand>();
            history = new List<ICommand>();
            available = new Dictionary<string,ICommandFactory>();
            available.Add("LIST", new ListFactory());
            // available.Add("QUEUE", new QueueFactory());
            available.Add("EXPORT", new ExportFactory());
            available.Add("IMPORT", new ImportFactory());
            available.Add("EXIT", new ExitFactory());
            available.Add("FIND", new FindFactory());
            available.Add("ADD", new AddFactory());
            available.Add("EDIT", new EditFactory());
            available.Add("DELETE", new DeleteFactory());
            available.Add("HISTORY", new HistoryFactory());
            available.Add("UNDO", new UndoFactory());
            available.Add("REDO", new RedoFactory());
            
            while (flag)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                string[]args = input.Split(" ");
                string command = args[0].ToUpper();
                if (available.ContainsKey(command) == false)
                {
                    Console.WriteLine($"Invalid command: {command}");
                    continue;
                }
                if (command == "EXIT" || command=="REDO"||command=="UNDO"||command=="HISTORY"|| command=="EXPORT" || command=="IMPORT")
                {
                    try
                    {
                        ICommand c =available[command].Create(args.Skip(1).ToArray(), university);
                        history.Add(c);
                        c.Execute();
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e);
                        Console.ResetColor();
                    }
                }
                else
                {
                    try
                    {
                        ICommand c = available[command].Create(args.Skip(1).ToArray(), university);
                        queue.Add(c);
                        history.Add(c);
                        c.Execute();
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e);
                        Console.ResetColor();
                    }
                }

            }
            Console.WriteLine("Goodbye!");
        }
    }
}

