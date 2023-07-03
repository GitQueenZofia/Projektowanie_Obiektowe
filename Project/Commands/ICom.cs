using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface ICommand
    {
        public void Init(string[] args, University u,string[]args2=null);
        public void Execute();
        public string ToString();
        public void Undo();
    }
}