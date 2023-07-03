using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Room_Adapter2 : IRoom
    {
        Room_Adaptee2 room;
        public Room_Adapter2(Room_Adaptee2 r)
        {
            room = r;
        }
        public int GetNumber()
        {
            return room.number;
        }
        public string GetRoomType()
        {
            return room.type;
        }
        public void ChangeNumber(int n)
        {
            room.number = n;
        }
        public void ChangeRoomType(string t)
        {
            room.type = t;
        }
        public List<IMyClass> GetClasses()
        {
            List<IMyClass> cl = new List<IMyClass>();
            string[] s = room.classes.Split(",");
            foreach (var v in s)
                cl.Add((IMyClass)HashMap2.hashmap[v]);
            return cl;
        }

        public void AddClass(IMyClass cl)
        {
            if (room.classes.CompareTo("")==0)
                room.classes = cl.GetCode();
            else
                room.classes = room.classes + "," + cl.GetCode();

        }
        public override string ToString()
        {
            string s = $"Room number: {GetNumber()} Type: {GetRoomType()}";
            return s;
        }
        
    }
    public class Room_Adaptee2
	{
        public int number;
        public string type;
        public string classes;
        public Room_Adaptee2(int n, string t, List<IMyClass> cl)
        {
            number = n;
            type = t;
            //classes = string.Join(",", cl.Select(v => v.GetCode()));
            classes = new string("");
        }
    }
}
