using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Project
{
    public class Room: IRoom
    {
        int number;
        string type;
        public List<IMyClass> classes;
        public int GetNumber()
        {
             return number; 
        }
        public string GetRoomType()
        {
            return type; 
        }
        public void ChangeNumber(int n)
        {
            number = n;
        }
        public void ChangeRoomType(string t)
        {
            type = t;
        }

        public List<IMyClass> GetClasses()
        {
            return classes;
        }
        public void AddClass(IMyClass cl)
        {
            classes.Add(cl);
        }
        public Room(int n,string t,List<IMyClass> c)
        {
            number = n;
            type = t;
            classes = c;
        }
        public override string ToString()
        {
            string s = $"Room number: {number} Type: {type}";
            return s;
        }
        public IComparable GetField(string name)
        {
            name = name.ToUpper();
            switch (name)
            {
                case "NUMBER":
                    return GetNumber();
                case "TYPE":
                    return GetRoomType();
                default:
                    return -1;

            }
        }
    }
}
