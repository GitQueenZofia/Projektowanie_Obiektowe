using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class InvalidFile:Exception
    {
        string ex;
        public InvalidFile(string ex)
        {
            this.ex = ex;
        }
        public override string ToString()
        {
            return $"File doesn't exist";
        }
    }
    public class TooMany : Exception
    {
        string ex;
        public TooMany(string ex)
        {
            this.ex = ex;
        }
        public override string ToString()
        {
            return $"Too many arguments";
        }
    }
    public class NotEnough : Exception
    {
        string ex;
        public NotEnough(string ex)
        {
            this.ex = ex;
        }
        public override string ToString()
        {
            return $"Not enough arguments";
        }
    }
    public class InvalidArg : Exception
    {
        string ex;
        public InvalidArg(string ex)
        {
            this.ex = ex;
        }
        public override string ToString()
        {
            return $"Invalid argument: {ex}";
        }
    }
    public class InvalidClass:Exception
    {
        string ex;
        public InvalidClass(string ex)
        {
            this.ex = ex;
        }
        public override string ToString()
        {
            return $"Invalid class name {ex}";
        }
    }
}