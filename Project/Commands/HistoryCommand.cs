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
    public class HistoryCommand:ICommand
    {
        public void Init(string[] args, University u, string[] args2 = null)
        {

        }
        public void Execute()
        {
            foreach (var v in MyConsole.history)
                Console.WriteLine(v);
        }
        public override string ToString()
        {
            return $"HISTORY";
        }
        public void Undo()
        {

        }
    }
    public class UndoCommand : ICommand
    {
        public void Init(string[] args, University u, string[] args2 = null)
        {

        }
        public void Execute()
        {
            if(MyConsole.queue.Count==0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NO COMMAND TO UNDO!");
                Console.ResetColor();
                return;
            }
            MyConsole.queue[MyConsole.queue.Count - 1].Undo();
            MyConsole.queue.RemoveAt(MyConsole.queue.Count - 1);

        }
        public override string ToString()
        {
            return $"UNDO";
        }
        public void Undo()
        {

        }
    }
    public class RedoCommand : ICommand
    {
        public void Init(string[] args, University u, string[] args2 = null)
        {

        }
        public void Execute()
        {
            if (MyConsole.queue.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NO COMMAND TO REDO!");
                Console.ResetColor();
                return;
            }
            MyConsole.queue[MyConsole.queue.Count - 1].Execute();
        }
        public override string ToString()
        {
            return "REDO";
        }
        public void Undo()
        {
            
        }
    }
}
