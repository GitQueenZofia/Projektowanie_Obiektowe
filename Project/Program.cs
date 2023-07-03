using System;
using System.Collections.Generic;
namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
             University u = new University();
             u.Fill();
             MyConsole console = new MyConsole(u);

        }
    }
}
