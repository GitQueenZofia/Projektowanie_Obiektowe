using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{

    public class ExitCommand : ICommand
    {
        public void Init(string[] args, University u, string[]args2)
        {

        }
        public void Execute()
        {
            MyConsole.flag = false;
        }
        public override string ToString()
        {
            return "EXIT";
        }
        public void Undo()
        {

        }
    }
}